namespace WebApp.Shared.Errors;

/// <summary>
/// The application-wide error type carried by <see cref="Aigamo.Results.Result{T, TError}"/>.
/// Each case maps to an HTTP status in the modules' ResultExtensions.
/// </summary>
[GenerateMatch]
public abstract record WebAppError
{
	private WebAppError() { }

	public sealed record BadRequest : WebAppError;

	public sealed record Unauthorized : WebAppError;

	public sealed record Forbidden : WebAppError;

	public sealed record NotFound : WebAppError;

	public sealed record UnprocessableEntity : WebAppError;

	/// <summary>
	/// An error that has no dedicated HTTP status and is surfaced as 500.
	/// Used for internal invariants that previously threw
	/// <see cref="System.InvalidOperationException"/>.
	/// </summary>
	public sealed record Unexpected : WebAppError;
}
