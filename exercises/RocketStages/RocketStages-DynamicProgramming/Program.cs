using RocketStages;

// Read the inputs from the console
int numberOfStages = int.Parse(Console.ReadLine()!);

List<Stage> availableStages = new();
for (int i = 0; i < numberOfStages; i++) {
    string[] stageValues = Console.ReadLine().Split();
    uint emptyMass = uint.Parse(stageValues[0]);
    uint fuelMass = uint.Parse(stageValues[1]);
    uint thrust = uint.Parse(stageValues[2]);
    uint fuelConsumption = uint.Parse(stageValues[3]);
    availableStages.Add(new(emptyMass, fuelMass, thrust, fuelConsumption));
}

// Use the IdealRocketCalculator to find the maximum achievable speed
IdealRocketCalculator idealRocketCalculator = new(availableStages);
Console.WriteLine(Math.Round(idealRocketCalculator.MaximumPossibleSpeed));