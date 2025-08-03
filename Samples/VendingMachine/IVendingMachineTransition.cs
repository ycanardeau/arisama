using Aigamo.Arisama;

namespace VendingMachine;

internal interface IVendingMachineTransition : ITransition;

internal interface ICanInsertCoin : IVendingMachineTransition
{
	Coin TotalAmount { get; }
}

internal interface ICanChooseProduct : IVendingMachineTransition;

internal interface ICanReturnChange : IVendingMachineTransition
{
	Coin TotalAmount { get; }
}

internal interface ICanDispenseProduct : IVendingMachineTransition
{
	ProductId ProductId { get; }
}
