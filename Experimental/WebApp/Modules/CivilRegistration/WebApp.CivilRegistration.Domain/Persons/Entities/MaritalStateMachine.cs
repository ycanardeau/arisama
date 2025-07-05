using Nut.Results;
using WebApp.CivilRegistration.Domain.Common.Entities;
using WebApp.CivilRegistration.Domain.Persons.Events;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal class MaritalStateMachine : Entity<MaritalStateMachineId>
{
	public PersonId PersonId { get; set; }
	public Person Person { get; set; } = default!;
	public MaritalStatusVersion Version { get; set; }

	public ICollection<MaritalStatus> States { get; set; } = [];

	private MaritalStateMachine() { }

	private MaritalStatusVersion IncrementVersion()
	{
		Version++;
		return Version;
	}

	private TNextState AddState<TNextState>(TNextState nextState)
		where TNextState : MaritalStatus
	{
		nextState.Version = IncrementVersion();

		States.Add(nextState);

		AddDomainEvent(new MaritalStatusChangedDomainEvent(this, nextState));

		return nextState;
	}

	public static Result<MaritalStateMachine> Create()
	{
		var stateMachine = new MaritalStateMachine
		{
			Id = MaritalStateMachineId.CreateVersion7(),
		};

		stateMachine.AddState(new Single
		{
			Id = MaritalStatusId.CreateVersion7(),
			Payload = new(),
		});

		return stateMachine;
	}

	public MaritalStatus CurrentState => States.MaxBy(x => x.Version) ?? throw new InvalidOperationException("Sequence contains no elements");

	private Result<TNextState> ExecuteIf<TTransition, TCommand, TNextState>(TCommand command)
		where TCommand : MaritalCommand
		where TNextState : MaritalStatus
		where TTransition : IMaritalTransition<TCommand, TNextState>
	{
		return CurrentState is not TTransition transition
			? Result.Error<TNextState>(new InvalidOperationException($"{nameof(CurrentState)} is not {typeof(TTransition).Name}"))
			: transition.Execute(this, command)
				.Map(AddState);
	}

	public Result<Married> Marry(MarryCommand command)
	{
		return ExecuteIf<ICanMarry, MarryCommand, Married>(command);
	}

	public Result<Divorced> Divorce(DivorceCommand command)
	{
		return ExecuteIf<ICanDivorce, DivorceCommand, Divorced>(command);
	}

	public Result<Widowed> BecomeWidowed(BecomeWidowedCommand command)
	{
		return ExecuteIf<ICanBecomeWidowed, BecomeWidowedCommand, Widowed>(command);
	}

	public Result<Deceased> Decease(DeceaseCommand command)
	{
		return ExecuteIf<ICanDecease, DeceaseCommand, Deceased>(command);
	}
}
