namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

[Alias(
	"WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions.IMaritalStateMachineGrain"
)]
internal interface IMaritalStateMachineGrain : IGrainWithGuidKey
{
	[Alias("Initialize")]
	Task<Result<Unit, WebAppError>> Initialize();

	[Alias("Marray")]
	Task<Result<Unit, WebAppError>> Marry(Guid marryWith);

	[Alias("Divorce")]
	Task<Result<Unit, WebAppError>> Divorce();

	[Alias("BecomeWidowed")]
	Task<Result<Unit, WebAppError>> BecomeWidowed();

	[Alias("Decease")]
	Task<Result<Unit, WebAppError>> Decease();
}
