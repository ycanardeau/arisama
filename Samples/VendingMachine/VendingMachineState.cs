using Aigamo.Arisama;
using StronglyTypedIds;

namespace VendingMachine;

[StronglyTypedId(Template.Int)]
readonly partial struct Coin
{
	public static Coin operator +(Coin left, Coin right) => new(left.Value + right.Value);
}

[StronglyTypedId(Template.Int)]
readonly partial struct ProductId;

internal abstract record VendingMachineState : IState;

internal sealed record Idle : VendingMachineState
	, ICanInsertCoin
{
	public Coin TotalAmount { get; } = Coin.Empty;
}

internal sealed record CoinInserted(Coin Amount, Coin TotalAmount) : VendingMachineState
	, ICanInsertCoin
	, ICanChooseProduct
	, ICanReturnChange;

internal sealed record ProductChosen(ProductId ProductId) : VendingMachineState
	, ICanDispenseProduct;

internal sealed record ChangeReturned(Coin TotalAmount) : VendingMachineState;

internal sealed record ProductDispensed(ProductId ProductId) : VendingMachineState;
