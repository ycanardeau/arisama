using WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;

namespace WebApp.CivilRegistration.Domain.DivorceCertificates.Entities;

internal abstract record DivorceCertificateCommand
{
	public sealed record CreateCommand(MarriageCertificate MarriageCertificate) : DivorceCertificateCommand;

	private DivorceCertificateCommand() { }
}
