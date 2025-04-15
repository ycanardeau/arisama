using DiscriminatedOnions;
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

	public static Result<MaritalStateMachine, InvalidOperationException> Create()
	{
		var stateMachine = new MaritalStateMachine();

		stateMachine.AddState(new SingleState
		{
			Payload = new(),
		});

		return Result.Ok(stateMachine);
	}

	public MaritalStatus CurrentState => States.MaxBy(x => x.Version) ?? throw new InvalidOperationException("Sequence contains no elements");

	private Result<TNextState, InvalidOperationException> ExecuteIf<TTransition, TCommand, TNextState>(TCommand command)
		where TCommand : MaritalCommand
		where TNextState : MaritalStatus
		where TTransition : IMaritalTransition<TCommand, TNextState>
	{
		return CurrentState is not TTransition transition
			? Result.Error(new InvalidOperationException($"{nameof(CurrentState)} is not {typeof(TTransition).Name}"))
			: transition.Execute(this, command)
				.Map(AddState);
	}

	public Result<MarriedState, InvalidOperationException> Marry(MarryCommand command)
	{
		return ExecuteIf<ICanMarry, MarryCommand, MarriedState>(command);
	}

	public Result<DivorcedState, InvalidOperationException> Divorce(DivorceCommand command)
	{
		return ExecuteIf<ICanDivorce, DivorceCommand, DivorcedState>(command);
	}

	public Result<WidowedState, InvalidOperationException> BecomeWidowed(BecomeWidowedCommand command)
	{
		return ExecuteIf<ICanBecomeWidowed, BecomeWidowedCommand, WidowedState>(command);
	}

	public Result<DeceasedState, InvalidOperationException> Decease(DeceaseCommand command)
	{
		return ExecuteIf<ICanDecease, DeceaseCommand, DeceasedState>(command);
	}
}
