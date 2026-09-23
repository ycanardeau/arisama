namespace WebApp.CivilRegistration.Orleans.Endpoints;

internal static class ResultExtensions
{
	private static IResult Problem(int statusCode, string code, string detail) =>
		TypedResults.Problem(
			statusCode: statusCode,
			detail: detail,
			extensions: new Dictionary<string, object?> { ["code"] = code }
		);

	public static IResult ToMinimalApiResult<T>(
		this Result<T, CivilRegistrationOrleansError> result
	)
	{
		return result.Fold(
			onOk: value => TypedResults.Ok(value),
			onError: error =>
				error.Match(
					AlreadyInitialized: _ =>
						Problem(
							StatusCodes.Status409Conflict,
							nameof(CivilRegistrationOrleansError.AlreadyInitialized),
							"The marital state machine has already been initialized."
						),
					InvalidMaritalState: _ =>
						Problem(
							StatusCodes.Status422UnprocessableEntity,
							nameof(CivilRegistrationOrleansError.InvalidMaritalState),
							"The requested transition is not valid from the current marital state."
						)
				)
		);
	}
}
