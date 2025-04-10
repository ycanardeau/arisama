using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.MarriageCertificates.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct MarriageCertificateId
{
	public MarriageCertificateId()
	{
		Value = Guid.CreateVersion7();
	}
}
