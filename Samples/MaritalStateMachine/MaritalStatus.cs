using Aigamo.Arisama;

namespace MaritalStateMachine;

internal abstract record MaritalStatus : IState;

internal sealed record Single : MaritalStatus
	, ICanDecease
	, ICanMarry
;

internal sealed record Married : MaritalStatus
	, ICanDecease
	, ICanDivorce
	, ICanBecomeWidowed
;

internal sealed record Divorced : MaritalStatus
	, ICanDecease
	, ICanMarry
;

internal sealed record Widowed : MaritalStatus
	, ICanDecease
	, ICanMarry
;

internal sealed record Deceased : MaritalStatus
;
