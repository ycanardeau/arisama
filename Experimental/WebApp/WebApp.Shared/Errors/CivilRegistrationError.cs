namespace WebApp.Shared.Errors;

/// <summary>
/// Domain errors for the Civil Registration module, carried by
/// <see cref="Aigamo.Results.Result{T, TError}"/>. Each case names a specific business-rule
/// violation; the web layer decides the corresponding HTTP status (see the module's
/// ResultExtensions), keeping HTTP concerns out of the domain.
/// </summary>
[GenerateMatch]
public abstract record CivilRegistrationError
{
	private CivilRegistrationError() { }

	/// <summary>A person cannot marry themselves.</summary>
	public sealed record SameIndividual : CivilRegistrationError;

	/// <summary>Same-sex marriage is not allowed in Japan as of writing.</summary>
	public sealed record SameSexMarriage : CivilRegistrationError;

	/// <summary>The person cannot take the role of husband.</summary>
	public sealed record IneligibleHusband : CivilRegistrationError;

	/// <summary>The person cannot take the role of wife.</summary>
	public sealed record IneligibleWife : CivilRegistrationError;

	/// <summary>The person is not of marriageable age.</summary>
	public sealed record NotMarriageable : CivilRegistrationError;

	/// <summary>The requested transition is not valid from the current marital state.</summary>
	public sealed record InvalidMaritalState : CivilRegistrationError;

	/// <summary>The referenced person does not exist.</summary>
	public sealed record PersonNotFound : CivilRegistrationError;

	/// <summary>The referenced marriage certificate does not exist.</summary>
	public sealed record MarriageCertificateNotFound : CivilRegistrationError;
}
