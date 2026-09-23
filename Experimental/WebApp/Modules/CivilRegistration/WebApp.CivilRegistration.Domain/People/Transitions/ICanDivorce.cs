using WebApp.CivilRegistration.Domain.People.Entities;
using WebApp.CivilRegistration.Domain.People.ValueObjects;

namespace WebApp.CivilRegistration.Domain.People.Transitions;

internal interface ICanDivorce
	: IMaritalTransition<DivorceCommand, MaritalStatus.Divorced>,
		IHasMarriageInformation
{
	Result<MaritalStatus.Divorced, CivilRegistrationError> IMaritalTransition<
		DivorceCommand,
		MaritalStatus.Divorced
	>.Execute(MaritalStateMachine stateMachine, DivorceCommand command)
	{
		return new MaritalStatus.Divorced(
			MarriageInformation,
			DivorceInformation: new(
				DivorceCertificateId: command.DivorceCertificate.Id,
				DivorcedAtAge: stateMachine.Person.Age,
				DivorcedFromId: MarriageInformation.MarriedWithId
			)
		);
	}
}
