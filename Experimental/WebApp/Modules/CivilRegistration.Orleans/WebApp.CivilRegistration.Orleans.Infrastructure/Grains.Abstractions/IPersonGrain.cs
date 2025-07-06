using Nut.Results;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions.IPersonGrain")]
internal interface IPersonGrain : IGrainWithGuidKey
{
	[Alias("Initialize")]
	Task<Result> Initialize();

	[Alias("Marry")]
	Task<Result> Marry(Guid marryWith);

	[Alias("Divorce")]
	Task<Result> Divorce();

	[Alias("BecomeWidowed")]
	Task<Result> BecomeWidowed();

	[Alias("Decease")]
	Task<Result> Decease();
}
