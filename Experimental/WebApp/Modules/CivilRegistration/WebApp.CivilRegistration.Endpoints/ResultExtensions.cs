using Microsoft.AspNetCore.Http;
using Nut.Results;

namespace WebApp.CivilRegistration.Endpoints;

internal static class ResultExtensions
{
	public static IResult ToApiResult<T>(this Result<T> result)
	{
		return result
			.Map(x => Results.Ok(x))
			.GetOr(x => Results.BadRequest(x));
	}

	public static async Task<TOut> Pipe<TIn, TOut>(this Task<TIn> previous, Func<TIn, TOut> next)
	{
		return next(await previous);
	}
}
