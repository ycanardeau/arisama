using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.Persons.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct PersonId
{
	public PersonId()
	{
		Value = Guid.CreateVersion7();
	}
}
