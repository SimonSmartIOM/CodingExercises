namespace RocketStages;

/// <summary>
/// This class contains the business logic for calculating the maximum achievable speed of a rocket.
/// Wrapping this in a class allows for easy test automation!
/// </summary>
public class IdealRocketCalculator {
    private readonly List<Stage> _availableStages;
    private double _maxSpeed; // Utility variable to store the maximum speed found during the calculation.

    /// <summary>
    /// Constructor for the IdealRocketCalculator.
    /// </summary>
    /// <param name="availableStages">The available stages to build the rocket.</param>
    public IdealRocketCalculator(List<Stage> availableStages) {
        _availableStages = availableStages;
    }

    public double MaximumAchievableSpeed {
        get {
            _maxSpeed = 0; // Reset the maximum speed before calculating
            GenerateCombinations(new Rocket(new()), 0, 0);
            return _maxSpeed;
        }
    }

    /// <summary>
    /// Iterate through all possible combinations of rocket stages to find the maximum achievable speed.
    /// </summary>
    /// <param name="rocket"></param>
    /// <param name="previousMaximumSpeed"></param>
    /// <param name="index"></param>
    private void GenerateCombinations(Rocket rocket, double previousMaximumSpeed, int index) {
        _maxSpeed = Math.Max(_maxSpeed, rocket.MaximumAchievableSpeed);
        if (index != _availableStages.Count) // Only continue if there are more stages to consider.
        {
            // First, try including the next stage.
            Rocket rocketWithStage = new(rocket.Stages);
            rocketWithStage.Stages.Add(_availableStages[index]);
            if (rocketWithStage.Valid) // Ignore invalid rockets (e.g. too heavy or not enough thrust to lift its own weight).
            {
                double maximumSpeed = rocketWithStage.MaximumAchievableSpeed; // Calculate the speed of the rocket with the stage included
                if (maximumSpeed >= previousMaximumSpeed) // Only continue iterating if adding this stage causes speed to increase.
                {
                    //_maxSpeed = Math.Max(_maxSpeed, maximumSpeed);
                    GenerateCombinations(rocketWithStage, maximumSpeed, index + 1); // Continue to the next stage with the current stage included.
                }
            }

            // Now try continuing to the next stage, without including this one.
            Rocket rocketWithoutStage = new(rocket.Stages);
            GenerateCombinations(rocketWithoutStage, previousMaximumSpeed, index + 1);
        }
    }
}