namespace WebApp.UrlShortener.Infrastructure.GrainInterfaces;

//[Alias("WebApp.UrlShortener.Infrastructure.GrainInterfaces.IRobotGrain")]
internal interface IRobotGrain : IGrainWithStringKey
{
	//[Alias("AddInstruction")]
	Task AddInstruction(string instruction);

	//[Alias("GetNextInstruction")]
	Task<string?> GetNextInstruction();

	//[Alias("GetInstructionCount")]
	Task<int> GetInstructionCount();
}
