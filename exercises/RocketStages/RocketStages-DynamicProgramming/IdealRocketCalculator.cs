namespace RocketStages;

/// <summary>
/// Constants used throughout the rocket simulation.
/// </summary>
public static class Constants {
    public const double Gravity = 9.8; // Gravitational acceleration in m/s^2
    public const int MaximumAllowedMass = 10000; // Total mass cannot exceed 10000
}

public class IdealRocketCalculator {
    private readonly decimal[] _possibleMaxSpeeds; // Array to store the maximum speed achievable for each possible total mass of the rocket.

    public IdealRocketCalculator(List<Stage> availableStages) {
        _possibleMaxSpeeds = new decimal[Constants.MaximumAllowedMass+1];
        
        //_possibleMaxSpeeds = new(Constants.MaximumAllowedMass); // Initialize the list with room for all possible masses up to the maximum.
        Array.Fill(_possibleMaxSpeeds, 0); // Initialize all possible speeds to 0.

        // To determine the maximum possible speed, we will use a dynamic programming approach.
        // For each stage, we will calculate the maximum speed achievable for each possible total mass of the rocket (from zero to the maximum allowed).
        foreach (Stage stage in availableStages) { // Loop through every stage
            for (int currentMass = Constants.MaximumAllowedMass; currentMass >= 0; currentMass--) { // Iterate through all possible masses from maximum to zero.
                
                uint rocketMassWithStage = (uint)currentMass + stage.TotalMass; // Get the mass of the rocket after adding the stage.
                bool rocketValid = rocketMassWithStage <= Constants.MaximumAllowedMass && stage.Thrust > (rocketMassWithStage * Constants.Gravity); // Check if the rocket is valid with this stage - not too heavy and capable of lifting its own weight.
                
                if (rocketValid) {
                    ref decimal currentMaxSpeed = ref _possibleMaxSpeeds[rocketMassWithStage]; // Get the current maximum speed for this mass of the rocket. We get this by reference so we can modify it directly.
                    
                    // This is the Tsiolkovsky rocket equation (https://en.wikipedia.org/wiki/Tsiolkovsky_rocket_equation), which calculates the change in velocity based on the mass of the rocket and the fuel consumed.
                    double burnTime = (double)stage.FuelMass / stage.FuelConsumption;
                    decimal velocity = (decimal)(stage.Thrust * (Math.Log(rocketMassWithStage) - Math.Log(currentMass + stage.EmptyMass)) / stage.FuelConsumption - burnTime * (double)Constants.Gravity);

                    decimal newMaximumSpeed = _possibleMaxSpeeds[currentMass] + velocity; // Calculate the new maximum speed by adding the acceleration from this stage to the previous maximum speed for the current mass.
                    currentMaxSpeed = Math.Max(currentMaxSpeed, newMaximumSpeed); // Update the maximum speed for this mass if the new speed is greater.
                }
            }
        }
    }
    
    /// <summary>
    /// The maximum possible speed of the rocket is the maximum speed achievable with the lightest rocket that can still lift its own weight, as calculated above.
    /// </summary>
    public decimal MaximumPossibleSpeed => _possibleMaxSpeeds.Max();
}