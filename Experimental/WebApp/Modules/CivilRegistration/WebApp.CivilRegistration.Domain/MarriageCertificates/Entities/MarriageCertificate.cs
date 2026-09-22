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

	private Result<MarriageCertificate, WebAppError> Marry()
	{
		return Husband == Wife
				? UnprocessableEntity<MarriageCertificate>( /* "A marriage requires two individuals" */
				)
			: Husband.Gender == Wife.Gender
				? UnprocessableEntity<MarriageCertificate>( /* "Same-sex marriage is not allowed in Japan as of writing" */
				)
			: !Husband.CanBeHusband
				? UnprocessableEntity<MarriageCertificate>( /* $"Person {Husband.Id} cannot be a husband" */
				)
			: !Wife.CanBeWife
				? UnprocessableEntity<MarriageCertificate>( /* $"Person {Wife.Id} cannot be a wife" */
				)
			: Husband
				.Marry(new MarryCommand(this, Wife))
				.Map(x => Wife.Marry(new MarryCommand(this, Husband)))
				.Map(x => this);
	}

	public static Result<MarriageCertificate, WebAppError> Create(CreateCommand command)
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
