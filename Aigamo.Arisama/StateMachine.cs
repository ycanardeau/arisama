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
	public sealed record StateChangingContext(
		StateMachine<TTransition, TCommand, TState> StateMachine,
		TState State,
		TState PreviousState
	);

	public sealed record StateChangedContext(
		StateMachine<TTransition, TCommand, TState> StateMachine,
		TState State,
		TState PreviousState
	);

	public sealed class StateMachineOptions
	{
		public Func<StateChangingContext, Task> StateChanging { get; set; } = context => Task.CompletedTask;
		public Func<StateChangedContext, Task> StateChanged { get; set; } = context => Task.CompletedTask;
	}

	private readonly ILogger<StateMachine<TTransition, TCommand, TState>> _logger;
	private readonly ImmutableDictionary<Type, StateMachineBuilder<TTransition, TCommand, TState>.StateConfiguration> _configurations;
	private readonly StateMachineOptions _options;

	private readonly List<TState> _states = [];
	public IReadOnlyCollection<TState> States => _states.AsReadOnly();

	private StateMachine(
		ILogger<StateMachine<TTransition, TCommand, TState>> logger,
		ImmutableDictionary<Type, StateMachineBuilder<TTransition, TCommand, TState>.StateConfiguration> configurations,
		StateMachineOptions options
	)
	{
		_logger = logger;
		_configurations = configurations;
		_options = options;
	}

	private void AddState(TState state)
	{
		_states.Add(state);
	}

	internal static StateMachine<TTransition, TCommand, TState> Create(
		ILogger<StateMachine<TTransition, TCommand, TState>> logger,
		ImmutableDictionary<Type, StateMachineBuilder<TTransition, TCommand, TState>.StateConfiguration> configurations,
		StateMachineOptions options,
		IEnumerable<TState> initialStates
	)
	{
		var stateMachine = new StateMachine<TTransition, TCommand, TState>(logger, configurations, options);
		foreach (var initialState in initialStates)
		{
			stateMachine.AddState(initialState);
		}
		return stateMachine;
	}

	internal async Task HandleAsync<TFrom, TOn, TTo>(Func<TFrom, TOn, TTo> callback, TOn command)
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

		var state = callback(from, command);

		_logger.LogInformation("Transitioning from {}", typeof(TFrom).Name);

		await _options.StateChanging(new StateChangingContext(StateMachine: this, State: state, PreviousState: previousState));

		AddState(state);

		_logger.LogInformation("Transitioned to {}", typeof(TTo).Name);

		await _options.StateChanged(new StateChangedContext(StateMachine: this, State: state, PreviousState: previousState));
	}

	public Task SendAsync<TOn>(TOn command)
		where TOn : TCommand
	{
		return _configurations[typeof(TOn)].CommandHandler(this, command);
	}
}
