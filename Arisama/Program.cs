namespace Arisama;

internal abstract record VendingMachineStateContext
{
    public sealed record Idle : VendingMachineStateContext;

    public sealed record CoinInserted(int Amount) : VendingMachineStateContext;

    public sealed record ProductChosen : VendingMachineStateContext;

    public sealed record ChangeReturned : VendingMachineStateContext;

    public sealed record ProductDispensed : VendingMachineStateContext;
}

internal abstract record VendingMachineState
{
    public sealed record Idle(VendingMachineStateContext.Idle Context) : VendingMachineState<VendingMachineStateContext.Idle>(Context);

    public sealed record CoinInserted(VendingMachineStateContext.CoinInserted Context) : VendingMachineState<VendingMachineStateContext.CoinInserted>(Context);

    public sealed record ProductChosen(VendingMachineStateContext.ProductChosen Context) : VendingMachineState<VendingMachineStateContext.ProductChosen>(Context);

    public sealed record ChangeReturned(VendingMachineStateContext.ChangeReturned Context) : VendingMachineState<VendingMachineStateContext.ChangeReturned>(Context);

    public sealed record ProductDispensed(VendingMachineStateContext.ProductDispensed Context) : VendingMachineState<VendingMachineStateContext.ProductDispensed>(Context);
}

internal abstract record VendingMachineState<TContext>(TContext Context) : VendingMachineState
    where TContext : VendingMachineStateContext;

internal class VendingMachine
{
    private readonly List<VendingMachineState> _states = [];
    public IReadOnlyCollection<VendingMachineState> States => _states.AsReadOnly();

    private VendingMachine() { }

    private void FromTo<TFrom, TTo>(Func<TFrom, TTo> callback)
        where TFrom : VendingMachineState
        where TTo : VendingMachineState
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
        FromTo<VendingMachineState.Idle, VendingMachineState.CoinInserted>(from => new VendingMachineState.CoinInserted(new(Amount: amount)));
    }

    public void ChooseProduct()
    {
        FromTo<VendingMachineState.CoinInserted, VendingMachineState.ProductChosen>(from => new VendingMachineState.ProductChosen(new()));
    }

    public void ReturnChange()
    {
        FromTo<VendingMachineState.CoinInserted, VendingMachineState.Idle>(from => new VendingMachineState.Idle(new()));
    }

    public void DispenseProduct()
    {
        FromTo<VendingMachineState.ProductChosen, VendingMachineState.ProductDispensed>(from => new VendingMachineState.ProductDispensed(new()));
    }
}

internal static class Program
{
    public static void Main()
    {
        var vendingMachine = VendingMachine.Create();

        vendingMachine.InsertCoin(amount: 100);

        vendingMachine.ChooseProduct();

        vendingMachine.DispenseProduct();
    }
}
