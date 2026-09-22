namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions.IPersonGrain")]
internal interface IPersonGrain : IGrainWithGuidKey
{
	[Alias("Initialize")]
	Task<Result<Unit, CivilRegistrationOrleansError>> Initialize();

	[Alias("Marry")]
	Task<Result<Unit, CivilRegistrationOrleansError>> Marry(Guid marryWith);

	[Alias("Divorce")]
	Task<Result<Unit, CivilRegistrationOrleansError>> Divorce();

	[Alias("BecomeWidowed")]
	Task<Result<Unit, CivilRegistrationOrleansError>> BecomeWidowed();

	[Alias("Decease")]
	Task<Result<Unit, CivilRegistrationOrleansError>> Decease();
}
