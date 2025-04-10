using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.Persons.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct MaritalStatusId
{
	public MaritalStatusId()
	{
		Value = Guid.CreateVersion7();
	}
}
