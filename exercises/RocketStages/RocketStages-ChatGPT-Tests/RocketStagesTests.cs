using System.Collections;

namespace RocketStages_ChatGPT_Tests;

/// <summary>
/// Tests for ChatGPT's solution to the Rocket Stages problem.
/// Note that ChatGPT's solution doesn't work, so these tests will fail.
/// </summary>
public class Tests {
    const double G = 9.8;
    const ulong MAX_MASS = 4294967295;

    public class RocketStage {
        public ulong EmptyMass;
        public ulong FuelMass;
        public ulong Thrust;
        public ulong Consumption;

        public RocketStage(ulong emptyMass, ulong fuelMass, ulong thrust, ulong consumption) {
            EmptyMass = emptyMass;
            FuelMass = fuelMass;
            Thrust = thrust;
            Consumption = consumption;
        }
    }

    /// <summary>
    /// ChatGPT didn't provide its code in a way that allows it to be tested directly, so we'll have to put the code here and tweak it for testing.
    /// </summary>
    /// <returns></returns>
    public int Implementation(List<RocketStage> stages) {
        double maxVelocity = 0;
        int n = stages.Count;
        
        // There are 2^n subsets, exclude 0 (empty set)
        for (int mask = 1; mask < (1 << n); mask++) {
            var selected = new List<RocketStage>();

            for (int i = 0; i < n; i++) {
                if ((mask & (1 << i)) != 0)
                    selected.Add(stages[i]);
            }

            selected.Reverse(); // simulate from bottom (last stage burns first)

            double velocity = 0;
            double totalMass = 0;

            foreach (var s in selected)
                totalMass += s.EmptyMass + s.FuelMass;

            if (totalMass > MAX_MASS)
                continue;

            bool valid = true;
            double currentMass = totalMass;

            foreach (var s in selected) {
                double time = (double)s.FuelMass / s.Consumption;
                double netForce = s.Thrust - currentMass * G;
                double acceleration = netForce / currentMass;

                if (acceleration < 0) {
                    valid = false;
                    break;
                }

                velocity += acceleration * time;

                currentMass -= s.FuelMass;
                currentMass -= s.EmptyMass;
            }

            if (valid)
                maxVelocity = Math.Max(maxVelocity, velocity);
        }
        
        return (int)Math.Round(maxVelocity);
    }
    
    [TestCaseSource(nameof(TestCases))]
    public void RocketStagesTests(List<RocketStage> availableStages, int expectedMaximumSpeed) {
        // ACT
        int result = Implementation(availableStages);

        // ASSERT
        Assert.That(result, Is.EqualTo(expectedMaximumSpeed));
    }

    public static IEnumerable TestCases {
        get {
            yield return new TestCaseData(new List<RocketStage> { new(9999, 1, 1000000, 1) }, 90); // Single stage, from the example.
            yield return new TestCaseData(new List<RocketStage> { new(100, 100, 5000, 10), new(200, 200, 8000, 20) }, 313); // Two stages, both used.
            yield return new TestCaseData(new List<RocketStage> { new(100, 100, 10000, 10), new(200, 200, 1000, 10) }, 595); // Second stage is not used.
            yield return new TestCaseData(new List<RocketStage> { new(50, 50, 4000, 5), new(80, 80, 6000, 8), new(120, 100, 9000, 10) }, 747); // Three stages, all used.
        }
    }
}