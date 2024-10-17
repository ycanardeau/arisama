using static Aigamo.Arisama.ConsoleApp.IVendingMachineCommand;
using static Aigamo.Arisama.ConsoleApp.IVendingMachineState;

namespace Aigamo.Arisama.ConsoleApp;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
internal readonly partial struct Coin
{
	public static Coin operator +(Coin left, Coin right) => new(left.Value + right.Value);
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
internal readonly partial struct ProductId;

internal partial interface IVendingMachineState : IState
{
	public interface ICanInsertCoin : IVendingMachineState
	{
		Coin TotalAmount { get; }
	}

	public interface ICanChooseProduct : IVendingMachineState;

	public interface ICanReturnChange : IVendingMachineState
	{
		Coin TotalAmount { get; }
	}

	public interface ICanDispenseProduct : IVendingMachineState
	{
		ProductId ProductId { get; }
	}
}

internal partial interface IVendingMachineState : IState
{
	public sealed record Idle : IVendingMachineState,
		ICanInsertCoin
	{
		public Coin TotalAmount { get; } = Coin.Empty;
	}

	public sealed record CoinInserted(Coin Amount, Coin TotalAmount) : IVendingMachineState,
		ICanInsertCoin,
		ICanChooseProduct,
		ICanReturnChange;

	public sealed record ProductChosen(ProductId ProductId) : IVendingMachineState,
		ICanDispenseProduct;

	public sealed record ChangeReturned(Coin TotalAmount) : IVendingMachineState;

	public sealed record ProductDispensed(ProductId ProductId) : IVendingMachineState;
}

internal interface IVendingMachineCommand : ICommand
{
	public sealed record InsertCoin(Coin Amount) : IVendingMachineCommand;

	public sealed record ChooseProduct(ProductId ProductId) : IVendingMachineCommand;

	public sealed record ReturnChange : IVendingMachineCommand;

	public sealed record DispenseProduct : IVendingMachineCommand;
}

internal static class Program
{
	static async Task Main()
	{
		var vendingMachine = StateMachine<IVendingMachineState, IVendingMachineCommand>.Create<Idle>();

		vendingMachine
			.ConfigureState<ICanInsertCoin, InsertCoin, CoinInserted>((from, command) => new CoinInserted(Amount: command.Amount, TotalAmount: from.TotalAmount + command.Amount))
			.ConfigureState<ICanChooseProduct, ChooseProduct, ProductChosen>((from, command) => new ProductChosen(ProductId: command.ProductId))
			.ConfigureState<ICanReturnChange, ReturnChange, ChangeReturned>((from, command) => new ChangeReturned(TotalAmount: from.TotalAmount))
			.ConfigureState<ICanDispenseProduct, DispenseProduct, ProductDispensed>((from, command) => new ProductDispensed(ProductId: from.ProductId));

		await vendingMachine.ExecuteAsync(new InsertCoin(Amount: new(100)));
		await vendingMachine.ExecuteAsync(new InsertCoin(Amount: new(50)));
		await vendingMachine.ExecuteAsync(new InsertCoin(Amount: new(10)));
		await vendingMachine.ExecuteAsync(new ReturnChange());
		await vendingMachine.ExecuteAsync(new ChooseProduct(ProductId: new(1)));
		await vendingMachine.ExecuteAsync(new DispenseProduct());

		foreach (var state in vendingMachine.States)
		{
			Console.WriteLine(state);
		}
	}
}
