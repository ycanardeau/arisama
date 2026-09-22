namespace WebApp.Shared.Errors;

/// <summary>
/// Domain errors for the Orleans-based Civil Registration module, carried by
/// <see cref="Aigamo.Results.Result{T, TError}"/>. The web layer maps each case to an HTTP
/// status (see the module's ResultExtensions).
/// </summary>
[GenerateMatch]
public abstract record CivilRegistrationOrleansError
{
	private CivilRegistrationOrleansError() { }

	/// <summary>The marital state machine has already been initialized.</summary>
	public sealed record AlreadyInitialized : CivilRegistrationOrleansError;

	/// <summary>The requested transition is not valid from the current marital state.</summary>
	public sealed record InvalidMaritalState : CivilRegistrationOrleansError;
}
