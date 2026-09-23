using WebApp.CivilRegistration.Domain.People.Entities;

namespace WebApp.CivilRegistration.Domain.DeathCertificates.Entities;

internal abstract record DeathCertificateCommand;

internal sealed record CreateCommand(Person Deceased, Person? Widowed) : DeathCertificateCommand;
