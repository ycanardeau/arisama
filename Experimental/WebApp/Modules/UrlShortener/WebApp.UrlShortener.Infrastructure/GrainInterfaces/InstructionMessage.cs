namespace WebApp.UrlShortener.Infrastructure.GrainInterfaces;

[GenerateSerializer]
public sealed record InstructionMessage(
	[property: Id(0)]
	string Instruction,
	[property: Id(1)]
	string Robot
);
