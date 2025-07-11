namespace RocketStages;

/// <summary>
/// Constants used throughout the rocket simulation.
/// </summary>
public static class Constants {
    public const double Gravity = 9.8; // Constant force of gravity.
    public const int MaximumAllowedMass = 10000; // Maximum allowed mass of the rocket.
}

/// <summary>
/// This class represents a rocket, consisting of several stages.
/// </summary>
public class Rocket {
    /// <summary>
    /// The stages in the rocket.
    /// </summary>
    public readonly List<Stage> Stages;
    
    /// <summary>
    /// Constructor for the Rocket class.
    /// This allows new rockets to be created based on a previous rocket's stages, assisting with creating all possible combinations.
    /// </summary>
    /// <param name="stages"></param>
    public Rocket(List<Stage> stages)
    {
        Stages = new List<Stage>(stages);
    }

    /// <summary>
    /// The acceleration of the rocket at launch.
    /// Acceleration below 1 indicates that the rocket does not have enough thrust to lift its own weight, which is considered invalid in this problem.
    /// </summary>
    private double AccelerationAtLaunch => Stages.LastOrDefault()?.Thrust / (TotalMass * Constants.Gravity) ?? 0;
    
    /// <summary>
    /// Simple validation check for a rocket - if its mass exceeds 10,000KG or its TWR is not at least 1, the rocket is invalid and we shouldn't try to calculate its Delta V.
    /// </summary>
    public bool Valid => TotalMass <= Constants.MaximumAllowedMass && AccelerationAtLaunch > 0;

    /// <summary>
    /// The total mass of all stages (including fuel).
    /// </summary>
    private double TotalMass => Stages.Sum(x => x.TotalMass);

    /// <summary>
    /// The maximum speed in Meters-per-Second that the rocket will achieve when all stages have burned.
    /// This iterates through each stage, starting with the bottom stage, and simulates the rocket's flight using the Tsiolkovsky rocket equation.
    /// </summary>
    public double MaximumAchievableSpeed {
        get {
            double velocity = 0;
            double totalMass = TotalMass;

            for (int x = Stages.Count - 1; x >= 0; x--) // Iterate through stages in reverse, because the bottom stage fires first!
            {
                Stage stage = Stages[x];
                if (stage.Thrust / (totalMass * Constants.Gravity) < 1) return 0; // If the acceleration is less than 1 when a stage ignites, we have reached an invalid state and should just exit rather than waste time continuing
                
                // This is the Tsiolkovsky rocket equation (https://en.wikipedia.org/wiki/Tsiolkovsky_rocket_equation), which calculates the change in velocity based on the mass of the rocket and the fuel consumed.
                velocity += ((stage.Thrust * (Math.Log(totalMass) - Math.Log(totalMass - stage.FuelMass))) / stage.FuelConsumption) - (stage.SecondsOfFuel * Constants.Gravity);
                
                totalMass -= stage.TotalMass; // Remove the stage after it has burned its fuel and has been discarded.
            }

            return Convert.ToUInt32(Math.Round(velocity, 0));
        }
    }
}