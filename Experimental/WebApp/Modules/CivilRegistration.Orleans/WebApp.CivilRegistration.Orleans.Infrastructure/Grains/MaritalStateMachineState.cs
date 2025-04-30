namespace WebApp.CivilRegistration.Orleans.Infrastructure.Grains;

internal interface IMaritalTransition
{
	int Version { get; }
}

internal interface ICanMarry : IMaritalTransition;

internal interface ICanDivorce : IMaritalTransition;

internal interface ICanBecomeWidowed : IMaritalTransition;

internal interface ICanDecease : IMaritalTransition;

[GenerateSerializer]
[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.MaritalStatus")]
internal abstract record MaritalStatus
{
	[Id(0)]
	public required int Version { get; init; }
}

[GenerateSerializer]
[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Single")]
internal sealed record Single : MaritalStatus
	, ICanDecease
	, ICanMarry;

[GenerateSerializer]
[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Married")]
internal sealed record Married : MaritalStatus
	, ICanDecease
	, ICanDivorce
	, ICanBecomeWidowed;

[GenerateSerializer]
[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Divorced")]
internal sealed record Divorced : MaritalStatus
	, ICanDecease
	, ICanMarry;

[GenerateSerializer]
[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Widowed")]
internal sealed record Widowed : MaritalStatus
	, ICanDecease
	, ICanMarry;

[GenerateSerializer]
[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.Deceased")]
internal sealed record Deceased : MaritalStatus;

[GenerateSerializer]
[Alias("WebApp.CivilRegistration.Orleans.Infrastructure.Grains.MaritalStateMachineState")]
internal sealed record MaritalStateMachineState
{
	[Id(0)]
	public List<MaritalStatus> States { get; init; } = [];
}
