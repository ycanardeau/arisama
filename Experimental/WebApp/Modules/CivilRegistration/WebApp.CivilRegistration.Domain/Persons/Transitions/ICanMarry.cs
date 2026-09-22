using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Transitions;

internal interface ICanMarry : IMaritalTransition<MarryCommand, MaritalStatus.Married>
{
	Result<MaritalStatus.Married> IMaritalTransition<MarryCommand, MaritalStatus.Married>.Execute(
		MaritalStateMachine stateMachine,
		MarryCommand command
	)
	{
		return !stateMachine.Person.CanMarryAtCurrentAge
			? Result.Error<MaritalStatus.Married>(
				new InvalidOperationException("Not of marriageable age")
			)
			: new MaritalStatus.Married(
				MarriageInformation: new(
					MarriageCertificateId: command.MarriageCertificate.Id,
					MarriedAtAge: stateMachine.Person.Age,
					MarriedWithId: command.MarryWith.Id
				)
			);
	}
}
