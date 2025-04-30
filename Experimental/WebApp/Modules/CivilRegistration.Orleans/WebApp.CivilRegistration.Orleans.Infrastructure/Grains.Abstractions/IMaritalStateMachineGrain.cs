namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions.IMaritalStateMachineGrain")]
internal interface IMaritalStateMachineGrain : IGrainWithStringKey
{
	[Alias("Initialize")]
	Task Initialize();

	[Alias("Marray")]
	Task Marry();

	[Alias("Divorce")]
	Task Divorce();

	[Alias("BecomeWidowed")]
	Task BecomeWidowed();

	[Alias("Decease")]
	Task Decease();
}
