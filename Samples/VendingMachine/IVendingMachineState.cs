using Aigamo.Arisama;
using StronglyTypedIds;
using static VendingMachine.IVendingMachineTransition;

namespace VendingMachine;

[StronglyTypedId(Template.Int)]
readonly partial struct Coin
{
	public static Coin operator +(Coin left, Coin right) => new(left.Value + right.Value);
}

[StronglyTypedId(Template.Int)]
readonly partial struct ProductId;

interface IVendingMachineState : IState
{
	public sealed record Idle : IVendingMachineState
		, ICanInsertCoin
	{
		public Coin TotalAmount { get; } = Coin.Empty;
	}

	public sealed record CoinInserted(Coin Amount, Coin TotalAmount) : IVendingMachineState
		, ICanInsertCoin
		, ICanChooseProduct
		, ICanReturnChange;

	public sealed record ProductChosen(ProductId ProductId) : IVendingMachineState
		, ICanDispenseProduct;

	public sealed record ChangeReturned(Coin TotalAmount) : IVendingMachineState;

	public sealed record ProductDispensed(ProductId ProductId) : IVendingMachineState;
}
