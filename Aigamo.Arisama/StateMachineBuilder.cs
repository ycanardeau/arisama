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
		public required Action<StateMachine<TTransition, TCommand, TState>, TCommand> CommandHandler { get; init; }
	}

	private readonly Dictionary<Type, StateConfiguration> _configurations = [];

	private StateMachineBuilder<TTransition, TCommand, TState> AddCommandHandler<TOn>(Action<StateMachine<TTransition, TCommand, TState>, TOn> commandHandler)
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

				commandHandler(stateMachine, concreteCommand);
			},
		});
		return this;
	}

	private StateMachineBuilder<TTransition, TCommand, TState> ConfigureState<TFrom, TOn, TTo>(Func<TFrom, TOn, TTo> callback)
		where TFrom : TTransition
		where TOn : TCommand, ICommand<TFrom, TTo>
		where TTo : TState
	{
		AddCommandHandler<TOn>((stateMachine, command) => stateMachine.Handle(callback, command));
		return this;
	}

	public StateMachineBuilder<TTransition, TCommand, TState> ConfigureState<TFrom, TOn, TTo>()
		where TFrom : TTransition
		where TOn : TCommand, ICommand<TFrom, TTo>
		where TTo : TState
	{
		return ConfigureState<TFrom, TOn, TTo>((from, command) => command.Execute(from));
	}

	public sealed class StateMachineBuilderTo<TFrom, TTo>(StateMachineBuilder<TTransition, TCommand, TState> builder)
		where TFrom : TTransition
		where TTo : TState
	{
		public StateMachineBuilder<TTransition, TCommand, TState> On<TOn>()
			where TOn : TCommand, ICommand<TFrom, TTo>
		{
			builder.ConfigureState<TFrom, TOn, TTo>();
			return builder;
		}
	}

	public sealed class StateMachineBuilderFrom<TFrom>(StateMachineBuilder<TTransition, TCommand, TState> builder)
		where TFrom : TTransition
	{
		public StateMachineBuilderTo<TFrom, TTo> To<TTo>()
			where TTo : TState
		{
			return new StateMachineBuilderTo<TFrom, TTo>(builder);
		}
	}

	public StateMachineBuilderFrom<TFrom> From<TFrom>()
		where TFrom : TTransition
	{
		return new StateMachineBuilderFrom<TFrom>(this);
	}

	public StateMachine<TTransition, TCommand, TState> Build(IEnumerable<TState> initialStates)
	{
		return StateMachine<TTransition, TCommand, TState>.Create(
			loggerFactory.CreateLogger<StateMachine<TTransition, TCommand, TState>>(),
			_configurations.ToImmutableDictionary(),
			initialStates
		);
	}
}
