using WebApp.CivilRegistration.Domain.People.Entities;
using WebApp.CivilRegistration.Domain.People.ValueObjects;

namespace WebApp.CivilRegistration.Domain.People.Transitions;

internal interface ICanDecease : IMaritalTransition<DeceaseCommand, MaritalStatus.Deceased>
{
	Result<MaritalStatus.Deceased, CivilRegistrationError> IMaritalTransition<
		DeceaseCommand,
		MaritalStatus.Deceased
	>.Execute(MaritalStateMachine stateMachine, DeceaseCommand command)
	{
		return new MaritalStatus.Deceased(
			DeathInformation: new(
				DeathCertificateId: command.DeathCertificate.Id,
				DeceasedAtAge: stateMachine.Person.Age
			)
		);
	}
}
