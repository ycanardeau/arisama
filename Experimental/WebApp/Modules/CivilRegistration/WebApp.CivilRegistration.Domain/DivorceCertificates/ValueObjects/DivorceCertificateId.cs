using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.DivorceCertificates.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct DivorceCertificateId
{
	public DivorceCertificateId()
	{
		Value = Guid.CreateVersion7();
	}
}
