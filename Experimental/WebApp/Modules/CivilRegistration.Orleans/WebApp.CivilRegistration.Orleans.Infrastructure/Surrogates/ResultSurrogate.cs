namespace WebApp.CivilRegistration.Orleans.Infrastructure.Surrogates;

// Orleans-serializable discriminator for CivilRegistrationOrleansError, which lives in the Orleans-free
// WebApp.Shared kernel and therefore cannot carry [GenerateSerializer] itself.
[GenerateSerializer]
internal enum CivilRegistrationOrleansErrorKind : byte
{
	AlreadyInitialized,
	InvalidMaritalState,
}

[GenerateSerializer]
[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Surrogates.ResultSurrogate")]
internal struct ResultSurrogate
{
	[Id(0)]
	public bool IsOk;

	[Id(1)]
	public CivilRegistrationOrleansErrorKind Error;
}
