namespace RocketStages;

/// <summary>
/// This class represents a single stage that can be included in a rocket.
/// </summary>
public class Stage(uint emptyMass, uint fuelMass, uint thrust, uint fuelConsumption) {
    /// <summary>
    /// The mass of the stage, in kilograms, when it is empty (without fuel).
    /// </summary>
    public readonly uint EmptyMass = emptyMass;

    /// <summary>
    /// The mass of the fuel, in kilograms, in the stage.
    /// </summary>
    public readonly uint FuelMass = fuelMass;

    /// <summary>
    /// The total mass of the stage, including fuel.
    /// </summary>
    public uint TotalMass => EmptyMass + FuelMass;

    /// <summary>
    /// The number of seconds of fuel available in the stage.
    /// </summary>
    public uint SecondsOfFuel => FuelMass / FuelConsumption;

    /// <summary>
    /// The thrust in newtons provided by the engine in the stage.
    /// </summary>
    public readonly uint Thrust = thrust;

    /// <summary>
    /// The fuel consumption, in kilograms per second, of the stage.
    /// </summary>
    public readonly uint FuelConsumption = fuelConsumption;
}