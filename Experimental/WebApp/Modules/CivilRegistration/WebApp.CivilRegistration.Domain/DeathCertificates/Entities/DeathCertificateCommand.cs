using WebApp.CivilRegistration.Domain.Persons.Entities;

namespace WebApp.CivilRegistration.Domain.DeathCertificates.Entities;

internal abstract record DeathCertificateCommand;

internal sealed record CreateCommand(Person Deceased) : DeathCertificateCommand;
