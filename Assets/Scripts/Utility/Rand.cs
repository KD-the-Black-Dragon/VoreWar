// Modified for the VoreWar mercenary-company conversion.
// 2026-11-01: Explicit seeded instances for isolated company generation (spec 07).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;


public class Rand
{
    Random rand = new Random();
    public Rand() { }
    public Rand(int seed) { rand = new Random(seed); }

    internal int Next()
    {
        return rand.Next();
    }


    internal int Next(int maxValue)
    {
        if (maxValue < 1)
            maxValue = 1;
        return rand.Next(maxValue);
    }

    internal int Next(int minValue, int maxValue)
    {
        if (maxValue < minValue)
            maxValue = minValue;
        if (maxValue < 1)
            maxValue = 1;
        return rand.Next(minValue, maxValue);
    }

    internal double NextDouble()
    {
        return rand.NextDouble();
    }
}

