namespace WebApp.UrlShortener.Infrastructure.GrainInterfaces;

internal interface IBatchGrain : IGrainWithIntegerKey
{
	[Transaction(TransactionOption.Create)]
	Task AddInstructions((string Id, string Instruction)[] values);
}
