using Aigamo.Arisama;

namespace VendingMachine;

internal abstract record VendingMachineCommand : ICommand;

internal sealed record InsertCoin(Coin Amount) : VendingMachineCommand, ICommand<ICanInsertCoin, CoinInserted>
{
	public CoinInserted Execute(ICanInsertCoin from)
	{
		return new CoinInserted(Amount: Amount, TotalAmount: from.TotalAmount + Amount);
	}
}

internal sealed record ChooseProduct(ProductId ProductId) : VendingMachineCommand, ICommand<ICanChooseProduct, ProductChosen>
{
	public ProductChosen Execute(ICanChooseProduct from)
	{
		return new ProductChosen(ProductId: ProductId);
	}
}

internal sealed record ReturnChange : VendingMachineCommand, ICommand<ICanReturnChange, ChangeReturned>
{
	public ChangeReturned Execute(ICanReturnChange from)
	{
		return new ChangeReturned(TotalAmount: from.TotalAmount);
	}
}

internal sealed record DispenseProduct : VendingMachineCommand, ICommand<ICanDispenseProduct, ProductDispensed>
{
	public ProductDispensed Execute(ICanDispenseProduct from)
	{
		return new ProductDispensed(ProductId: from.ProductId);
	}
}
