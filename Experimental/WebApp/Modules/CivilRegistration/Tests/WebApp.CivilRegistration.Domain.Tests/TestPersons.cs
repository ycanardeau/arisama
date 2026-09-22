namespace WebApp.CivilRegistration.Domain.Tests;

internal static class TestPersons
{
	public static Person Male(int age) => Create(age, new Gender.Male());

	public static Person Female(int age) => Create(age, new Gender.Female());

	private static Person Create(int age, Gender gender)
	{
		var person = Person.Create(new Age(age), gender).ValueOrThrow();

		// In production EF Core populates the state machine's back-reference to its owning
		// person when the aggregate is loaded; wire it here so it is usable in memory.
		person.MaritalStateMachine.Person = person;

		return person;
	}
}
