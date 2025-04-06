using DiscriminatedOnions;
using WebApp.CivilRegistration.Domain.DeathCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.DeathCertificates.Entities;

internal class DeathCertificate
{
	public DeathCertificateId Id { get; set; }
	public PersonId DeceasedId { get; set; }
	public required Person Deceased { get; set; }

	private DeathCertificate() { }

	private Result<DeathCertificate, InvalidOperationException> Decease()
	{
		return Deceased.Decease(new DeceaseCommand())
			.Map(x => this);
	}

	public static Result<DeathCertificate, InvalidOperationException> Create(CreateCommand command)
	{
		var deathCertificate = new DeathCertificate
		{
			Deceased = command.Deceased,
		};

		return deathCertificate.Decease();
	}
}
