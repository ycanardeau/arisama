using Nut.Results;
using WebApp.CivilRegistration.Domain.Common.Entities;
using WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;
using WebApp.CivilRegistration.Domain.Persons.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;

internal class MarriageCertificate : Entity<MarriageCertificateId>
{
	public PersonId HusbandId { get; set; }
	public required Person Husband { get; set; }
	public PersonId WifeId { get; set; }
	public required Person Wife { get; set; }

	private MarriageCertificate() { }

	private Result<MarriageCertificate> Marry()
	{
		return Husband == Wife
			? Result.Error<MarriageCertificate>(new InvalidOperationException("A marriage requires two individuals"))
			: Husband.Gender == Wife.Gender
			? Result.Error<MarriageCertificate>(new InvalidOperationException("Same-sex marriage is not allowed in Japan as of writing"))
			: !Husband.CanBeHusband
			? Result.Error<MarriageCertificate>(new InvalidOperationException($"Person {Husband.Id} cannot be a husband"))
			: !Wife.CanBeWife
			? Result.Error<MarriageCertificate>(new InvalidOperationException($"Person {Wife.Id} cannot be a wife"))
			: Husband.Marry(new MarryCommand(this, Wife))
				.Map(x => Wife.Marry(new MarryCommand(this, Husband)))
				.Map(x => this);
	}

	public static Result<MarriageCertificate> Create(CreateCommand command)
	{
		var marriageCertificate = new MarriageCertificate
		{
			Id = MarriageCertificateId.CreateVersion7(),
			Husband = command.Husband,
			Wife = command.Wife,
		};

		return marriageCertificate.Marry();
	}
}
