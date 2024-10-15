namespace Arisama;

public interface IState;

public interface ICommand;

public sealed class StateMachine<TState, TCommand>
	where TState : class, IState
	where TCommand : class, ICommand
{
	private readonly List<TState> _states = [];
	public IReadOnlyCollection<TState> States => _states.AsReadOnly();

	public interface IStateMachineBuilderOn<TFrom, TOn>
		where TOn : class, TCommand
	{
		void To<TTo>(Func<TFrom, TOn, TTo> callback)
			where TTo : class, TState;
	}

	private sealed class StateMachineBuilderOn<TFrom, TOn>(StateMachine<TState, TCommand> stateMachine) : IStateMachineBuilderOn<TFrom, TOn>
		where TFrom : class, TState
		where TOn : class, TCommand
	{
		public void To<TTo>(Func<TFrom, TOn, TTo> callback)
			where TTo : class, TState
		{
			throw new NotImplementedException();
		}
	}

	public interface IStateMachineBuilderFrom<TFrom>
		where TFrom : class, TState
	{
		IStateMachineBuilderOn<TFrom, TOn> On<TOn>()
			where TOn : class, TCommand;
	}

	private sealed class StateMachineBuilderFrom<TFrom>(StateMachine<TState, TCommand> stateMachine) : IStateMachineBuilderFrom<TFrom>
		where TFrom : class, TState
	{
		public IStateMachineBuilderOn<TFrom, TOn> On<TOn>()
			where TOn : class, TCommand
		{
			return new StateMachineBuilderOn<TFrom, TOn>(stateMachine);
		}
	}

	private StateMachine() { }

	public static StateMachine<TState, TCommand> Create<TInitialState>()
		where TInitialState : class, TState, new()
	{
		var stateMachine = new StateMachine<TState, TCommand>();

		stateMachine._states.Add(new TInitialState());

		return stateMachine;
	}

	public IStateMachineBuilderFrom<TFrom> From<TFrom>()
		where TFrom : class, TState
	{
		return new StateMachineBuilderFrom<TFrom>(this);
	}

	public Task ExecuteAsync(TCommand command)
	{
		throw new NotImplementedException();
	}
}
