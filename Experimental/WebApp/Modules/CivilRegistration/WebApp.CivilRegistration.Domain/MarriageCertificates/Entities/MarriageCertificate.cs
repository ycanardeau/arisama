using DiscriminatedOnions;
using WebApp.CivilRegistration.Domain.Common.Entities;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;

internal class MarriageCertificate : Entity<MarriageCertificateId>
{
	public required MarriageCertificateGuid Guid { get; set; }
	public PersonId Person1Id { get; set; }
	public required Person Person1 { get; set; }
	public PersonId Person2Id { get; set; }
	public required Person Person2 { get; set; }

	private MarriageCertificate() { }

	private Result<MarriageCertificate, InvalidOperationException> Marry()
	{
		return Person1 == Person2
			? Result.Error(new InvalidOperationException("A marriage requires two individuals"))
			: Person1.Gender == Person2.Gender
			? Result.Error(new InvalidOperationException("Same-sex marriage is not allowed in Japan as of writing"))
			: Person1.Marry(new MarryCommand(this, Person2))
				.Map(x => Person2.Marry(new MarryCommand(this, Person1)))
				.Map(x => this);
	}

	public static Result<MarriageCertificate, InvalidOperationException> Create(CreateCommand command)
	{
		var marriageCertificate = new MarriageCertificate
		{
			Guid = MarriageCertificateGuid.CreateVersion7(),
			Person1 = command.Person1,
			Person2 = command.Person2,
		};

		return marriageCertificate.Marry();
	}
}
