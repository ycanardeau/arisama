using FastEndpoints;

namespace WebApp.CivilRegistration.Endpoints.MarriageCertificates;

internal class MarriageCertificatesGroup : Group
{
	public MarriageCertificatesGroup()
	{
		Configure("/marriage-certificates", ep =>
		{
		});
	}
}
