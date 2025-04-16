using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.MarriageCertificates.Dtos;

namespace WebApp.CivilRegistration.Contracts.MarriageCertificates.Commands;

public sealed record CreateMarriageCertificateCommand(int HusbandId, int WifeId) : IRequest<Result<CreateMarriageCertificateResponseDto, InvalidOperationException>>;
