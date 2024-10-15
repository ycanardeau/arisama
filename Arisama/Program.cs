namespace Arisama;

using static VendingMachineState;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
internal readonly partial struct Coin
{
    public static Coin operator +(Coin left, Coin right) => new(left.Value + right.Value);
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
internal readonly partial struct ProductId;

internal interface IVendingMachineState;

internal abstract record VendingMachineState : IVendingMachineState
{
    public interface ICanInsertCoin : IVendingMachineState
    {
        Coin TotalAmount { get; }
    }

    public interface ICanChooseProduct : IVendingMachineState;

    public interface ICanReturnChange : IVendingMachineState;

    public interface ICanDispenseProduct : IVendingMachineState
    {
        ProductId ProductId { get; }
    }

    public sealed record Idle : VendingMachineState, ICanInsertCoin
    {
        public Coin TotalAmount { get; } = Coin.Empty;
    }

    public sealed record CoinInserted(Coin Amount, Coin TotalAmount) : VendingMachineState, ICanInsertCoin, ICanChooseProduct, ICanReturnChange;

    public sealed record ProductChosen(ProductId ProductId) : VendingMachineState, ICanDispenseProduct;

    public sealed record ChangeReturned : VendingMachineState;

    public sealed record ProductDispensed(ProductId ProductId) : VendingMachineState;
}

internal interface IVendingMachineCommand
{
    Task ExecuteAsync(VendingMachine vendingMachine);
}

internal sealed record InsertCoinVendingMachineCommand(Coin Amount) : IVendingMachineCommand
{
    public Task ExecuteAsync(VendingMachine vendingMachine)
    {
        vendingMachine.FromTo<ICanInsertCoin, CoinInserted>(from => new CoinInserted(Amount: Amount, TotalAmount: from.TotalAmount + Amount));
        return Task.CompletedTask;
    }
}

internal sealed record ChooseProductVendingMachineCommand : IVendingMachineCommand
{
    public Task ExecuteAsync(VendingMachine vendingMachine)
    {
        vendingMachine.FromTo<ICanChooseProduct, ProductChosen>(from => new ProductChosen(ProductId: new(1)));
        return Task.CompletedTask;
    }
}

internal sealed record ReturnChangeVendingMachineCommand : IVendingMachineCommand
{
    public Task ExecuteAsync(VendingMachine vendingMachine)
    {
        vendingMachine.FromTo<ICanReturnChange, Idle>(from => new Idle());
        return Task.CompletedTask;
    }
}

internal sealed record DispenseProductVendingMachineCommand : IVendingMachineCommand
{
    public Task ExecuteAsync(VendingMachine vendingMachine)
    {
        vendingMachine.FromTo<ICanDispenseProduct, ProductDispensed>(from => new ProductDispensed(ProductId: from.ProductId));
        return Task.CompletedTask;
    }
}

internal class VendingMachine
{
    private readonly List<VendingMachineState> _states = [];
    public IReadOnlyCollection<VendingMachineState> States => _states.AsReadOnly();

    private VendingMachine() { }

    public static VendingMachine Create()
    {
        var vendingMachine = new VendingMachine();

        vendingMachine._states.Add(new Idle());

        return vendingMachine;
    }

    public void FromTo<TFrom, TTo>(Func<TFrom, TTo> callback)
        where TFrom : class, IVendingMachineState
        where TTo : VendingMachineState
    {
        if (States.Last() is not TFrom from)
        {
            Console.WriteLine($"Invalid transition from {typeof(TFrom).Name} to {typeof(TTo).Name}.");
            return;
        }

        Console.WriteLine($"Transitioning from {typeof(TFrom).Name} (Context: {from}).");

        var to = callback(from);
        _states.Add(to);

        Console.WriteLine($"Transitioned to {typeof(TTo).Name} (Context: {to}).");
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
            new InsertCoinVendingMachineCommand(Amount: new(100)),
            new InsertCoinVendingMachineCommand(Amount: new(50)),
            new InsertCoinVendingMachineCommand(Amount: new(10)),
            new ChooseProductVendingMachineCommand(),
            new DispenseProductVendingMachineCommand(),
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
