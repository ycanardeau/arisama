namespace WebApp.CivilRegistration.Endpoints;

internal static class ResultExtensions
{
	private static IResult Problem(int statusCode, string code, string detail) =>
		TypedResults.Problem(
			statusCode: statusCode,
			detail: detail,
			extensions: new Dictionary<string, object?> { ["code"] = code }
		);

	private static IResult Unprocessable(string code, string detail) =>
		Problem(StatusCodes.Status422UnprocessableEntity, code, detail);

	private static IResult NotFound(string code, string detail) =>
		Problem(StatusCodes.Status404NotFound, code, detail);

	public static IResult ToMinimalApiResult<T>(this Result<T, CivilRegistrationError> result)
	{
		return result.Fold(
			onOk: value => TypedResults.Ok(value),
			onError: error =>
				error.Match(
					SameIndividual: _ =>
						Unprocessable(
							nameof(CivilRegistrationError.SameIndividual),
							"A marriage requires two individuals."
						),
					SameSexMarriage: _ =>
						Unprocessable(
							nameof(CivilRegistrationError.SameSexMarriage),
							"Same-sex marriage is not allowed in Japan as of writing."
						),
					IneligibleHusband: _ =>
						Unprocessable(
							nameof(CivilRegistrationError.IneligibleHusband),
							"The person cannot take the role of husband."
						),
					IneligibleWife: _ =>
						Unprocessable(
							nameof(CivilRegistrationError.IneligibleWife),
							"The person cannot take the role of wife."
						),
					NotMarriageable: _ =>
						Unprocessable(
							nameof(CivilRegistrationError.NotMarriageable),
							"The person is not of marriageable age."
						),
					InvalidMaritalState: _ =>
						Unprocessable(
							nameof(CivilRegistrationError.InvalidMaritalState),
							"The requested transition is not valid from the current marital state."
						),
					PersonNotFound: _ =>
						NotFound(
							nameof(CivilRegistrationError.PersonNotFound),
							"The referenced person does not exist."
						),
					MarriageCertificateNotFound: _ =>
						NotFound(
							nameof(CivilRegistrationError.MarriageCertificateNotFound),
							"The referenced marriage certificate does not exist."
						)
				)
		);
	}
}
