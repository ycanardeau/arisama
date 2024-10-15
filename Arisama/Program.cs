namespace Arisama;

internal abstract record VendingMachineStateContext
{
    public sealed record Idle : VendingMachineStateContext;

    public sealed record CoinInserted(int Amount) : VendingMachineStateContext;

    public sealed record ProductChosen : VendingMachineStateContext;

    public sealed record ChangeReturned : VendingMachineStateContext;

    public sealed record ProductDispensed : VendingMachineStateContext;
}

internal interface IVendingMachineState;

internal abstract record VendingMachineState : IVendingMachineState
{
    public interface ICanInsertCoin : IVendingMachineState;

    public interface ICanChooseProduct : IVendingMachineState;

    public interface ICanReturnChange : IVendingMachineState;

    public interface ICanDispenseProduct : IVendingMachineState;

    public sealed record Idle(VendingMachineStateContext.Idle Context) : VendingMachineState<VendingMachineStateContext.Idle>(Context), ICanInsertCoin;

    public sealed record CoinInserted(VendingMachineStateContext.CoinInserted Context) : VendingMachineState<VendingMachineStateContext.CoinInserted>(Context), ICanInsertCoin, ICanChooseProduct, ICanReturnChange;

    public sealed record ProductChosen(VendingMachineStateContext.ProductChosen Context) : VendingMachineState<VendingMachineStateContext.ProductChosen>(Context), ICanDispenseProduct;

    public sealed record ChangeReturned(VendingMachineStateContext.ChangeReturned Context) : VendingMachineState<VendingMachineStateContext.ChangeReturned>(Context);

    public sealed record ProductDispensed(VendingMachineStateContext.ProductDispensed Context) : VendingMachineState<VendingMachineStateContext.ProductDispensed>(Context);
}

internal abstract record VendingMachineState<TContext>(TContext Context) : VendingMachineState
    where TContext : VendingMachineStateContext;

internal class VendingMachine
{
    private readonly List<IVendingMachineState> _states = [];
    public IReadOnlyCollection<IVendingMachineState> States => _states.AsReadOnly();

    private VendingMachine() { }

    private void FromTo<TFrom, TTo>(Func<TFrom, TTo> callback)
        where TFrom : class, IVendingMachineState
        where TTo : class, IVendingMachineState
    {
        Console.WriteLine($"Transitioning from {typeof(TFrom).Name} to {typeof(TTo).Name}.");

        if (States.Last() is not TFrom from)
        {
            Console.WriteLine($"Invalid transition from {typeof(TFrom).Name} to {typeof(TTo).Name}.");
            return;
        }

        _states.Add(callback(from));

        Console.WriteLine($"Transitioned from {typeof(TFrom).Name} to {typeof(TTo).Name}.");
    }

    public static VendingMachine Create()
    {
        var vendingMachine = new VendingMachine();

        vendingMachine._states.Add(new VendingMachineState.Idle(new()));

        return vendingMachine;
    }

    public void InsertCoin(int amount)
    {
        FromTo<VendingMachineState.ICanInsertCoin, VendingMachineState.CoinInserted>(from => new VendingMachineState.CoinInserted(new(Amount: amount)));
    }

    public void ChooseProduct()
    {
        FromTo<VendingMachineState.ICanChooseProduct, VendingMachineState.ProductChosen>(from => new VendingMachineState.ProductChosen(new()));
    }

    public void ReturnChange()
    {
        FromTo<VendingMachineState.ICanReturnChange, VendingMachineState.Idle>(from => new VendingMachineState.Idle(new()));
    }

    public void DispenseProduct()
    {
        FromTo<VendingMachineState.ICanDispenseProduct, VendingMachineState.ProductDispensed>(from => new VendingMachineState.ProductDispensed(new()));
    }
}

internal static class Program
{
    public static void Main()
    {
        var vendingMachine = VendingMachine.Create();

        vendingMachine.InsertCoin(amount: 100);

        vendingMachine.InsertCoin(amount: 50);

        vendingMachine.InsertCoin(amount: 10);

        vendingMachine.ChooseProduct();

        vendingMachine.DispenseProduct();
    }
}
