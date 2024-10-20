using System.Collections.Immutable;
using System.Diagnostics;

namespace Aigamo.Arisama;

public sealed class StateMachineBuilder<TTransition, TCommand, TState>
	where TTransition : ITransition
	where TCommand : ICommand
	where TState : IState
{
	private readonly Dictionary<Type, Action<StateMachine<TTransition, TCommand, TState>, TCommand>> _commandHandlers = [];

	private StateMachineBuilder<TTransition, TCommand, TState> AddCommandHandler<TOn>(Action<StateMachine<TTransition, TCommand, TState>, TOn> commandHandler)
		where TOn : TCommand
	{
		_commandHandlers.Add(typeof(TOn), (stateMachine, command) =>
		{
			if (command is not TOn concreteCommand)
			{
				throw new UnreachableException($"Invalid command type. Expected: {nameof(TOn)}, Actual: {command.GetType().Name}");
			}

			commandHandler(stateMachine, concreteCommand);
		});
		return this;
	}

	public StateMachineBuilder<TTransition, TCommand, TState> ConfigureState<TFrom, TOn, TTo>(Func<TFrom, TOn, TTo> callback)
		where TFrom : TTransition
		where TOn : TCommand, ICommand<TFrom, TTo>
		where TTo : TState
	{
		AddCommandHandler<TOn>((stateMachine, command) => stateMachine.Handle(callback, command));
		return this;
	}

	public StateMachine<TTransition, TCommand, TState> Build<TInitialState>(TInitialState initialState)
		where TInitialState : TState
	{
		return StateMachine<TTransition, TCommand, TState>.Create(_commandHandlers.ToImmutableDictionary(), initialState);
	}
}
