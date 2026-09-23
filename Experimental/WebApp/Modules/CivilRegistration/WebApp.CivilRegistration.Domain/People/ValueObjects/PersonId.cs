using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.People.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct PersonId
{
	public static PersonId CreateVersion7()
	{
		return new(Guid.CreateVersion7());
	}
}
