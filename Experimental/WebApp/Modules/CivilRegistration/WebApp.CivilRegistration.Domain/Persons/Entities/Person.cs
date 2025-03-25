using DiscriminatedOnions;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;
using static WebApp.CivilRegistration.Domain.Persons.Entities.MaritalCommand;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal class Person
{
	public PersonId Id { get; set; }
	public required MaritalStateMachine MaritalStateMachine { get; set; }

	private Person() { }

	private sealed record CreatePersonContext
	{
		public MaritalStateMachine MaritalStateMachine { get; init; } = default!;
	}

	private static Result<CreatePersonContext, InvalidOperationException> CreateMaritalStateMachine(CreatePersonContext context)
	{
		return MaritalStateMachine.Create()
			.Map(x => context with
			{
				MaritalStateMachine = x,
			});
	}

	public static Result<Person, InvalidOperationException> Create()
	{
		return Result.Ok<CreatePersonContext, InvalidOperationException>(new CreatePersonContext())
			.Bind(CreateMaritalStateMachine)
			.Map(x => new Person
			{
				MaritalStateMachine = x.MaritalStateMachine,
			});
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
