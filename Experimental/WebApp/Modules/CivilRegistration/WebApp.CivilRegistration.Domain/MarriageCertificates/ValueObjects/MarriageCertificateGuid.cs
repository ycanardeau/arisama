using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct MarriageCertificateGuid
{
	public static MarriageCertificateGuid CreateVersion7()
	{
		return new(Guid.CreateVersion7());
	}
}
