using Aigamo.Arisama;
using Microsoft.Extensions.Logging;
using static VendingMachine.IVendingMachineCommand;
using static VendingMachine.IVendingMachineState;
using static VendingMachine.IVendingMachineTransition;

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

		var vendingMachine = new StateMachineBuilder<IVendingMachineTransition, IVendingMachineCommand, IVendingMachineState>(loggerFactory)
			.ConfigureState<ICanInsertCoin, InsertCoin, CoinInserted>()
			.ConfigureState<ICanChooseProduct, ChooseProduct, ProductChosen>()
			.ConfigureState<ICanReturnChange, ReturnChange, ChangeReturned>()
			.ConfigureState<ICanDispenseProduct, DispenseProduct, ProductDispensed>()
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
