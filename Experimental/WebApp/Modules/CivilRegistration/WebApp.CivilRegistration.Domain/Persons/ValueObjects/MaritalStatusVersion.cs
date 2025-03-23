using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.Persons.ValueObjects;

[StronglyTypedId(Template.Int)]
internal readonly partial struct MaritalStatusVersion
{
	public static MaritalStatusVersion operator ++(MaritalStatusVersion value) => new(value.Value + 1);
}
