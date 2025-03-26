using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.DeathCertificates.Dtos;

namespace WebApp.CivilRegistration.Contracts.DeathCertificates.Commands;

public sealed record CreateDeathCertificateCommand(int DeceasedId) : IRequest<Result<CreateDeathCertificateResponseDto, InvalidOperationException>>;
