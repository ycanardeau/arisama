namespace Arisama;

internal abstract record VendingMachineStateContext
{
    public sealed record Idle : VendingMachineStateContext;

    public sealed record CoinInserted : VendingMachineStateContext;

    public sealed record ProductChosen : VendingMachineStateContext;

    public sealed record ChangeReturned : VendingMachineStateContext;

    public sealed record ProductDispensed : VendingMachineStateContext;
}

internal abstract record VendingMachineState
{
    public sealed record Idle : VendingMachineState<VendingMachineStateContext.Idle>;

    public sealed record CoinInserted : VendingMachineState<VendingMachineStateContext.CoinInserted>;

    public sealed record ProductChosen : VendingMachineState<VendingMachineStateContext.ProductChosen>;

    public sealed record ChangeReturned : VendingMachineState<VendingMachineStateContext.ChangeReturned>;

    public sealed record ProductDispensed : VendingMachineState<VendingMachineStateContext.ProductDispensed>;
}

internal abstract record VendingMachineState<TContext> : VendingMachineState
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

        vendingMachine._states.Add(new VendingMachineState.Idle());

        return vendingMachine;
    }

    public void InsertCoin(int amount)
    {
        FromTo<VendingMachineState.Idle, VendingMachineState.CoinInserted>(from => new VendingMachineState.CoinInserted());
    }

    public void ChooseProduct()
    {
        FromTo<VendingMachineState.CoinInserted, VendingMachineState.ProductChosen>(from => new VendingMachineState.ProductChosen());
    }

    public void ReturnChange()
    {
        FromTo<VendingMachineState.CoinInserted, VendingMachineState.Idle>(from => new VendingMachineState.Idle());
    }

    public void DispenseProduct()
    {
        FromTo<VendingMachineState.ProductChosen, VendingMachineState.ProductDispensed>(from => new VendingMachineState.ProductDispensed());
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
