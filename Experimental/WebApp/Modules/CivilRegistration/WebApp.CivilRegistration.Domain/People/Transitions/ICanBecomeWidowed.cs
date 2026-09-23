using WebApp.CivilRegistration.Domain.People.Entities;
using WebApp.CivilRegistration.Domain.People.ValueObjects;

namespace WebApp.CivilRegistration.Domain.People.Transitions;

internal interface ICanBecomeWidowed
	: IMaritalTransition<BecomeWidowedCommand, MaritalStatus.Widowed>,
		IHasMarriageInformation
{
	Result<MaritalStatus.Widowed, CivilRegistrationError> IMaritalTransition<
		BecomeWidowedCommand,
		MaritalStatus.Widowed
	>.Execute(MaritalStateMachine stateMachine, BecomeWidowedCommand command)
	{
		return new MaritalStatus.Widowed(
			MarriageInformation,
			WidowhoodInformation: new(
				WidowedAtAge: stateMachine.Person.Age,
				WidowedFromId: MarriageInformation.MarriedWithId
			)
		);
	}
}
