using System.Diagnostics;

namespace WebApp.CivilRegistration.Contracts.Persons.Enums;

public enum ApiGender
{
	Male = 1,
	Female = 2,
}

public static class ApiGenderEnumExtensions
{
	public static U Match<U>(
		this ApiGender value,
		Func<U> onMale,
		Func<U> onFemale
	)
	{
		return value switch
		{
			ApiGender.Male => onMale(),
			ApiGender.Female => onFemale(),
			_ => throw new UnreachableException(),
		};
	}
}
