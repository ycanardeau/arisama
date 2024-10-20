using Aigamo.Arisama;

namespace VendingMachine;

interface IVendingMachineTransition : ITransition
{
	public interface ICanInsertCoin : IVendingMachineTransition
	{
		Coin TotalAmount { get; }
	}

	public interface ICanChooseProduct : IVendingMachineTransition;

	public interface ICanReturnChange : IVendingMachineTransition
	{
		Coin TotalAmount { get; }
	}

	public interface ICanDispenseProduct : IVendingMachineTransition
	{
		ProductId ProductId { get; }
	}
}
