using static Aigamo.Arisama.ConsoleApp.IVendingMachineCommand;
using static Aigamo.Arisama.ConsoleApp.IVendingMachineState;

namespace Aigamo.Arisama.ConsoleApp;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
readonly partial struct Coin
{
	public static Coin operator +(Coin left, Coin right) => new(left.Value + right.Value);
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
readonly partial struct ProductId;

partial interface IVendingMachineState : IState
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

partial interface IVendingMachineState : IState
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

interface IVendingMachineCommand : ICommand
{
	public sealed record InsertCoin(Coin Amount) : IVendingMachineCommand;

	public sealed record ChooseProduct(ProductId ProductId) : IVendingMachineCommand;

	public sealed record ReturnChange : IVendingMachineCommand;

	public sealed record DispenseProduct : IVendingMachineCommand;
}

static class Program
{
	static void Main()
	{
		var vendingMachine = new StateMachineBuilder<IVendingMachineState, IVendingMachineCommand>()
			.From<ICanInsertCoin>()
				.On<InsertCoin>()
				.To((from, command) => new CoinInserted(Amount: command.Amount, TotalAmount: from.TotalAmount + command.Amount))
			.From<ICanChooseProduct>()
				.On<ChooseProduct>()
				.To((from, command) => new ProductChosen(ProductId: command.ProductId))
			.From<ICanReturnChange>()
				.On<ReturnChange>()
				.To((from, command) => new ChangeReturned(TotalAmount: from.TotalAmount))
			.From<ICanDispenseProduct>()
				.On<DispenseProduct>()
				.To((from, command) => new ProductDispensed(ProductId: from.ProductId))
			.Build(new Idle());

		vendingMachine.Send(new InsertCoin(Amount: new(100)));
		vendingMachine.Send(new InsertCoin(Amount: new(50)));
		vendingMachine.Send(new InsertCoin(Amount: new(10)));
		vendingMachine.Send(new ReturnChange());
		vendingMachine.Send(new ChooseProduct(ProductId: new(1)));
		vendingMachine.Send(new DispenseProduct());

		foreach (var state in vendingMachine.States)
		{
			Console.WriteLine(state);
		}
	}
}
