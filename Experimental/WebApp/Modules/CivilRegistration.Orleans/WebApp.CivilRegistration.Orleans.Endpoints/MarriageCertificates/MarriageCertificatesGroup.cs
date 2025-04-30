using FastEndpoints;

namespace WebApp.CivilRegistration.Orleans.Endpoints.MarriageCertificates;

internal class MarriageCertificatesGroup : Group
{
	public MarriageCertificatesGroup()
	{
		Configure("/orleans.marriage-certificates", ep =>
		{
		});
	}
}
