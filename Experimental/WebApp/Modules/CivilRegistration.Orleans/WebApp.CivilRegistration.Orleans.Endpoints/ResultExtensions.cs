namespace WebApp.CivilRegistration.Orleans.Endpoints;

internal static class ResultExtensions
{
	public static IResult ToMinimalApiResult<T>(this Result<T, WebAppError> result)
	{
		return result.Fold(
			onOk: value => TypedResults.Ok(value),
			onError: error =>
				error.Match<IResult>(
					onBadRequest: _ => TypedResults.BadRequest(),
					onUnauthorized: _ => TypedResults.Unauthorized(),
					onForbidden: _ => TypedResults.Forbid(),
					onNotFound: _ => TypedResults.NotFound(),
					onUnprocessableEntity: _ => TypedResults.UnprocessableEntity(),
					onUnexpected: _ => TypedResults.InternalServerError()
				)
		);
	}
}
