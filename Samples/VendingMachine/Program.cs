using Aigamo.Arisama;
using Microsoft.Extensions.Logging;
using static VendingMachine.IVendingMachineCommand;
using static VendingMachine.IVendingMachineState;
using static VendingMachine.IVendingMachineTransition;

namespace VendingMachine;

static class Program
{
	static void Main()
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
			.From<ICanInsertCoin>()
				.To<CoinInserted>()
				.On<InsertCoin>()
			.From<ICanChooseProduct>()
				.To<ProductChosen>()
				.On<ChooseProduct>()
			.From<ICanReturnChange>()
				.To<ChangeReturned>()
				.On<ReturnChange>()
			.From<ICanDispenseProduct>()
				.To<ProductDispensed>()
				.On<DispenseProduct>()
			.Build([new Idle()]);

		vendingMachine.Send(new InsertCoin(Amount: new(100)));
		vendingMachine.Send(new InsertCoin(Amount: new(50)));
		vendingMachine.Send(new InsertCoin(Amount: new(10)));
		vendingMachine.Send(new ReturnChange());
		//vendingMachine.Send(new ChooseProduct(ProductId: new(1)));
		//vendingMachine.Send(new DispenseProduct());

		foreach (var state in vendingMachine.States)
		{
			Console.WriteLine(state);
		}
	}
}
