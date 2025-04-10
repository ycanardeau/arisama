using DiscriminatedOnions;
using MediatR;
using WebApp.CivilRegistration.Contracts.MarriageCertificates.Dtos;

namespace WebApp.CivilRegistration.Contracts.MarriageCertificates.Commands;

public sealed record CreateMarriageCertificateCommand(Guid Person1Id, Guid Person2Id) : IRequest<Result<CreateMarriageCertificateResponseDto, InvalidOperationException>>;
