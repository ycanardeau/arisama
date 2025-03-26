using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.DivorceCertificates.Dtos;

namespace WebApp.CivilRegistration.Contracts.DivorceCertificates.Commands;

public sealed record CreateDivorceCertificateCommand(int MarriageCertificateId) : IRequest<Result<CreateDivorceCertificateResponseDto, InvalidOperationException>>;
