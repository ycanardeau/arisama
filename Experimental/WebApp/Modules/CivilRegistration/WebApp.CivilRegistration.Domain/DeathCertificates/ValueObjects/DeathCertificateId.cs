using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.DeathCertificates.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct DeathCertificateId
{
	public DeathCertificateId()
	{
		Value = Guid.CreateVersion7();
	}
}
