using static Arisama.ConsoleApp.IVendingMachineCommand;
using static Arisama.ConsoleApp.IVendingMachineState;

namespace Arisama.ConsoleApp;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
internal readonly partial struct Coin
{
	public static Coin operator +(Coin left, Coin right) => new(left.Value + right.Value);
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
internal readonly partial struct ProductId;

internal interface IVendingMachineState : IState
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

internal interface IVendingMachineCommandHandler<TCommand> : ICommandHandler<IVendingMachineState, TCommand>
	where TCommand : class, IVendingMachineCommand
{
	public class InsertCoinHandler : IVendingMachineCommandHandler<InsertCoin>
	{
		public Task ExecuteAsync(StateMachine<IVendingMachineState> vendingMachine, InsertCoin command)
		{
			vendingMachine.From<ICanInsertCoin>()
				.To(from => new CoinInserted(Amount: command.Amount, TotalAmount: from.TotalAmount + command.Amount));

			return Task.CompletedTask;
		}
	}

	public class ChooseProductHandler : IVendingMachineCommandHandler<ChooseProduct>
	{
		public Task ExecuteAsync(StateMachine<IVendingMachineState> vendingMachine, ChooseProduct command)
		{
			vendingMachine.From<ICanChooseProduct>()
				.To(from => new ProductChosen(ProductId: command.ProductId));

			return Task.CompletedTask;
		}
	}

	public class ReturnChangeHandler : IVendingMachineCommandHandler<ReturnChange>
	{
		public Task ExecuteAsync(StateMachine<IVendingMachineState> vendingMachine, ReturnChange command)
		{
			vendingMachine.From<ICanReturnChange>()
				.To(from => new ChangeReturned(TotalAmount: from.TotalAmount));

			return Task.CompletedTask;
		}
	}

	public class DispenseProductHandler : IVendingMachineCommandHandler<DispenseProduct>
	{
		public Task ExecuteAsync(StateMachine<IVendingMachineState> vendingMachine, DispenseProduct command)
		{
			vendingMachine.From<ICanDispenseProduct>()
				.To(from => new ProductDispensed(ProductId: from.ProductId));

			return Task.CompletedTask;
		}
	}
}

internal static class Program
{
	static async Task Main()
	{
		var vendingMachine = StateMachine<IVendingMachineState>.Create<Idle>();

		IVendingMachineCommand[] commands = [
			new InsertCoin(Amount: new(100)),
			new InsertCoin(Amount: new(50)),
			new InsertCoin(Amount: new(10)),
			new ReturnChange(),
			new ChooseProduct(ProductId: new(1)),
			new DispenseProduct(),
		];

		foreach (var command in commands)
		{
			await vendingMachine.ExecuteAsync(command);
			Console.WriteLine();
		}

		foreach (var state in vendingMachine.States)
		{
			Console.WriteLine(state);
		}
	}
}
