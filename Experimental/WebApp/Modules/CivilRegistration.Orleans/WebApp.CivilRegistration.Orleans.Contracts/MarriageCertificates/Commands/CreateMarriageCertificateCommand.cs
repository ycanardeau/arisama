using MediatR;
using Nut.Results;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Dtos;

namespace WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Commands;

public sealed record CreateMarriageCertificateCommand(Guid HusbandId, Guid WifeId) : IRequest<Result<CreateMarriageCertificateResponseDto>>;
