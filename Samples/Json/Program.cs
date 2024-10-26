using System.Text.Json;
using Aigamo.Arisama;
using Microsoft.Extensions.Logging;
using static Json.IMembershipCommand;
using static Json.IMembershipState;
using static Json.IMembershipTransition;

namespace Json;

static class Program
{
	static async Task Main()
	{
		var loggerFactory = LoggerFactory.Create(builder => { });
		var builder = new StateMachineBuilder<IMembershipTransition, IMembershipCommand, IMembershipState>(loggerFactory)
			.ConfigureState<ICanSuspend, Suspend, Inactive>()
			.ConfigureState<ICanTerminate, Terminate, Terminated>()
			.ConfigureState<ICanReactivate, Reactivate, Active>();

		Console.WriteLine("Creating member from JSON");
		var aMember = builder.Build(JsonSerializer.Deserialize<IEnumerable<IMembershipState>>(@"[{""$type"":""Active""}]") ?? throw new InvalidOperationException());

		Console.WriteLine($"Member created, membership state is {aMember.States.Last()}");

		await aMember.SendAsync(new Suspend());
		await aMember.SendAsync(new Reactivate());
		await aMember.SendAsync(new Terminate());

		Console.WriteLine("Member JSON:");

		var json = JsonSerializer.Serialize(aMember.States);
		Console.WriteLine(json);

		var anotherMember = builder.Build(JsonSerializer.Deserialize<IEnumerable<IMembershipState>>(json) ?? throw new InvalidOperationException());

		if (aMember.States.SequenceEqual(anotherMember.States))
		{
			Console.WriteLine("Members are equal");
		}

		Console.WriteLine("Press any key...");
		Console.ReadKey();
	}
}
