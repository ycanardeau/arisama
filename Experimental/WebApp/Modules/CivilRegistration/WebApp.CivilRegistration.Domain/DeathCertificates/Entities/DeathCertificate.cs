using WebApp.CivilRegistration.Domain.Common.Entities;
using WebApp.CivilRegistration.Domain.DeathCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.People.Entities;
using WebApp.CivilRegistration.Domain.People.ValueObjects;

namespace WebApp.CivilRegistration.Domain.DeathCertificates.Entities;

internal class DeathCertificate : Entity<DeathCertificateId>
{
	public PersonId DeceasedId { get; set; }
	public required Person Deceased { get; set; }
	public PersonId? WidowedId { get; set; }
	public required Person? Widowed { get; set; }

	private DeathCertificate() { }

	private Result<DeathCertificate, CivilRegistrationError> Decease()
	{
		return Deceased
			.Decease(new DeceaseCommand(this))
			.Map(x => Widowed?.BecomeWidowed(new BecomeWidowedCommand()))
			.Map(x => this);
	}

	public static Result<DeathCertificate, CivilRegistrationError> Create(CreateCommand command)
	{
		var deathCertificate = new DeathCertificate
		{
			Id = DeathCertificateId.CreateVersion7(),
			Deceased = command.Deceased,
			Widowed = command.Widowed,
		};

		return deathCertificate.Decease();
	}
}
