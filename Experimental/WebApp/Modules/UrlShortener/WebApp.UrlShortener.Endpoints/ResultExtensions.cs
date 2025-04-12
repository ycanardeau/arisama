using DiscriminatedOnions;
using Microsoft.AspNetCore.Http;

namespace WebApp.UrlShortener.Endpoints;

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
