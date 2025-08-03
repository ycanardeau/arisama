using Aigamo.Arisama;

namespace MaritalStateMachine;

internal interface IMaritalTransition : ITransition;

internal interface ICanMarry : IMaritalTransition;

internal interface ICanDivorce : IMaritalTransition;

internal interface ICanBecomeWidowed : IMaritalTransition;

internal interface ICanDecease : IMaritalTransition;
