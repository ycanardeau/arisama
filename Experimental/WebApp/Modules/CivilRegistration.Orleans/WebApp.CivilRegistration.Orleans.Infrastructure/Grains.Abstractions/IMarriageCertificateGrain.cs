using Nut.Results;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions.IMarriageCertificateGrain")]
internal interface IMarriageCertificateGrain : IGrainWithGuidKey
{
	[Alias("Marry")]
	Task<Result> Marry(IPersonGrain husband, IPersonGrain wife);
}
