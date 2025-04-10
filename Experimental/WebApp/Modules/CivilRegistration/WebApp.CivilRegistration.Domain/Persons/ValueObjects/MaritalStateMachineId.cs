using StronglyTypedIds;

namespace WebApp.CivilRegistration.Domain.Persons.ValueObjects;

[StronglyTypedId(Template.Guid)]
internal readonly partial struct MaritalStateMachineId
{
	public MaritalStateMachineId()
	{
		Value = Guid.CreateVersion7();
	}
}
