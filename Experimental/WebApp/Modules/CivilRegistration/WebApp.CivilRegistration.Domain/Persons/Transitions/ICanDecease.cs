using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Transitions;

internal interface ICanDecease : IMaritalTransition<DeceaseCommand, MaritalStatus.Deceased>
{
	Result<MaritalStatus.Deceased> IMaritalTransition<
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
