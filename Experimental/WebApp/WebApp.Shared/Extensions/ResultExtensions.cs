using WebApp.Shared.Errors;

namespace WebApp.Shared.Extensions;

// Success factories that fix TError per module, so callers can write Ok()/Ok(value) without
// naming the error type. Error values are constructed directly (e.g. new CivilRegistrationError.
// PersonNotFound()) and rely on Aigamo.Results' implicit conversions.
public static class CivilRegistrationResults
{
	public static Result<Unit, CivilRegistrationError> Ok() => new Unit();

	public static Result<T, CivilRegistrationError> Ok<T>(T value) => value;
}

public static class CivilRegistrationOrleansResults
{
	public static Result<Unit, CivilRegistrationOrleansError> Ok() => new Unit();

	public static Result<T, CivilRegistrationOrleansError> Ok<T>(T value) => value;

	// Lifts an error into a unit result, for call sites (e.g. Orleans grains) that need a
	// Result to chain onto, such as .AsTask().
	public static Result<Unit, CivilRegistrationOrleansError> Fail(
		CivilRegistrationOrleansError error
	) => error;
}
