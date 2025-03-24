using DiscriminatedOnions;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalCommand;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal class Person
{
	public PersonId Id { get; set; }
	public required MaritalStateMachine MaritalStateMachine { get; set; }

	private Person() { }

	public static Result<Person, InvalidOperationException> Create()
	{
		var person = new Person
		{
			MaritalStateMachine = new(),
		};

		return person.MaritalStateMachine.Initialize()
			.Map(x => person);
	}

	public Result<Person, InvalidOperationException> Marry(MarryCommand command)
	{
		return MaritalStateMachine.Marry(command)
			.Map(x => this);
	}

	public Result<Person, InvalidOperationException> Divorce(DivorceCommand command)
	{
		return MaritalStateMachine.Divorce(command)
			.Map(x => this);
	}

	public Result<Person, InvalidOperationException> BecomeWidowed(BecomeWidowedCommand command)
	{
		return MaritalStateMachine.BecomeWidowed(command)
			.Map(x => this);
	}
}
