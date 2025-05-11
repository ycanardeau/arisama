using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.DivorceCertificates.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct DivorceCertificateId
{
	public static DivorceCertificateId CreateVersion7()
	{
		return new(Guid.CreateVersion7());
	}
}
