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

	private readonly Dictionary<Type, Func<StateMachine<TState, TCommand>, TCommand, Task>> _commandHandlers = [];

	private interface IStateMachineBuilderFrom<TFrom>
		where TFrom : TState
	{
		void To<TTo>(Func<TFrom, TTo> callback)
			where TTo : TState;
	}

	private sealed class StateMachineBuilderFrom<TFrom>(StateMachine<TState, TCommand> stateMachine) : IStateMachineBuilderFrom<TFrom>
		where TFrom : TState
	{
		public void To<TTo>(Func<TFrom, TTo> callback)
			where TTo : TState
		{
			var lines = new List<string>();
			try
			{
				var latestState = stateMachine.States.Last();
				if (latestState is not TFrom from)
				{
					lines.Add($"Invalid transition from {latestState.GetType().Name} to {typeof(TTo).Name}.");
					return;
				}

				lines.Add($"Transitioning from {typeof(TFrom).Name} (Context: {from}).");

				var to = callback(from);
				stateMachine._states.Add(to);

				lines.Add($"Transitioned to {typeof(TTo).Name} (Context: {to}).");
			}
			finally
			{
				Console.WriteLine(string.Join('\n', lines));
				Console.WriteLine();
			}
		}
	}

	private StateMachine() { }

	public static StateMachine<TState, TCommand> Create<TInitialState>()
		where TInitialState : TState, new()
	{
		var stateMachine = new StateMachine<TState, TCommand>();

		stateMachine._states.Add(new TInitialState());

		return stateMachine;
	}

	private IStateMachineBuilderFrom<TFrom> From<TFrom>()
		where TFrom : TState
	{
		return new StateMachineBuilderFrom<TFrom>(this);
	}

	private StateMachine<TState, TCommand> AddCommandHandler<TConcreteCommand>(Func<StateMachine<TState, TCommand>, TConcreteCommand, Task> commandHandler)
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

	public StateMachine<TState, TCommand> ConfigureState<TFrom, TConcreteCommand, TTo>(Func<TFrom, TConcreteCommand, TTo> callback)
		where TFrom : TState
		where TConcreteCommand : TCommand
		where TTo : TState
	{
		AddCommandHandler<TConcreteCommand>((stateMachine, command) =>
		{
			stateMachine.From<TFrom>()
				.To(from => callback(from, command));
			return Task.CompletedTask;
		});
		return this;
	}

	public Task ExecuteAsync<TConcreteCommand>(TConcreteCommand command)
		where TConcreteCommand : TCommand
	{
		return _commandHandlers[typeof(TConcreteCommand)](this, command);
	}
}
