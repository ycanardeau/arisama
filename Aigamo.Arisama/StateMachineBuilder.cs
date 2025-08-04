using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Aigamo.Arisama;

public sealed class StateMachineBuilder<TTransition, TCommand, TState>(ILoggerFactory loggerFactory)
	where TTransition : ITransition
	where TCommand : ICommand
	where TState : IState
{
	public sealed class StateConfiguration
	{
		public required Func<StateMachine<TTransition, TCommand, TState>, TCommand, Task> CommandHandler { get; init; }
	}

	private readonly Dictionary<Type, StateConfiguration> _configurations = [];

	private StateMachineBuilder<TTransition, TCommand, TState> AddCommandHandler<TOn>(Func<StateMachine<TTransition, TCommand, TState>, TOn, Task> commandHandler)
		where TOn : TCommand
	{
		_configurations.Add(typeof(TOn), new StateConfiguration
		{
			CommandHandler = (stateMachine, command) =>
			{
				if (command is not TOn concreteCommand)
				{
					throw new UnreachableException($"Invalid command type. Expected: {nameof(TOn)}, Actual: {command.GetType().Name}");
				}

				return commandHandler(stateMachine, concreteCommand);
			},
		});
		return this;
	}

	private StateMachineBuilder<TTransition, TCommand, TState> AddTransition<TFrom, TOn, TTo>(Func<TFrom, TOn, TTo> callback)
		where TFrom : TTransition
		where TOn : TCommand, ICommand<TFrom, TTo>
		where TTo : TState
	{
		AddCommandHandler<TOn>((stateMachine, command) => stateMachine.HandleAsync(callback, command));
		return this;
	}

	public StateMachineBuilder<TTransition, TCommand, TState> AddTransition<TFrom, TOn, TTo>()
		where TFrom : TTransition
		where TOn : TCommand, ICommand<TFrom, TTo>
		where TTo : TState
	{
		return AddTransition<TFrom, TOn, TTo>((from, command) => command.Execute(from));
	}

	public StateMachine<TTransition, TCommand, TState> Build(IEnumerable<TState> initialStates, StateMachine<TTransition, TCommand, TState>.StateMachineOptions options)
	{
		return StateMachine<TTransition, TCommand, TState>.Create(
			loggerFactory.CreateLogger<StateMachine<TTransition, TCommand, TState>>(),
			_configurations.ToImmutableDictionary(),
			options,
			initialStates
		);
	}

	public StateMachine<TTransition, TCommand, TState> Build(IEnumerable<TState> initialStates)
	{
		return StateMachine<TTransition, TCommand, TState>.Create(
			loggerFactory.CreateLogger<StateMachine<TTransition, TCommand, TState>>(),
			_configurations.ToImmutableDictionary(),
			options: new StateMachine<TTransition, TCommand, TState>.StateMachineOptions(),
			initialStates
		);
	}

	public StateMachine<TTransition, TCommand, TState> Build(IEnumerable<TState> initialStates, Action<StateMachine<TTransition, TCommand, TState>.StateMachineOptions> configureOptions)
	{
		var options = new StateMachine<TTransition, TCommand, TState>.StateMachineOptions();
		configureOptions(options);
		return StateMachine<TTransition, TCommand, TState>.Create(
			loggerFactory.CreateLogger<StateMachine<TTransition, TCommand, TState>>(),
			_configurations.ToImmutableDictionary(),
			options,
			initialStates
		);
	}
}
