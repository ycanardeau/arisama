using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.DeathCertificates.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct DeathCertificateGuid
{
	public static DeathCertificateGuid CreateVersion7()
	{
		return new(Guid.CreateVersion7());
	}
}
