namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions.IPersonGrain")]
internal interface IPersonGrain : IGrainWithStringKey
{
	[Alias("Initialize")]
	Task Initialize();

	[Alias("Marry")]
	Task Marry();

	[Alias("Divorce")]
	Task Divorce();

	[Alias("BecomeWidowed")]
	Task BecomeWidowed();

	[Alias("Decease")]
	Task Decease();
}
