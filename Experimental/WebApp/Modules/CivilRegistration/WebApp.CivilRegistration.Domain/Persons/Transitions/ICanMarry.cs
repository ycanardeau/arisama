using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Transitions;

internal interface ICanMarry : IMaritalTransition<MarryCommand, MaritalStatus.Married>
{
	Result<MaritalStatus.Married, CivilRegistrationError> IMaritalTransition<
		MarryCommand,
		MaritalStatus.Married
	>.Execute(MaritalStateMachine stateMachine, MarryCommand command)
	{
		return !stateMachine.Person.CanMarryAtCurrentAge
			? new CivilRegistrationError.NotMarriageable()
			: new MaritalStatus.Married(
				MarriageInformation: new(
					MarriageCertificateId: command.MarriageCertificate.Id,
					MarriedAtAge: stateMachine.Person.Age,
					MarriedWithId: command.MarryWith.Id
				)
			);
	}
}
