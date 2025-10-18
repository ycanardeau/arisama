using System.Diagnostics;
using WebApp.Shared.Exceptions;

namespace WebApp.CivilRegistration.Endpoints;

internal static class ResultExtensions
{
	public static IResult ToMinimalApiResult<T>(this Result<T> result)
	{
		return result.Match<T, IResult>(
			ok: x => TypedResults.Ok(x),
			err: x => x switch
			{
				BadRequestException => TypedResults.BadRequest(),
				UnauthorizedException => TypedResults.Unauthorized(),
				ForbiddenException => TypedResults.Forbid(),
				NotFoundException => TypedResults.NotFound(),
				UnprocessableEntityException => TypedResults.UnprocessableEntity(),
				_ => throw new UnreachableException()
			}
		).Get();
	}
}
