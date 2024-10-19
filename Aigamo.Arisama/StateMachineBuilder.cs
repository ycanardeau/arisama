using System.Collections.Immutable;
using System.Diagnostics;

namespace Aigamo.Arisama;

public sealed class StateMachineBuilder<TState, TCommand>
	where TState : IState
	where TCommand : ICommand
{
	private readonly Dictionary<Type, Action<StateMachine<TState, TCommand>, TCommand>> _commandHandlers = [];

	private StateMachineBuilder<TState, TCommand> AddCommandHandler<TConcreteCommand>(Action<StateMachine<TState, TCommand>, TConcreteCommand> commandHandler)
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

	private StateMachineBuilder<TState, TCommand> ConfigureState<TFrom, TConcreteCommand, TTo>(Func<TFrom, TConcreteCommand, TTo> callback)
		where TFrom : TState
		where TConcreteCommand : TCommand
		where TTo : TState
	{
		AddCommandHandler<TConcreteCommand>((stateMachine, command) => stateMachine.Handle(callback, command));
		return this;
	}

	public sealed class StateMachineBuilderOn<TFrom, TOn>(StateMachineBuilder<TState, TCommand> builder)
		where TFrom : TState
		where TOn : TCommand
	{
		public StateMachineBuilder<TState, TCommand> To<TTo>(Func<TFrom, TOn, TTo> callback)
			where TTo : TState
		{
			builder.ConfigureState(callback);
			return builder;
		}
	}

	public sealed class StateMachineBuilderFrom<TFrom>(StateMachineBuilder<TState, TCommand> builder)
		where TFrom : TState
	{
		public StateMachineBuilderOn<TFrom, TOn> On<TOn>()
			where TOn : TCommand
		{
			return new StateMachineBuilderOn<TFrom, TOn>(builder);
		}
	}

	public StateMachineBuilderFrom<TFrom> From<TFrom>()
		where TFrom : TState
	{
		return new StateMachineBuilderFrom<TFrom>(this);
	}

	public StateMachine<TState, TCommand> Build<TInitialState>(TInitialState initialState)
		where TInitialState : TState
	{
		return StateMachine<TState, TCommand>.Create(_commandHandlers.ToImmutableDictionary(), initialState);
	}
}
