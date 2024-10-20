using System.Collections.Immutable;
using Microsoft.Extensions.Logging;

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
	private readonly ILogger<StateMachine<TTransition, TCommand, TState>> _logger;
	private readonly ImmutableDictionary<Type, Action<StateMachine<TTransition, TCommand, TState>, TCommand>> _commandHandlers;

	private readonly List<TState> _states = [];
	public IReadOnlyCollection<TState> States => _states.AsReadOnly();

	public sealed record StateChangedEventArgs(TState State, TState PreviousState);
	public delegate void StateChangedEventHandler(StateMachine<TTransition, TCommand, TState> sender, StateChangedEventArgs e);
	public event StateChangedEventHandler? StateChanged;

	private StateMachine(
		ILogger<StateMachine<TTransition, TCommand, TState>> logger,
		ImmutableDictionary<Type, Action<StateMachine<TTransition, TCommand, TState>, TCommand>> commandHandlers
	)
	{
		_logger = logger;
		_commandHandlers = commandHandlers;
	}

	private void AddState(TState state)
	{
		_states.Add(state);
	}

	internal static StateMachine<TTransition, TCommand, TState> Create(
		ILogger<StateMachine<TTransition, TCommand, TState>> logger,
		ImmutableDictionary<Type, Action<StateMachine<TTransition, TCommand, TState>, TCommand>> commandHandlers,
		IEnumerable<TState> initialStates
	)
	{
		var stateMachine = new StateMachine<TTransition, TCommand, TState>(logger, commandHandlers);
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
		var previousState = States.Last();
		if (previousState is not TFrom from)
		{
			_logger.LogError("Invalid transition from {} to {}", previousState.GetType().Name, typeof(TTo).Name);
			throw new InvalidOperationException($"Invalid transition from {previousState.GetType().Name} to {typeof(TTo).Name}");
		}

		_logger.LogInformation("Transitioning from {}", typeof(TFrom).Name);

		var state = callback(from, command);
		AddState(state);

		_logger.LogInformation("Transitioned to {}", typeof(TTo).Name);

		StateChanged?.Invoke(this, new StateChangedEventArgs(State: state, PreviousState: previousState));
	}

	public void Send<TOn>(TOn command)
		where TOn : TCommand
	{
		_commandHandlers[typeof(TOn)](this, command);
	}
}
