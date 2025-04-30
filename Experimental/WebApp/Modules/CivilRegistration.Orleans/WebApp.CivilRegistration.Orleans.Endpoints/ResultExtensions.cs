using DiscriminatedOnions;
using Microsoft.AspNetCore.Http;

namespace WebApp.CivilRegistration.Orleans.Endpoints;

internal static class ResultExtensions
{
	public static IResult ToApiResult<T, TError>(this Result<T, TError> result)
	{
		return result.Match(
			x => Results.BadRequest(x),
			x => Results.Ok(x)
		);
	}
}
