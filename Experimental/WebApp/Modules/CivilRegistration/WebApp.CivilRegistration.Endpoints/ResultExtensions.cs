namespace WebApp.CivilRegistration.Endpoints;

internal static class ResultExtensions
{
	public static IResult ToMinimalApiResult<T>(this Result<T, CivilRegistrationError> result)
	{
		return result.Fold(
			onOk: value => TypedResults.Ok(value),
			onError: error =>
				error.Match<IResult>(
					onSameIndividual: _ => TypedResults.UnprocessableEntity(),
					onSameSexMarriage: _ => TypedResults.UnprocessableEntity(),
					onIneligibleHusband: _ => TypedResults.UnprocessableEntity(),
					onIneligibleWife: _ => TypedResults.UnprocessableEntity(),
					onNotMarriageable: _ => TypedResults.UnprocessableEntity(),
					onInvalidMaritalState: _ => TypedResults.UnprocessableEntity(),
					onPersonNotFound: _ => TypedResults.NotFound(),
					onMarriageCertificateNotFound: _ => TypedResults.NotFound()
				)
		);
	}
}
