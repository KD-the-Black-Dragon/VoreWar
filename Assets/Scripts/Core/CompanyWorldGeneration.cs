// Added by this fork for the VoreWar mercenary-company conversion.
// 2026-11-05: Target five-day town journeys with a short six-town road circuit and two real exits per town (spec 37).
// 2026-11-05: Compact six-zone placement, bottom-center start, cleared roads and village farms (spec 36).
// 2026-11-05: Generate coherent 160x120 regions, six spaced towns and continuous roads after terrain (spec 35).
// See CHANGES.md and License.txt for GPL-3.0 terms; existing artwork attribution remains applicable.
using System;
using System.Collections.Generic;
using System.Linq;

internal static class CompanyWorldGeneration
{
    internal const int Width = 120, Height = 80, TownCount = 6, MaximumDimension = 256;

    internal static void Generate(CompanyCampaign state)
    {
        state.Width = Width; state.Height = Height;
        state.Terrain = new int[Width * Height]; state.Discovered = new bool[state.Terrain.Length];
        var elevation = new Noise.OpenSimplexNoise(state.Seed);
        var moisture = new Noise.OpenSimplexNoise((long)state.Seed + 6413);
        var climate = new Noise.OpenSimplexNoise((long)state.Seed - 1741);
        for (int y = 0; y < Height; y++) for (int x = 0; x < Width; x++)
        {
            double high = elevation.Evaluate(x * .035, y * .035);
            double wet = moisture.Evaluate(x * .055, y * .055);
            double warm = .65 * y / (Height - 1) + .35 * climate.Evaluate(x * .025, y * .025);
            state.Terrain[y * Width + x] = high > .25 ? 2 : warm > .45 && wet < -.08 ? 3 : wet > .10 ? 1 : 0;
        }
        // Map rows increase downward. Zone4 is the bottom-center starting zone.
        int[] zones = { 4, 0, 1, 2, 3, 5 };
        int[] columns = { Width / 2 - 30 + state.Next(5) - 2, Width / 2 + state.Next(5) - 2, Width / 2 + 30 + state.Next(5) - 2 };
        int[] rows = { Height / 2 - 15 + state.Next(5) - 2, Height / 2 + 15 + state.Next(5) - 2 };
        for (int i = 0; i < TownCount; i++)
        {
            int column = zones[i] % 3, row = zones[i] / 3;
            int x = columns[column];
            int y = rows[row];
            int tile = y * Width + x;
            state.Towns.Add(new CompanyTown { Id = Guid.NewGuid().ToString("N"), Name = "Town " + (i + 1), Tile = tile });
            state.Terrain[tile] = 0;
        }
        GenerateRoads(state);
        ApplyLandUse(state); // Clear the chosen routes only after road placement, then cultivate town neighbors.
        var lengths = RoadLegLengths(state);
        if (lengths.Count != TownCount || lengths.Any(length => length < 25 || length > 35))
            throw new InvalidOperationException("Town road journeys must stay within 25–35 moves.");
        state.Position = state.Towns[0].Tile;
    }

    internal static void ApplyLandUse(CompanyCampaign state)
    {
        if (state.Roads == null) return; // Pre-road campaigns retain their original geography.
        if (state.Farms == null) state.Farms = new bool[state.Terrain.Length];
        else Array.Clear(state.Farms, 0, state.Farms.Length);
        for (int tile = 0; tile < state.Roads.Length; tile++) if (state.Roads[tile]) state.Terrain[tile] = 0;
        var towns = new HashSet<int>(state.Towns.Select(t => t.Tile));
        foreach (int town in towns) for (int dy = -1; dy <= 1; dy++) for (int dx = -1; dx <= 1; dx++)
        {
            int x = town % state.Width + dx, y = town / state.Width + dy;
            if (x < 0 || y < 0 || x >= state.Width || y >= state.Height) continue;
            int tile = y * state.Width + x;
            if (towns.Contains(tile) || state.Roads[tile]) continue;
            state.Farms[tile] = true; state.Terrain[tile] = 0;
        }
    }

    static void GenerateRoads(CompanyCampaign state)
    {
        state.Roads = new bool[state.Terrain.Length];
        // Follow the perimeter of the 3x2 layout: every town gets two distinct exits.
        int[] circuit = { 0, 4, 1, 2, 3, 5 };
        for (int i = 0; i < circuit.Length; i++)
            Connect(state, state.Towns[circuit[i]].Tile, state.Towns[circuit[(i + 1) % circuit.Length]].Tile);
    }

    static void Connect(CompanyCampaign state, int start, int goal)
    {
        int x = start % state.Width, y = start / state.Width;
        int endX = goal % state.Width, endY = goal / state.Width;
        bool horizontal = y == endY;
        if (!horizontal && x != endX) throw new InvalidOperationException("Road endpoints must share a row or column.");
        int length = Math.Abs(endX - x) + Math.Abs(endY - y);
        // Favor short journeys over terrain detours. A gentle outward bend adds exactly two moves.
        bool bend = length < 28 || (length <= 32 && state.Next(2) == 0);
        if (bend)
        {
            int direction = Math.Sign(horizontal ? endX - x : endY - y);
            int one = (horizontal ? x : y) + direction * (length / 3);
            int two = (horizontal ? x : y) + direction * (length * 2 / 3);
            int offset = horizontal ? (y < state.Height / 2 ? -1 : 1) : (x < state.Width / 2 ? -1 : 1);
            if (horizontal)
            {
                Line(state, x, y, one, y); Line(state, one, y, one, y + offset);
                Line(state, one, y + offset, two, y + offset); Line(state, two, y + offset, two, y);
                Line(state, two, y, endX, endY);
            }
            else
            {
                Line(state, x, y, x, one); Line(state, x, one, x + offset, one);
                Line(state, x + offset, one, x + offset, two); Line(state, x + offset, two, x, two);
                Line(state, x, two, endX, endY);
            }
        }
        else Line(state, x, y, endX, endY);
    }

    static void Line(CompanyCampaign state, int x, int y, int endX, int endY)
    {
        int dx = Math.Sign(endX - x), dy = Math.Sign(endY - y);
        while (true)
        {
            state.Roads[y * state.Width + x] = true;
            if (x == endX && y == endY) return;
            x += dx; y += dy;
        }
    }

    internal static List<int> RoadLegLengths(CompanyCampaign state)
    {
        ValidateNetwork(state);
        var towns = new HashSet<int>(state.Towns.Select(t => t.Tile));
        for (int tile = 0; tile < state.Roads.Length; tile++)
            if (state.Roads[tile] && Neighbors(state, tile).Count(n => state.Roads[n]) != 2)
                throw new InvalidOperationException("A ring road must have exactly two neighbors at every tile, including towns.");
        var lengths = new List<int>();
        foreach (int town in towns)
        {
            var destinations = new HashSet<int>();
            foreach (int exit in Neighbors(state, town).Where(n => state.Roads[n]))
            {
                int previous = town, current = exit, distance = 1;
                while (!towns.Contains(current))
                {
                    int next = Neighbors(state, current).Single(n => state.Roads[n] && n != previous);
                    previous = current; current = next;
                    if (++distance > state.Roads.Length) throw new InvalidOperationException("A road failed to reach another town.");
                }
                if (current == town || !destinations.Add(current)) throw new InvalidOperationException("Two exits must reach two different towns.");
                if (town < current) lengths.Add(distance); // Count each undirected connection once.
            }
            if (destinations.Count != 2) throw new InvalidOperationException("Each town must connect to exactly two other towns.");
        }
        return lengths;
    }

    internal static IEnumerable<int> Neighbors(CompanyCampaign state, int tile)
    {
        int x = tile % state.Width, y = tile / state.Width;
        if (x > 0) yield return tile - 1;
        if (x + 1 < state.Width) yield return tile + 1;
        if (y > 0) yield return tile - state.Width;
        if (y + 1 < state.Height) yield return tile + state.Width;
    }

    internal static int RoadMask(CompanyCampaign state, int tile)
    {
        if (state.Roads == null || !state.Roads[tile]) return 0;
        int mask = 0;
        foreach (int next in Neighbors(state, tile)) if (state.Roads[next])
            mask |= next == tile - 1 ? 1 : next == tile + 1 ? 2 : next == tile - state.Width ? 4 : 8;
        return mask;
    }

    internal static void ValidateNetwork(CompanyCampaign state)
    {
        if (state.Roads == null || state.Roads.Length != state.Terrain.Length) throw new InvalidOperationException("Missing road network.");
        var seen = new HashSet<int>(); var queue = new Queue<int>();
        queue.Enqueue(state.Towns[0].Tile); seen.Add(state.Towns[0].Tile);
        while (queue.Count > 0)
            foreach (int next in Neighbors(state, queue.Dequeue())) if (state.Roads[next] && seen.Add(next)) queue.Enqueue(next);
        var towns = new HashSet<int>(state.Towns.Select(t => t.Tile));
        if (towns.Any(t => !state.Roads[t] || !seen.Contains(t))) throw new InvalidOperationException("A town is disconnected from roads.");
        for (int tile = 0; tile < state.Roads.Length; tile++) if (state.Roads[tile] &&
            (!seen.Contains(tile) || (!towns.Contains(tile) && Neighbors(state, tile).Count(n => state.Roads[n]) < 2)))
            throw new InvalidOperationException("Roads contain an isolated segment or a non-town dead end.");
    }
}
