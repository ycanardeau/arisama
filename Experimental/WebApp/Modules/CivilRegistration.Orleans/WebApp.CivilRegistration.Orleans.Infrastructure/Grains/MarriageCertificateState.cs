namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains;

[GenerateSerializer]
[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.MarriageCertificateState")]
internal sealed record MarriageCertificateState
{
	[Id(0)]
	public required string/* TODO: Use strongly-typed ID. */ HusbandId { get; init; }
	[Id(1)]
	public required string/* TODO: Use strongly-typed ID. */ WifeId { get; init; }
}
