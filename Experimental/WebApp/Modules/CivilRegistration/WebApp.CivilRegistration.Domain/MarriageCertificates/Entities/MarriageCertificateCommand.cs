using WebApp.CivilRegistration.Domain.Persons.Entities;

namespace WebApp.CivilRegistration.Domain.MarriageCertificates.Entities;

internal abstract record MarriageCertificateCommand;

internal sealed record CreateCommand(Person Husband, Person Wife) : MarriageCertificateCommand;
