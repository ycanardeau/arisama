using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.MarriageCertificates.Dtos;

namespace WebApp.CivilRegistration.Contracts.MarriageCertificates.Commands;

public sealed record CreateMarriageCertificateCommand(int Person1Id, int Person2Id) : IRequest<Result<CreateMarriageCertificateResponseDto, InvalidOperationException>>;
