namespace WebApp.UrlShortener.Infrastructure.GrainClasses;

[GenerateSerializer]
internal class RobotState
{
	[Id(0)]
	public Queue<string> Instructions { get; set; } = new();
}
