using FastEndpoints;

namespace WebApp.CivilRegistration.Endpoints.DivorceCertificates;

internal class DivorceCertificatesGroup : Group
{
	public DivorceCertificatesGroup()
	{
		Configure("/divorce-certificates", ep =>
		{
		});
	}
}
