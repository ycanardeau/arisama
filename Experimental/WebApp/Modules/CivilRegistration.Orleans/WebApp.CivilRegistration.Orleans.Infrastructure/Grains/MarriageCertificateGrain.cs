using Nut.Results;
using WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains;

internal class MarriageCertificateGrain(
	[PersistentState(stateName: "marriageCertificate", storageName: "marriageCertificates")]
	IPersistentState<MarriageCertificateState> state
) : Grain, IMarriageCertificateGrain
{
	public Task<Result> Marry(IPersonGrain husband, IPersonGrain wife)
	{
		return husband.Marry(marryWith: wife.GetPrimaryKey())
			.FlatMap(() => wife.Marry(marryWith: husband.GetPrimaryKey()));
	}
}
