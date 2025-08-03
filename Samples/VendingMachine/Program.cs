using Aigamo.Arisama;
using Microsoft.Extensions.Logging;

namespace VendingMachine;

static class Program
{
	static async Task Main()
	{
		var loggerFactory = LoggerFactory.Create(builder =>
		{
			builder.AddSimpleConsole(options =>
			{
				options.IncludeScopes = true;
				options.SingleLine = true;
				options.TimestampFormat = "HH:mm:ss ";
			});
		});

		var vendingMachine = new StateMachineBuilder<IVendingMachineTransition, VendingMachineCommand, VendingMachineState>(loggerFactory)
			.AddTransition<ICanInsertCoin, InsertCoin, CoinInserted>()
			.AddTransition<ICanChooseProduct, ChooseProduct, ProductChosen>()
			.AddTransition<ICanReturnChange, ReturnChange, ChangeReturned>()
			.AddTransition<ICanDispenseProduct, DispenseProduct, ProductDispensed>()
			.Build([new Idle()]);

		await vendingMachine.SendAsync(new InsertCoin(Amount: new(100)));
		await vendingMachine.SendAsync(new InsertCoin(Amount: new(50)));
		await vendingMachine.SendAsync(new InsertCoin(Amount: new(10)));
		await vendingMachine.SendAsync(new ReturnChange());
		//await vendingMachine.Send(new ChooseProduct(ProductId: new(1)));
		//await vendingMachine.Send(new DispenseProduct());

		foreach (var state in vendingMachine.States)
		{
			Console.WriteLine(state);
		}
	}
}
