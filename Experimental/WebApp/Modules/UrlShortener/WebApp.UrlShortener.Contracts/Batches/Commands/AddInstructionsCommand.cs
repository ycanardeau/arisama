using DiscriminatedOnions;
using MediatR;
using WebApp.UrlShortener.Contracts.Batches.Dtos;

namespace WebApp.UrlShortener.Contracts.Batches.Commands;

public sealed record KeyValue(string Id, string Instruction);

public sealed record AddInstructionsCommand(KeyValue[] Values) : IRequest<Result<AddInstructionsResponseDto, InvalidOperationException>>;
