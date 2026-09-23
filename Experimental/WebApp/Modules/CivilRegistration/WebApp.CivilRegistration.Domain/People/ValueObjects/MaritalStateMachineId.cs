using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.People.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct MaritalStateMachineId
{
	public static MaritalStateMachineId CreateVersion7()
	{
		return new(Guid.CreateVersion7());
	}
}
