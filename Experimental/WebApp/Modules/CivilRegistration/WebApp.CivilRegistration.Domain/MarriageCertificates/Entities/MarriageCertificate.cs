using DiscriminatedOnions;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using static WebApp.CivilRegistration.Domain.MarriageCertificates.Entities.MarriageCertificateCommand;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalCommand;

namespace WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;

internal class MarriageCertificate
{
	public MarriageCertificateId Id { get; set; }
	public PersonId Person1Id { get; set; }
	public required Person Person1 { get; set; }
	public PersonId Person2Id { get; set; }
	public required Person Person2 { get; set; }

	private MarriageCertificate() { }

	private Result<MarriageCertificate, InvalidOperationException> Marry()
	{
		return Person1.Marry(new MarryCommand(Person2))
			.Bind(x => Person2.Marry(new MarryCommand(Person1)))
			.Map(x => this);
	}

	public static Result<MarriageCertificate, InvalidOperationException> Create(CreateCommand command)
	{
		var marriageCertificate = new MarriageCertificate
		{
			Person1 = command.Person1,
			Person2 = command.Person2,
		};

		return marriageCertificate.Marry();
	}
}
