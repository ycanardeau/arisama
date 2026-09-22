using WebApp.CivilRegistration.Domain.Common.Entities;
using WebApp.CivilRegistration.Domain.Persons.Events;
using WebApp.CivilRegistration.Domain.Persons.Transitions;
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
		IncrementVersion();

		States.Add(nextState);

		AddDomainEvent(new MaritalStatusChangedDomainEvent(this, nextState));

		return nextState;
	}

	public static Result<MaritalStateMachine, CivilRegistrationError> Create()
	{
		var stateMachine = new MaritalStateMachine { Id = MaritalStateMachineId.CreateVersion7() };

		stateMachine.AddState(new MaritalStatus.Single());

		return stateMachine;
	}

	public MaritalStatus CurrentState => States.Last();

	private Result<TNextState, CivilRegistrationError> ExecuteIf<TTransition, TCommand, TNextState>(
		TCommand command
	)
		where TCommand : MaritalCommand
		where TNextState : MaritalStatus
		where TTransition : IMaritalTransition<TCommand, TNextState>
	{
		return CurrentState is not TTransition transition
			? new CivilRegistrationError.InvalidMaritalState()
			: transition.Execute(this, command).Map(AddState);
	}

	public Result<MaritalStatus.Married, CivilRegistrationError> Marry(MarryCommand command)
	{
		return ExecuteIf<ICanMarry, MarryCommand, MaritalStatus.Married>(command);
	}

	public Result<MaritalStatus.Divorced, CivilRegistrationError> Divorce(DivorceCommand command)
	{
		return ExecuteIf<ICanDivorce, DivorceCommand, MaritalStatus.Divorced>(command);
	}

	public Result<MaritalStatus.Widowed, CivilRegistrationError> BecomeWidowed(
		BecomeWidowedCommand command
	)
	{
		return ExecuteIf<ICanBecomeWidowed, BecomeWidowedCommand, MaritalStatus.Widowed>(command);
	}

	public Result<MaritalStatus.Deceased, CivilRegistrationError> Decease(DeceaseCommand command)
	{
		return ExecuteIf<ICanDecease, DeceaseCommand, MaritalStatus.Deceased>(command);
	}
}
