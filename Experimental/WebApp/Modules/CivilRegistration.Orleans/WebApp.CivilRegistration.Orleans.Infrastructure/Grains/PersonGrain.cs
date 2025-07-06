using Nut.Results;
using WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Abstractions;

namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains;

internal class PersonGrain(
	[PersistentState(stateName: "person", storageName: "persons")]
	IPersistentState<PersonState> state
) : Grain, IPersonGrain
{
	private IMaritalStateMachineGrain _maritalStateMachineGrain = null!;

	public override Task OnActivateAsync(CancellationToken cancellationToken)
	{
		_maritalStateMachineGrain = GrainFactory.GetGrain<IMaritalStateMachineGrain>(this.GetPrimaryKey());
		return base.OnActivateAsync(cancellationToken);
	}

	public Task<Result> Initialize()
	{
		return _maritalStateMachineGrain.Initialize();
	}

	public Task<Result> Marry(Guid marryWith)
	{
		return _maritalStateMachineGrain.Marry(marryWith);
	}

	public Task<Result> Divorce()
	{
		return _maritalStateMachineGrain.Divorce();
	}

	public Task<Result> BecomeWidowed()
	{
		return _maritalStateMachineGrain.BecomeWidowed();
	}

	public Task<Result> Decease()
	{
		return _maritalStateMachineGrain.Decease();
	}
}
