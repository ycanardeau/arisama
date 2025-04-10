using WebApp.CivilRegistration.Domain.DeathCertificates.Entities;
using WebApp.CivilRegistration.Domain.DivorceCertificates.Entities;
using WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;

namespace WebApp.CivilRegistration.Domain.Persons.Entities;

internal abstract record MaritalCommand;

internal sealed record MarryCommand(
	MarriageCertificate MarriageCertificate,
	Person MarryWith
) : MaritalCommand;

internal sealed record DivorceCommand(DivorceCertificate DivorceCertificate) : MaritalCommand;

internal sealed record BecomeWidowedCommand : MaritalCommand;

internal sealed record DeceaseCommand(DeathCertificate DeathCertificate) : MaritalCommand;
