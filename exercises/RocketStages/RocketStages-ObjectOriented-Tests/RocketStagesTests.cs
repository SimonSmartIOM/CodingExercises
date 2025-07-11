using System.Collections;
using RocketStages;

namespace RocketStages_Tests;

/// <summary>
/// Tests for the Object-Oriented solution to the Rocket Stages problem.
/// </summary>
public class Tests {
    [TestCaseSource(nameof(TestCases))]
    public void RocketStagesTests(List<Stage> availableStages, int expectedMaximumSpeed) {
        // ARRANGE
        IdealRocketCalculator idealRocketCalculator = new(availableStages);
        
        // ACT
        int actualMaximumAchievableSpeed = (int)Math.Round(idealRocketCalculator.MaximumAchievableSpeed);

        // ASSERT
        Assert.That(actualMaximumAchievableSpeed, Is.EqualTo(expectedMaximumSpeed));
    }

    public static IEnumerable TestCases {
        get {
            yield return new TestCaseData(new List<Stage> { new(9999, 1, 1000000, 1) }, 90); // Single stage, from the example.
            yield return new TestCaseData(new List<Stage> { new(100, 100, 5000, 10), new(200, 200, 8000, 20) }, 313); // Two stages, both used.
            yield return new TestCaseData(new List<Stage> { new(100, 100, 10000, 10), new(200, 200, 1000, 10) }, 595); // Second stage is not used.
            yield return new TestCaseData(new List<Stage> { new(50, 50, 4000, 5), new(80, 80, 6000, 8), new(120, 100, 9000, 10) }, 747); // Three stages, all used.
        }
    }
}