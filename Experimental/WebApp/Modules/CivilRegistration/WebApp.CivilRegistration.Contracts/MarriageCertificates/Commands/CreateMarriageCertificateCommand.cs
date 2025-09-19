using WebApp.CivilRegistration.Contracts.MarriageCertificates.Dtos;

namespace WebApp.CivilRegistration.Contracts.MarriageCertificates.Commands;

public sealed record CreateMarriageCertificateCommand(Guid HusbandId, Guid WifeId) : IRequest<Result<CreateMarriageCertificateResponseDto>>;
