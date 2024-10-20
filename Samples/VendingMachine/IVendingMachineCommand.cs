using Aigamo.Arisama;
using static VendingMachine.IVendingMachineState;
using static VendingMachine.IVendingMachineTransition;

namespace VendingMachine;

interface IVendingMachineCommand : ICommand
{
	public sealed record InsertCoin(Coin Amount) : IVendingMachineCommand, ICommand<ICanInsertCoin, CoinInserted>
	{
		public CoinInserted Execute(ICanInsertCoin from)
		{
			return new CoinInserted(Amount: Amount, TotalAmount: from.TotalAmount + Amount);
		}
	}

	public sealed record ChooseProduct(ProductId ProductId) : IVendingMachineCommand, ICommand<ICanChooseProduct, ProductChosen>
	{
		public ProductChosen Execute(ICanChooseProduct from)
		{
			return new ProductChosen(ProductId: ProductId);
		}
	}

	public sealed record ReturnChange : IVendingMachineCommand, ICommand<ICanReturnChange, ChangeReturned>
	{
		public ChangeReturned Execute(ICanReturnChange from)
		{
			return new ChangeReturned(TotalAmount: from.TotalAmount);
		}
	}

	public sealed record DispenseProduct : IVendingMachineCommand, ICommand<ICanDispenseProduct, ProductDispensed>
	{
		public ProductDispensed Execute(ICanDispenseProduct from)
		{
			return new ProductDispensed(ProductId: from.ProductId);
		}
	}
}
