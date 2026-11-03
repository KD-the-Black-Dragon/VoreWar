// Modified for the VoreWar mercenary-company conversion.
// 2026-11-01: Explicit seeded instances for isolated company generation (spec 07).
// 2026-11-03: Capture and restore Unity Mono random-generator state without changing its algorithm (spec 19).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Linq;


public class Rand
{
    Random rand = new Random();
    public Rand() { }
    public Rand(int seed) { rand = new Random(seed); }

    // Unity 2018 Mono System.Random uses two indices and a 56-entry seed array.
    internal int[] Capture()
    {
        var fields = typeof(Random).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).OrderBy(f => f.Name).ToArray();
        var values = new System.Collections.Generic.List<int>();
        foreach (var field in fields)
        {
            if (field.FieldType == typeof(int)) values.Add((int)field.GetValue(rand));
            else if (field.FieldType == typeof(int[])) values.AddRange((int[])field.GetValue(rand));
            else throw new InvalidOperationException("Unsupported random-generator layout.");
        }
        return values.ToArray();
    }
    internal static Rand Resume(int[] values)
    {
        var result = new Rand(0); int offset = 0;
        foreach (var field in typeof(Random).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).OrderBy(f => f.Name))
        {
            if (field.FieldType == typeof(int)) field.SetValue(result.rand, values[offset++]);
            else if (field.FieldType == typeof(int[]))
            {
                int length = ((int[])field.GetValue(result.rand)).Length;
                var array = new int[length]; Array.Copy(values, offset, array, 0, length); offset += length; field.SetValue(result.rand, array);
            }
            else throw new InvalidOperationException("Unsupported random-generator layout.");
        }
        if (offset != values.Length) throw new InvalidOperationException("Invalid random-generator state.");
        return result;
    }

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

