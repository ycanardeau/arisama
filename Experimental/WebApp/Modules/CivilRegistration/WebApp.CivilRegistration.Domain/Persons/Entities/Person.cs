using DiscriminatedOnions;
using WebApp.CivilRegistration.Domain.Common.Entities;
using WebApp.CivilRegistration.Domain.Persons.ValueObjects;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal class Person : Entity<PersonId>
{
	public required Gender Gender { get; set; }
	public required Age Age { get; set; }
	public required MaritalStateMachine MaritalStateMachine { get; set; }

	private Person() { }

	public bool CanBeHusband => Gender.CanBeHusband;

	public bool CanBeWife => Gender.CanBeWife;

	public bool CanMarryAtCurrentAge => Gender.CanMarryAtAge(Age);

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

	public static Result<Person, InvalidOperationException> Create(Age age, Gender gender)
	{
		return Result.Ok<CreatePersonContext, InvalidOperationException>(new CreatePersonContext())
			.Bind(CreateMaritalStateMachine)
			.Map(x => new Person
			{
				Age = age,
				Gender = gender,
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

	public Result<Person, InvalidOperationException> Decease(DeceaseCommand command)
	{
		return MaritalStateMachine.Decease(command)
			.Map(x => this);
	}
}
