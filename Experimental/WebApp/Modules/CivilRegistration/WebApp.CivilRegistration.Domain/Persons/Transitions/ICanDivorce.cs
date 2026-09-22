using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Transitions;

internal interface ICanDivorce
	: IMaritalTransition<DivorceCommand, MaritalStatus.Divorced>,
		IHasMarriageInformation
{
	Result<MaritalStatus.Divorced> IMaritalTransition<
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
