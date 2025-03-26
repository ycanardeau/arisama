using WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;

namespace WebApp.CivilRegistration.Domain.DivorceCertificates.Entities;

internal abstract record DivorceCertificateCommand;

internal sealed record CreateCommand(MarriageCertificate MarriageCertificate) : DivorceCertificateCommand;
