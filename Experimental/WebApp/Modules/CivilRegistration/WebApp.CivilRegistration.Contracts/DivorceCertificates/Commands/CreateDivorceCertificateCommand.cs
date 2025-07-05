using MediatR;
using Nut.Results;
using WebApp.CivilRegistration.Contracts.DivorceCertificates.Dtos;

namespace WebApp.CivilRegistration.Contracts.DivorceCertificates.Commands;

public sealed record CreateDivorceCertificateCommand(Guid MarriageCertificateId) : IRequest<Result<CreateDivorceCertificateResponseDto>>;
