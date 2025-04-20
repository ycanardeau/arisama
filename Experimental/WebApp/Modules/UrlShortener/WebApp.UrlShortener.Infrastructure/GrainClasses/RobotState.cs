namespace WebApp.UrlShortener.Infrastructure.GrainClasses;

internal class RobotState
{
	public Queue<string> Instructions { get; set; } = new();
}
