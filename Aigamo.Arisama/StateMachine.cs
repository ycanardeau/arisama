using System.Collections.Immutable;
using System.Diagnostics;

namespace Aigamo.Arisama;

public interface IState;

public interface ICommand;

public sealed class StateMachine<TState, TCommand>
	where TState : IState
	where TCommand : ICommand
{
	private readonly List<TState> _states = [];
	public IReadOnlyCollection<TState> States => _states.AsReadOnly();

	private readonly ImmutableDictionary<Type, Func<StateMachine<TState, TCommand>, TCommand, Task>> _commandHandlers;

	private StateMachine(ImmutableDictionary<Type, Func<StateMachine<TState, TCommand>, TCommand, Task>> commandHandlers)
	{
		_commandHandlers = commandHandlers;
	}

	internal void AddState(TState state)
	{
		_states.Add(state);
	}

	internal static StateMachine<TState, TCommand> Create<TInitialState>(
		ImmutableDictionary<Type, Func<StateMachine<TState, TCommand>, TCommand, Task>> commandHandlers,
		TInitialState initialState
	)
		where TInitialState : TState
	{
		var stateMachine = new StateMachine<TState, TCommand>(commandHandlers);
		stateMachine.AddState(initialState);
		return stateMachine;
	}

	public Task ExecuteAsync<TConcreteCommand>(TConcreteCommand command)
		where TConcreteCommand : TCommand
	{
		return _commandHandlers[typeof(TConcreteCommand)](this, command);
	}
}

public sealed class StateMachineBuilder<TState, TCommand>
	where TState : IState
	where TCommand : ICommand
{
	private readonly Dictionary<Type, Func<StateMachine<TState, TCommand>, TCommand, Task>> _commandHandlers = [];

	private StateMachineBuilder<TState, TCommand> AddCommandHandler<TConcreteCommand>(Func<StateMachine<TState, TCommand>, TConcreteCommand, Task> commandHandler)
		where TConcreteCommand : TCommand
	{
		_commandHandlers.Add(typeof(TConcreteCommand), (stateMachine, command) =>
		{
			if (command is not TConcreteCommand concreteCommand)
			{
				throw new UnreachableException($"Invalid command type. Expected: {nameof(TConcreteCommand)}, Actual: {command.GetType().Name}");
			}

			return commandHandler(stateMachine, concreteCommand);
		});
		return this;
	}

	public StateMachineBuilder<TState, TCommand> ConfigureState<TFrom, TConcreteCommand, TTo>(Func<TFrom, TConcreteCommand, TTo> callback)
		where TFrom : TState
		where TConcreteCommand : TCommand
		where TTo : TState
	{
		AddCommandHandler<TConcreteCommand>((stateMachine, command) =>
		{
			var lines = new List<string>();
			try
			{
				var latestState = stateMachine.States.Last();
				if (latestState is not TFrom from)
				{
					lines.Add($"Invalid transition from {latestState.GetType().Name} to {typeof(TTo).Name}.");
					return Task.CompletedTask;
				}

				lines.Add($"Transitioning from {typeof(TFrom).Name} (Context: {from}).");

				var to = callback(from, command);
				stateMachine.AddState(to);

				lines.Add($"Transitioned to {typeof(TTo).Name} (Context: {to}).");
			}
			finally
			{
				Console.WriteLine(string.Join('\n', lines));
				Console.WriteLine();
			}

			return Task.CompletedTask;
		});
		return this;
	}

	public StateMachine<TState, TCommand> Build<TInitialState>(TInitialState initialState)
		where TInitialState : TState
	{
		return StateMachine<TState, TCommand>.Create(_commandHandlers.ToImmutableDictionary(), initialState);
	}
}
