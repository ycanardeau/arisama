using Nut.Results;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions.IMaritalStateMachineGrain")]
internal interface IMaritalStateMachineGrain : IGrainWithGuidKey
{
	[Alias("Initialize")]
	Task<Result> Initialize();

	[Alias("Marray")]
	Task<Result> Marry(Guid marryWith);

	[Alias("Divorce")]
	Task<Result> Divorce();

	[Alias("BecomeWidowed")]
	Task<Result> BecomeWidowed();

	[Alias("Decease")]
	Task<Result> Decease();
}
