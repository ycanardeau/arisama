using WebApp.CivilRegistration.Contracts.DeathCertificates.Dtos;

namespace WebApp.CivilRegistration.Contracts.DeathCertificates.Commands;

public sealed record CreateDeathCertificateCommand(Guid DeceasedId) : IRequest<Result<CreateDeathCertificateResponseDto>>;
