using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.DivorceCertificates.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct DivorceCertificateGuid
{
	public static DivorceCertificateGuid CreateVersion7()
	{
		return new(Guid.CreateVersion7());
	}
}
