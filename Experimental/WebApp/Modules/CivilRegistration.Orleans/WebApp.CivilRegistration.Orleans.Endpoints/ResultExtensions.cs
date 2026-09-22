namespace WebApp.CivilRegistration.Orleans.Endpoints;

internal static class ResultExtensions
{
	public static IResult ToMinimalApiResult<T>(
		this Result<T, CivilRegistrationOrleansError> result
	)
	{
		return result.Fold(
			onOk: value => TypedResults.Ok(value),
			onError: error =>
				error.Match<IResult>(
					onAlreadyInitialized: _ => TypedResults.Conflict(),
					onInvalidMaritalState: _ => TypedResults.UnprocessableEntity()
				)
		);
	}
}
