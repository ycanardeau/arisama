using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Dtos;

namespace WebApp.CivilRegistration.Orleans.Contracts.MarriageCertificates.Commands;

public sealed record CreateMarriageCertificateCommand(int HusbandId, int WifeId) : IRequest<Result<CreateMarriageCertificateResponseDto, InvalidOperationException>>;
