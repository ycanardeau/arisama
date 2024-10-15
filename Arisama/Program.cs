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
    Task ExecuteAsync(VendingMachine vendingMachine);

    public sealed record InsertCoin(Coin Amount) : IVendingMachineCommand
    {
        public Task ExecuteAsync(VendingMachine vendingMachine)
        {
            vendingMachine.From<ICanInsertCoin>()
                .To(from => new CoinInserted(Amount: Amount, TotalAmount: from.TotalAmount + Amount));

            return Task.CompletedTask;
        }
    }

    public sealed record ChooseProduct(ProductId ProductId) : IVendingMachineCommand
    {
        public Task ExecuteAsync(VendingMachine vendingMachine)
        {
            vendingMachine.From<ICanChooseProduct>()
                .To(from => new ProductChosen(ProductId: ProductId));

            return Task.CompletedTask;
        }
    }

    public sealed record ReturnChange : IVendingMachineCommand
    {
        public Task ExecuteAsync(VendingMachine vendingMachine)
        {
            vendingMachine.From<ICanReturnChange>()
                .To(from => new ChangeReturned(TotalAmount: from.TotalAmount));

            return Task.CompletedTask;
        }
    }

    public sealed record DispenseProduct : IVendingMachineCommand
    {
        public Task ExecuteAsync(VendingMachine vendingMachine)
        {
            vendingMachine.From<ICanDispenseProduct>()
                .To(from => new ProductDispensed(ProductId: from.ProductId));

            return Task.CompletedTask;
        }
    }
}

internal class VendingMachine
{
    public interface IFromBuilder<TFrom>
        where TFrom : class, IVendingMachineState
    {
        void To<TTo>(Func<TFrom, TTo> callback)
            where TTo : class, IVendingMachineState;
    }

    private sealed class FromBuilder<TFrom>(VendingMachine vendingMachine) : IFromBuilder<TFrom>
        where TFrom : class, IVendingMachineState
    {
        public void To<TTo>(Func<TFrom, TTo> callback)
            where TTo : class, IVendingMachineState
        {
            var latestState = vendingMachine.States.Last();
            if (latestState is not TFrom from)
            {
                Console.WriteLine($"Invalid transition from {latestState.GetType().Name} to {typeof(TTo).Name}.");
                return;
            }

            Console.WriteLine($"Transitioning from {typeof(TFrom).Name} (Context: {from}).");

            var to = callback(from);
            vendingMachine._states.Add(to);

            Console.WriteLine($"Transitioned to {typeof(TTo).Name} (Context: {to}).");
        }
    }

    private readonly List<IVendingMachineState> _states = [];
    public IReadOnlyCollection<IVendingMachineState> States => _states.AsReadOnly();

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
        return command.ExecuteAsync(this);
    }
}

internal static class Program
{
    public static async Task Main()
    {
        var vendingMachine = VendingMachine.Create();

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
