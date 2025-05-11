using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.DeathCertificates.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct DeathCertificateId
{
	public static DeathCertificateId CreateVersion7()
	{
		return new(Guid.CreateVersion7());
	}
}
