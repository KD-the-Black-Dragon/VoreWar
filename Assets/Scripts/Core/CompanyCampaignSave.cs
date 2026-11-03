// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-01: Versioned company checkpoints, validation and corruption recovery (spec 06).
// 2026-11-01: Rebind nonserialized company item services without re-equipping or changing stats (specs 07–08).
// 2026-11-01: Verify health and fate values after checkpoint round trips instead of checking only company/day (09–10 feedback).
// 2026-11-01: Reject lossy injury, treatment and immunity serialization alongside the existing HP preservation guard (spec 11).
// 2026-11-01: Verify queued wage-warning preservation across interrupted-day checkpoint serialization (spec 11 feedback).
// 2026-11-02: Centralize validated normal/Ironman resumption and verify mana, progression, gear/cooldowns and raw stats on round trips (spec 12).
// 2026-11-02: Verify event outcome, participants, source, cooldown and committed travel survive save round trips (spec 13).
// 2026-11-02: Resume all validated company map sizes through the draggable viewport (spec 14 feedback).
// 2026-11-03: Persist named/autosave generations and complete battlefield snapshots (spec 19).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using OdinSerializer;

internal static class CompanyCampaignSave
{
    const string Magic = "VoreWar.Company.1";
    const int MaximumBytes = 32 * 1024 * 1024;
#if UNITY_EDITOR
    // Focused editor checks redirect all save access to preserved synthetic data under Logs.
    internal static string DiagnosticDirectory;
#endif
    public static string DefaultDirectory
    {
        get
        {
#if UNITY_EDITOR
            if (DiagnosticDirectory != null)
            {
                string root = Path.GetFullPath(Path.Combine(UnityEngine.Application.dataPath, "../Logs")) + Path.DirectorySeparatorChar;
                string directory = Path.GetFullPath(DiagnosticDirectory);
                if (!directory.StartsWith(root, StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Save diagnostics must stay inside project Logs.");
                return directory;
            }
#endif
            return Path.Combine(UnityEngine.Application.persistentDataPath, "CompanyCampaigns");
        }
    }
    internal static byte[] Encode(CompanyCampaign state) { return SerializationUtility.SerializeValue(state, DataFormat.Binary); }
    internal static void Restore(CompanyCampaign target, byte[] snapshot)
    {
        var restored = Decode(snapshot);
        foreach (var field in typeof(CompanyCampaign).GetFields(BindingFlags.Instance | BindingFlags.Public)) field.SetValue(target, field.GetValue(restored));
    }
    static CompanyCampaign Decode(byte[] payload)
    {
        var state = SerializationUtility.DeserializeValue<CompanyCampaign>(payload, DataFormat.Binary);
        if (state == null) throw new InvalidDataException("Empty company checkpoint.");
        CompanyEconomy.Bind(state);
        CompanyBattleSnapshot.Bind(state);
        state.Validate();
        CompanyBattleSnapshot.Validate(state);
        return state;
    }
    internal static void RequireHealthPreserved(CompanyCampaign source, CompanyCampaign restored)
    {
        if (source.RestingDay != restored.RestingDay || source.RestDaysRemaining != restored.RestDaysRemaining ||
            source.UpdatingDay != restored.UpdatingDay || source.DailyStage != restored.DailyStage ||
            source.DeferredWageDecision != restored.DeferredWageDecision)
            throw new InvalidDataException("Interrupted recovery progress changed during serialization.");
        if (source.NextRoadEventDay != restored.NextRoadEventDay || source.RandomState != restored.RandomState ||
            source.Position != restored.Position || source.Actions != restored.Actions ||
            !CompanyRoadEvents.Same(source.RoadEvent, restored.RoadEvent) ||
            (source.Battle == null ? null : source.Battle.RoadEventId) != (restored.Battle == null ? null : restored.Battle.RoadEventId))
            throw new InvalidDataException("Road-event progress changed during serialization.");
        CompanyBattleSnapshot.RequirePreserved(source, restored);
        var characters = restored.Characters.ToDictionary(c => c.Id);
        if (characters.Count != source.Characters.Count) throw new InvalidDataException("Character records changed while saving.");
        foreach (var c in source.Characters)
        {
            CompanyCharacter other;
            if (!characters.TryGetValue(c.Id, out other) || c.Unit.Health != other.Unit.Health ||
                c.Unit.TemporaryHealth != other.Unit.TemporaryHealth || c.Unit.Downed != other.Unit.Downed || c.Unit.Digested != other.Unit.Digested || !CompanyInjuries.Same(c.Unit, other.Unit))
                throw new InvalidDataException("Character health or injuries changed during serialization. The previous checkpoint was preserved.");
            var unit = c.Unit; var copy = other.Unit;
            if (unit.Mana != copy.Mana || unit.Level != copy.Level || unit.Experience != copy.Experience ||
                !unit.ItemUses.SequenceEqual(copy.ItemUses) || !unit.ItemCooldowns.SequenceEqual(copy.ItemCooldowns) ||
                !unit.Items.Select(i => i == null ? null : i.CompanyInstanceId).SequenceEqual(copy.Items.Select(i => i == null ? null : i.CompanyInstanceId)) ||
                Enumerable.Range(0, (int)Stat.None).Any(i => unit.GetStatBase((Stat)i) != copy.GetStatBase((Stat)i)))
                throw new InvalidDataException("Character resources, progression or equipment changed during serialization. The previous checkpoint was preserved.");
        }
    }
    public static string Save(CompanyCampaign state, string directory) { return CompanySaveSlots.Auto(state, directory); }
    internal static string WriteGeneration(CompanyCampaign state, string directory)
    {
        state.Validate();
        CompanyBattleSnapshot.Validate(state);
        if (!System.Text.RegularExpressions.Regex.IsMatch(state.SaveSlot ?? "legacy", "^[a-zA-Z0-9-]+$")) throw new InvalidDataException("Invalid save slot identity.");
        Directory.CreateDirectory(directory);
        byte[] bytes = Encode(state), hash;
        if (bytes.Length > MaximumBytes) throw new InvalidDataException("Company checkpoint is too large.");
        RequireHealthPreserved(state, Decode(bytes)); // Reject a lossy payload before publishing any checkpoint file.
        using (var sha = SHA256.Create()) hash = sha.ComputeHash(bytes);
        string path = Path.Combine(directory, DateTime.UtcNow.Ticks.ToString("D19") + "-" + Guid.NewGuid().ToString("N") + "~" + state.SaveSlot + ".company");
        // Immutable generations: interruption leaves an invalid newest file; older generations survive.
        // This writer never replaces/removes paths; verified slot retention is handled separately.
        using (var stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None))
        {
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(Magic); writer.Write(bytes.Length); writer.Write(hash); writer.Write(bytes);
                writer.Flush(); stream.Flush(true);
            }
        }
        var verified = Load(path);
        RequireHealthPreserved(state, verified);
        if (verified.Id != state.Id || verified.ElapsedDays != state.ElapsedDays) throw new IOException("Checkpoint verification failed.");
        return path;
    }
    public static CompanyCampaign Load(string path)
    {
        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (var reader = new BinaryReader(stream))
        {
            if (stream.Length < 40 || stream.Length > MaximumBytes + 128L) throw new InvalidDataException("Invalid company checkpoint size.");
            if (reader.ReadString() != Magic) throw new InvalidDataException("This is not a supported company checkpoint.");
            int length = reader.ReadInt32();
            if (length < 1 || length > MaximumBytes || stream.Length - stream.Position != length + 32L) throw new InvalidDataException("Incomplete company checkpoint.");
            byte[] expected = reader.ReadBytes(32), bytes = reader.ReadBytes(length), actual;
            using (var sha = SHA256.Create()) actual = sha.ComputeHash(bytes);
            if (!actual.SequenceEqual(expected)) throw new InvalidDataException("Company checkpoint checksum failed.");
            return Decode(bytes);
        }
    }
    internal static CompanyCampaign LoadForResume(string path)
    {
        // Validate fully before a view installs the prospective state.
        var state = Load(path);
        string directory = Path.GetDirectoryName(Path.GetFullPath(path));
        if (File.Exists(Path.Combine(directory, "fallen")) || Path.GetFileName(directory) != state.Id)
            throw new InvalidDataException("Unsupported company checkpoint or mismatched company folder.");
        if (state.Ironman)
        {
            int skipped; string latest;
            LoadLatest(directory, out skipped, out latest);
            if (!string.Equals(Path.GetFullPath(path), Path.GetFullPath(latest), StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Ironman can only resume its latest valid checkpoint.");
        }
        return state;
    }
    public static string[] Generations(string directory)
    {
        return Directory.Exists(directory) ? Directory.GetFiles(directory, "*.company").OrderByDescending(Path.GetFileName, StringComparer.Ordinal).ToArray() : new string[0];
    }
    public static CompanyCampaign LoadLatest(string directory, out int skipped)
    {
        string selected;
        return LoadLatest(directory, out skipped, out selected);
    }
    public static CompanyCampaign LoadLatest(string directory, out int skipped, out string selected)
    {
        if (File.Exists(Path.Combine(directory, "fallen"))) throw new InvalidDataException("This Ironman company has ended.");
        skipped = 0; selected = null;
        foreach (string path in Generations(directory))
        {
            try { var state = Load(path); selected = path; return state; }
            catch (Exception ex) when (!(ex is OutOfMemoryException))
            { skipped++; }
        }
        throw new InvalidDataException("No valid company checkpoint was found. Existing files were preserved.");
    }
}
