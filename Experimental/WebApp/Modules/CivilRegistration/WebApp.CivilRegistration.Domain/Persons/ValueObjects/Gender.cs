using System.Diagnostics;

namespace WebApp.CivilRegistration.Domain.Persons.ValueObjects;

internal abstract record Gender
{
	public abstract bool CanMarryAtAge(Age age);
}

internal sealed record Male : Gender
{
	/// <summary>
	/// The minimum marriageable age for males in Japan as of 2021.
	/// </summary>
	private static readonly Age MinimumMarriageableAge = new(18);

	public override bool CanMarryAtAge(Age age)
	{
		return age >= MinimumMarriageableAge;
	}
}

internal sealed record Female : Gender
{
	/// <summary>
	/// The minimum marriageable age for females in Japan as of 2021.
	/// </summary>
	private static readonly Age MinimumMarriageableAge = new(16);

	public override bool CanMarryAtAge(Age age)
	{
		return age >= MinimumMarriageableAge;
	}
}

internal static class GenderExtensions
{
	public static U Match<U>(
		this Gender value,
		Func<Male, U> onMale,
		Func<Female, U> onFemale
	)
	{
		return value switch
		{
			Male x => onMale(x),
			Female x => onFemale(x),
			_ => throw new UnreachableException(),
		};
	}
}
