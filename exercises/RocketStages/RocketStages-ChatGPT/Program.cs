using System;
using System.Collections.Generic;

class RocketStage
{
    public ulong EmptyMass;
    public ulong FuelMass;
    public ulong Thrust;
    public ulong Consumption;

    public RocketStage(ulong emptyMass, ulong fuelMass, ulong thrust, ulong consumption)
    {
        EmptyMass = emptyMass;
        FuelMass = fuelMass;
        Thrust = thrust;
        Consumption = consumption;
    }
}

class Program
{
    const double G = 9.8;
    const ulong MAX_MASS = 4294967295;

    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var stages = new List<RocketStage>();

        for (int i = 0; i < n; i++)
        {
            var parts = Console.ReadLine().Split();
            ulong e = ulong.Parse(parts[0]);
            ulong f = ulong.Parse(parts[1]);
            ulong t = ulong.Parse(parts[2]);
            ulong c = ulong.Parse(parts[3]);
            stages.Add(new RocketStage(e, f, t, c));
        }

        double maxVelocity = 0;

        // There are 2^n subsets, exclude 0 (empty set)
        for (int mask = 1; mask < (1 << n); mask++)
        {
            var selected = new List<RocketStage>();

            for (int i = 0; i < n; i++)
            {
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

            foreach (var s in selected)
            {
                double time = (double)s.FuelMass / s.Consumption;
                double netForce = s.Thrust - currentMass * G;
                double acceleration = netForce / currentMass;

                if (acceleration < 0)
                {
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

        Console.WriteLine((int)Math.Round(maxVelocity));
    }
}