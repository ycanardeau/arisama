using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.Persons.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct MaritalStatusId
{
	public static MaritalStatusId CreateVersion7()
	{
		return new(Guid.CreateVersion7());
	}
}
