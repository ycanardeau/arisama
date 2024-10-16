namespace Arisama;

public interface IState;

public interface ICommand<TState>
	where TState : class, IState
{
	Task ExecuteAsync(StateMachine<TState> stateMachine);
}

public sealed class StateMachine<TState>
	where TState : class, IState
{
	private readonly List<TState> _states = [];
	public IReadOnlyCollection<TState> States => _states.AsReadOnly();

	public interface IStateMachineBuilderFrom<TFrom>
		where TFrom : class, TState
	{
		void To<TTo>(Func<TFrom, TTo> callback)
			where TTo : class, TState;
	}

	private sealed class StateMachineBuilderFrom<TFrom>(StateMachine<TState> stateMachine) : IStateMachineBuilderFrom<TFrom>
		where TFrom : class, TState
	{
		public void To<TTo>(Func<TFrom, TTo> callback)
			where TTo : class, TState
		{
			var latestState = stateMachine.States.Last();
			if (latestState is not TFrom from)
			{
				Console.WriteLine($"Invalid transition from {latestState.GetType().Name} to {typeof(TTo).Name}.");
				return;
			}

			Console.WriteLine($"Transitioning from {typeof(TFrom).Name} (Context: {from}).");

			var to = callback(from);
			stateMachine._states.Add(to);

			Console.WriteLine($"Transitioned to {typeof(TTo).Name} (Context: {to}).");
		}
	}

	private StateMachine() { }

	public static StateMachine<TState> Create<TInitialState>()
		where TInitialState : TState, new()
	{
		var stateMachine = new StateMachine<TState>();

		stateMachine._states.Add(new TInitialState());

		return stateMachine;
	}

	public IStateMachineBuilderFrom<TFrom> From<TFrom>()
		where TFrom : class, TState
	{
		return new StateMachineBuilderFrom<TFrom>(this);
	}

	public Task ExecuteAsync(ICommand<TState> command)
	{
		return command.ExecuteAsync(this);
	}
}
