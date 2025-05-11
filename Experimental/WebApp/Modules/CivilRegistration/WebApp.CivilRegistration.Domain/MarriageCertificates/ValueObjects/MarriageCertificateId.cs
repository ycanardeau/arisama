using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct MarriageCertificateId
{
	public static MarriageCertificateId CreateVersion7()
	{
		return new(Guid.CreateVersion7());
	}
}
