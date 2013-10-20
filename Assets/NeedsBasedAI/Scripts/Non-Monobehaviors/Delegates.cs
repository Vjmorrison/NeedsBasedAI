using System;

/// <summary>
/// Defines a Lambda Function that takes a raw Need value from a need and converts it to a processed value as required by the actor.
/// </summary>
/// <param name="rawNeedValue">The raw need utility</param>
/// <returns>A converted, normalized utility value as defined by the lambda delegate</returns>
public delegate float UtilityDelegate(float normalizedNeedValue);

/// <summary>
/// Decays the current need value by a function as defined by the owning actor
/// </summary>
/// <param name="currentValue">the current need value</param>
/// <returns>a modified need value based on the results of the decay Lamda</returns>
public delegate float NeedDecayDelegate(float deltaTime, float currentValue);