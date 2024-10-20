using System.Collections.Immutable;

namespace Aigamo.Arisama;

public interface ITransition;

public interface IState;

public interface ICommand;

public interface ICommand<TFrom, TTo> : ICommand
	where TFrom : ITransition
	where TTo : IState
{
	TTo Execute(TFrom from);
}

public sealed class StateMachine<TTransition, TCommand, TState>
	where TTransition : ITransition
	where TCommand : ICommand
	where TState : IState
{
	private readonly List<TState> _states = [];
	public IReadOnlyCollection<TState> States => _states.AsReadOnly();

	private readonly ImmutableDictionary<Type, Action<StateMachine<TTransition, TCommand, TState>, TCommand>> _commandHandlers;
	public sealed record StateChangedEventArgs(TState State, TState PreviousState);
	public delegate void StateChangedEventHandler(StateMachine<TTransition, TCommand, TState> sender, StateChangedEventArgs e);

	public event StateChangedEventHandler? StateChanged;

	private StateMachine(ImmutableDictionary<Type, Action<StateMachine<TTransition, TCommand, TState>, TCommand>> commandHandlers)
	{
		_commandHandlers = commandHandlers;
	}

	private void AddState(TState state)
	{
		_states.Add(state);
	}

	internal static StateMachine<TTransition, TCommand, TState> Create(
		ImmutableDictionary<Type, Action<StateMachine<TTransition, TCommand, TState>, TCommand>> commandHandlers,
		IEnumerable<TState> initialStates
	)
	{
		var stateMachine = new StateMachine<TTransition, TCommand, TState>(commandHandlers);
		foreach (var initialState in initialStates)
		{
			stateMachine.AddState(initialState);
		}
		return stateMachine;
	}

	internal void Handle<TFrom, TOn, TTo>(Func<TFrom, TOn, TTo> callback, TOn command)
		where TFrom : TTransition
		where TOn : TCommand, ICommand<TFrom, TTo>
		where TTo : TState
	{
		var lines = new List<string>();
		try
		{
			var previousState = States.Last();
			if (previousState is not TFrom from)
			{
				lines.Add($"Invalid transition from {previousState.GetType().Name} to {typeof(TTo).Name}.");
				return;
			}

			lines.Add($"Transitioning from {typeof(TFrom).Name} (Context: {from}).");

			var state = callback(from, command);
			AddState(state);

			lines.Add($"Transitioned to {typeof(TTo).Name} (Context: {state}).");

			StateChanged?.Invoke(this, new StateChangedEventArgs(State: state, PreviousState: previousState));
		}
		finally
		{
			Console.WriteLine(string.Join('\n', lines));
			Console.WriteLine();
		}
	}

	public void Send<TOn>(TOn command)
		where TOn : TCommand
	{
		_commandHandlers[typeof(TOn)](this, command);
	}
}
