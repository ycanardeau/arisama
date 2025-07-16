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

[GenerateSerializer]
[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Surrogates.ResultSurrogate`1")]
internal struct ResultSurrogate<T>
{
	[Id(0)]
	public bool IsOk;

	[Id(1)]
	public Exception Exception;

	[Id(2)]
	public T Value;
}
