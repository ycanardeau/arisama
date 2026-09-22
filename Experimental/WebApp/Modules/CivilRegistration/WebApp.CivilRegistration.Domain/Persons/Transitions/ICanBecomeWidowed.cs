using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Transitions;

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
