using System.Collections.Immutable;
using System.Diagnostics;

namespace Aigamo.Arisama;

public sealed class StateMachineBuilder<TTransition, TCommand, TState>
	where TTransition : ITransition
	where TCommand : ICommand
	where TState : IState
{
	private readonly Dictionary<Type, Action<StateMachine<TTransition, TCommand, TState>, TCommand>> _commandHandlers = [];

	private StateMachineBuilder<TTransition, TCommand, TState> AddCommandHandler<TConcreteCommand>(Action<StateMachine<TTransition, TCommand, TState>, TConcreteCommand> commandHandler)
		where TConcreteCommand : TCommand
	{
		_commandHandlers.Add(typeof(TConcreteCommand), (stateMachine, command) =>
		{
			if (command is not TConcreteCommand concreteCommand)
			{
				throw new UnreachableException($"Invalid command type. Expected: {nameof(TConcreteCommand)}, Actual: {command.GetType().Name}");
			}

			commandHandler(stateMachine, concreteCommand);
		});
		return this;
	}

	private StateMachineBuilder<TTransition, TCommand, TState> ConfigureState<TFrom, TConcreteCommand, TTo>(Func<TFrom, TConcreteCommand, TTo> callback)
		where TFrom : TTransition
		where TConcreteCommand : TCommand
		where TTo : TState
	{
		AddCommandHandler<TConcreteCommand>((stateMachine, command) => stateMachine.Handle(callback, command));
		return this;
	}

	public sealed class StateMachineBuilderOn<TFrom, TOn>(StateMachineBuilder<TTransition, TCommand, TState> builder)
		where TFrom : TTransition
		where TOn : TCommand
	{
		public StateMachineBuilder<TTransition, TCommand, TState> To<TTo>(Func<TFrom, TOn, TTo> callback)
			where TTo : TState
		{
			builder.ConfigureState(callback);
			return builder;
		}
	}

	public sealed class StateMachineBuilderFrom<TFrom>(StateMachineBuilder<TTransition, TCommand, TState> builder)
		where TFrom : TTransition
	{
		public StateMachineBuilderOn<TFrom, TOn> On<TOn>()
			where TOn : TCommand
		{
			return new StateMachineBuilderOn<TFrom, TOn>(builder);
		}
	}

	public StateMachineBuilderFrom<TFrom> From<TFrom>()
		where TFrom : TTransition
	{
		return new StateMachineBuilderFrom<TFrom>(this);
	}

	public StateMachine<TTransition, TCommand, TState> Build<TInitialState>(TInitialState initialState)
		where TInitialState : TState
	{
		return StateMachine<TTransition, TCommand, TState>.Create(_commandHandlers.ToImmutableDictionary(), initialState);
	}
}
