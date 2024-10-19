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

	public StateMachineBuilder<TState, TCommand> ConfigureState<TFrom, TConcreteCommand, TTo>(Func<TFrom, TConcreteCommand, TTo> callback)
		where TFrom : TState
		where TConcreteCommand : TCommand
		where TTo : TState
	{
		AddCommandHandler<TConcreteCommand>((stateMachine, command) => stateMachine.Handle(callback, command));
		return this;
	}

	public StateMachine<TState, TCommand> Build<TInitialState>(TInitialState initialState)
		where TInitialState : TState
	{
		return StateMachine<TState, TCommand>.Create(_commandHandlers.ToImmutableDictionary(), initialState);
	}
}
