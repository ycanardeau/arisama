using System.Collections.Immutable;

namespace Aigamo.Arisama;

public interface IState;

public interface ICommand;

public sealed class StateMachine<TState, TCommand>
	where TState : IState
	where TCommand : ICommand
{
	private readonly List<TState> _states = [];
	public IReadOnlyCollection<TState> States => _states.AsReadOnly();

	private readonly ImmutableDictionary<Type, Action<StateMachine<TState, TCommand>, TCommand>> _commandHandlers;

	private StateMachine(ImmutableDictionary<Type, Action<StateMachine<TState, TCommand>, TCommand>> commandHandlers)
	{
		_commandHandlers = commandHandlers;
	}

	private void AddState(TState state)
	{
		_states.Add(state);
	}

	internal static StateMachine<TState, TCommand> Create<TInitialState>(
		ImmutableDictionary<Type, Action<StateMachine<TState, TCommand>, TCommand>> commandHandlers,
		TInitialState initialState
	)
		where TInitialState : TState
	{
		var stateMachine = new StateMachine<TState, TCommand>(commandHandlers);
		stateMachine.AddState(initialState);
		return stateMachine;
	}

	internal void Handle<TFrom, TConcreteCommand, TTo>(Func<TFrom, TConcreteCommand, TTo> callback, TConcreteCommand command)
		where TFrom : TState
		where TConcreteCommand : TCommand
		where TTo : TState
	{
		var lines = new List<string>();
		try
		{
			var latestState = States.Last();
			if (latestState is not TFrom from)
			{
				lines.Add($"Invalid transition from {latestState.GetType().Name} to {typeof(TTo).Name}.");
				return;
			}

			lines.Add($"Transitioning from {typeof(TFrom).Name} (Context: {from}).");

			var to = callback(from, command);
			AddState(to);

			lines.Add($"Transitioned to {typeof(TTo).Name} (Context: {to}).");
		}
		finally
		{
			Console.WriteLine(string.Join('\n', lines));
			Console.WriteLine();
		}
	}

	public void Send<TConcreteCommand>(TConcreteCommand command)
		where TConcreteCommand : TCommand
	{
		_commandHandlers[typeof(TConcreteCommand)](this, command);
	}
}
