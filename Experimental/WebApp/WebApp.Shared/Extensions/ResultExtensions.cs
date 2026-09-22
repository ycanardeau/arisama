using WebApp.Shared.Errors;

namespace WebApp.Shared.Extensions;

public static class ResultExtensions
{
	// Success factories that fix TError to WebAppError, mirroring the error helpers below.
	// Bodies rely on Aigamo.Results' implicit conversions (aigamo_results_conversion_style = implicit).
	public static Result<Unit, WebAppError> Ok() => new Unit();

	public static Result<T, WebAppError> Ok<T>(T value) => value;

	// Unwraps a successful result or throws. For call sites (e.g. Orleans surrogate conversion)
	// where the value is an invariant and an error would indicate corrupt data.
	public static T GetOrThrow<T>(this Result<T, WebAppError> result)
	{
		return result.Fold(
			onOk: value => value,
			onError: error =>
				throw new InvalidOperationException(
					$"Expected a successful result but got error '{error.GetType().Name}'."
				)
		);
	}

	public static Result<Unit, WebAppError> BadRequest() => new WebAppError.BadRequest();

	public static Result<T, WebAppError> BadRequest<T>() => new WebAppError.BadRequest();

	public static Result<Unit, WebAppError> Unauthorized() => new WebAppError.Unauthorized();

	public static Result<T, WebAppError> Unauthorized<T>() => new WebAppError.Unauthorized();

	public static Result<Unit, WebAppError> Forbidden() => new WebAppError.Forbidden();

	public static Result<T, WebAppError> Forbidden<T>() => new WebAppError.Forbidden();

	public static Result<Unit, WebAppError> NotFound() => new WebAppError.NotFound();

	public static Result<T, WebAppError> NotFound<T>() => new WebAppError.NotFound();

	public static Result<Unit, WebAppError> UnprocessableEntity() =>
		new WebAppError.UnprocessableEntity();

	public static Result<T, WebAppError> UnprocessableEntity<T>() =>
		new WebAppError.UnprocessableEntity();

	// Surfaced as HTTP 500. Used for internal invariants that previously threw
	// InvalidOperationException, preserving their original non-mapped behavior.
	public static Result<Unit, WebAppError> Unexpected() => new WebAppError.Unexpected();

	public static Result<T, WebAppError> Unexpected<T>() => new WebAppError.Unexpected();
}
