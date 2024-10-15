namespace Arisama;

using static IVendingMachineCommand;
using static IVendingMachineState;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
internal readonly partial struct Coin
{
    public static Coin operator +(Coin left, Coin right) => new(left.Value + right.Value);
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
internal readonly partial struct ProductId;

internal interface IVendingMachineState
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

    public sealed record Idle : IVendingMachineState, ICanInsertCoin
    {
        public Coin TotalAmount { get; } = Coin.Empty;
    }

    public sealed record CoinInserted(Coin Amount, Coin TotalAmount) : IVendingMachineState, ICanInsertCoin, ICanChooseProduct, ICanReturnChange;

    public sealed record ProductChosen(ProductId ProductId) : IVendingMachineState, ICanDispenseProduct;

    public sealed record ChangeReturned(Coin TotalAmount) : IVendingMachineState;

    public sealed record ProductDispensed(ProductId ProductId) : IVendingMachineState;
}

internal interface IVendingMachineCommand
{
    public sealed record InsertCoin(Coin Amount) : IVendingMachineCommand;

    public sealed record ChooseProduct(ProductId ProductId) : IVendingMachineCommand;

    public sealed record ReturnChange : IVendingMachineCommand;

    public sealed record DispenseProduct : IVendingMachineCommand;
}

internal class VendingMachine
{
    private readonly List<IVendingMachineState> _states = [];
    public IReadOnlyCollection<IVendingMachineState> States => _states.AsReadOnly();

	public interface IOnBuilder<TFrom, TOn>
		where TOn : class, IVendingMachineCommand
	{
		void To<TTo>(Func<TFrom, TOn, TTo> callback)
			where TTo : class, IVendingMachineState;
	}

	private sealed class OnBuilder<TFrom, TOn>(VendingMachine vendingMachine) : IOnBuilder<TFrom, TOn>
		where TFrom : class, IVendingMachineState
		where TOn : class, IVendingMachineCommand
	{
		public void To<TTo>(Func<TFrom, TOn, TTo> callback)
			where TTo : class, IVendingMachineState
		{
			throw new NotImplementedException();
		}
	}

    public interface IFromBuilder<TFrom>
        where TFrom : class, IVendingMachineState
    {
		IOnBuilder<TFrom, TOn> On<TOn>()
			where TOn : class, IVendingMachineCommand;
    }

    private sealed class FromBuilder<TFrom>(VendingMachine vendingMachine) : IFromBuilder<TFrom>
        where TFrom : class, IVendingMachineState
    {
		public IOnBuilder<TFrom, TOn> On<TOn>()
			where TOn : class, IVendingMachineCommand
		{
			return new OnBuilder<TFrom, TOn>(vendingMachine);
		}
    }

    private VendingMachine() { }

    public static VendingMachine Create()
    {
        var vendingMachine = new VendingMachine();

        vendingMachine._states.Add(new Idle());

        return vendingMachine;
    }

    public IFromBuilder<TFrom> From<TFrom>()
        where TFrom : class, IVendingMachineState
    {
        return new FromBuilder<TFrom>(this);
    }

    public Task ExecuteAsync(IVendingMachineCommand command)
    {
		throw new NotImplementedException();
    }
}

internal static class Program
{
    public static async Task Main()
    {
        var vendingMachine = VendingMachine.Create();

		vendingMachine.From<ICanInsertCoin>()
			.On<InsertCoin>()
			.To((from, command) => new CoinInserted(Amount: command.Amount, TotalAmount: from.TotalAmount + command.Amount));

		vendingMachine.From<ICanChooseProduct>()
			.On<ChooseProduct>()
			.To((from, command) => new ProductChosen(ProductId: command.ProductId));

		vendingMachine.From<ICanReturnChange>()
			.On<ReturnChange>()
			.To((from, command) => new ChangeReturned(TotalAmount: from.TotalAmount));

		vendingMachine.From<ICanDispenseProduct>()
			.On<DispenseProduct>()
			.To((from, command) => new ProductDispensed(ProductId: from.ProductId));

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
