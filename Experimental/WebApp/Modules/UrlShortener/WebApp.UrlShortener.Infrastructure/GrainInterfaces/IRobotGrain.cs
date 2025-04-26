namespace WebApp.UrlShortener.Infrastructure.GrainInterfaces;

//[Alias("WebApp.UrlShortener.Infrastructure.GrainInterfaces.IRobotGrain")]
internal interface IRobotGrain : IGrainWithStringKey
{
	//[Alias("AddInstruction")]
	[Transaction(TransactionOption.CreateOrJoin)]
	Task AddInstruction(string instruction);

	//[Alias("GetNextInstruction")]
	[Transaction(TransactionOption.CreateOrJoin)]
	Task<string?> GetNextInstruction();

	//[Alias("GetInstructionCount")]
	[Transaction(TransactionOption.CreateOrJoin)]
	Task<int> GetInstructionCount();
}
