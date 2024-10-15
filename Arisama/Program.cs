namespace Arisama;

using static VendingMachineState;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
internal readonly partial struct Coin
{
    public static Coin operator +(Coin left, Coin right) => new(left.Value + right.Value);
}

internal abstract record VendingMachineStateContext
{
    public sealed record Idle : VendingMachineStateContext;

    public sealed record CoinInserted(Coin Amount, Coin TotalAmount) : VendingMachineStateContext;

    public sealed record ProductChosen : VendingMachineStateContext;

    public sealed record ChangeReturned : VendingMachineStateContext;

    public sealed record ProductDispensed : VendingMachineStateContext;
}

internal interface IVendingMachineState
{
    VendingMachineStateContext ContextBase { get; }
}

internal abstract record VendingMachineState(VendingMachineStateContext ContextBase) : IVendingMachineState
{
    public interface ICanInsertCoin : IVendingMachineState
    {
        Coin TotalAmount { get; }
    }

    public interface ICanChooseProduct : IVendingMachineState;

    public interface ICanReturnChange : IVendingMachineState;

    public interface ICanDispenseProduct : IVendingMachineState;

    public sealed record Idle(VendingMachineStateContext.Idle Context) : VendingMachineState<VendingMachineStateContext.Idle>(Context), ICanInsertCoin
    {
        Coin ICanInsertCoin.TotalAmount => Coin.Empty;
    }

    public sealed record CoinInserted(VendingMachineStateContext.CoinInserted Context) : VendingMachineState<VendingMachineStateContext.CoinInserted>(Context), ICanInsertCoin, ICanChooseProduct, ICanReturnChange
    {
        Coin ICanInsertCoin.TotalAmount => Context.TotalAmount;
    }

    public sealed record ProductChosen(VendingMachineStateContext.ProductChosen Context) : VendingMachineState<VendingMachineStateContext.ProductChosen>(Context), ICanDispenseProduct;

    public sealed record ChangeReturned(VendingMachineStateContext.ChangeReturned Context) : VendingMachineState<VendingMachineStateContext.ChangeReturned>(Context);

    public sealed record ProductDispensed(VendingMachineStateContext.ProductDispensed Context) : VendingMachineState<VendingMachineStateContext.ProductDispensed>(Context);
}

internal abstract record VendingMachineState<TContext>(TContext Context) : VendingMachineState(Context)
    where TContext : VendingMachineStateContext;

internal interface IVendingMachineCommand
{
    Task ExecuteAsync(VendingMachine vendingMachine);
}

internal sealed record InsertCoinVendingMachineCommand(Coin Amount) : IVendingMachineCommand
{
    public Task ExecuteAsync(VendingMachine vendingMachine)
    {
        vendingMachine.FromTo<ICanInsertCoin, CoinInserted>(from => new CoinInserted(new(Amount: Amount, TotalAmount: from.TotalAmount + Amount)));
        return Task.CompletedTask;
    }
}

internal sealed record ChooseProductVendingMachineCommand : IVendingMachineCommand
{
    public Task ExecuteAsync(VendingMachine vendingMachine)
    {
        vendingMachine.FromTo<ICanChooseProduct, ProductChosen>(from => new ProductChosen(new()));
        return Task.CompletedTask;
    }
}

internal sealed record ReturnChangeVendingMachineCommand : IVendingMachineCommand
{
    public Task ExecuteAsync(VendingMachine vendingMachine)
    {
        vendingMachine.FromTo<ICanReturnChange, Idle>(from => new Idle(new()));
        return Task.CompletedTask;
    }
}

internal sealed record DispenseProductVendingMachineCommand : IVendingMachineCommand
{
    public Task ExecuteAsync(VendingMachine vendingMachine)
    {
        vendingMachine.FromTo<ICanDispenseProduct, ProductDispensed>(from => new ProductDispensed(new()));
        return Task.CompletedTask;
    }
}

internal class VendingMachine
{
    private readonly List<IVendingMachineState> _states = [];
    public IReadOnlyCollection<IVendingMachineState> States => _states.AsReadOnly();

    private VendingMachine() { }

    public static VendingMachine Create()
    {
        var vendingMachine = new VendingMachine();

        vendingMachine._states.Add(new Idle(new()));

        return vendingMachine;
    }

    public void FromTo<TFrom, TTo>(Func<TFrom, TTo> callback)
        where TFrom : class, IVendingMachineState
        where TTo : class, IVendingMachineState
    {
        if (States.Last() is not TFrom from)
        {
            Console.WriteLine($"Invalid transition from {typeof(TFrom).Name} to {typeof(TTo).Name}.");
            return;
        }

        Console.WriteLine($"Transitioning from {typeof(TFrom).Name} (Context: {from.ContextBase}).");

        var to = callback(from);
        _states.Add(to);

        Console.WriteLine($"Transitioned to {typeof(TTo).Name} (Context: {to.ContextBase}).");
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
        }
    }
}
