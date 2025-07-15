namespace WebApp.CivilRegistration.Orleans.Infrastructure.Surrogates;

[GenerateSerializer]
[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Surrogates.ResultSurrogate")]
internal struct ResultSurrogate
{
	[Id(0)]
	public bool IsOk;

	[Id(1)]
	public Exception Exception;
}
