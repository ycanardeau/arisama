using FastEndpoints;

namespace WebApp.CivilRegistration.Endpoints.DeathCertificates;

internal class DeathCertificatesGroup : Group
{
	public DeathCertificatesGroup()
	{
		Configure("/death-certificates", ep =>
		{
		});
	}
}
