// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-03: Named save slots, progress summaries, autosave retention and game-owned deletion (spec 19).
// See CHANGES.md and License.txt for GPL-3.0 terms; inherited notices remain applicable.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

internal static class CompanySaveSlots
{
    internal const int BackupCount = 3;
    internal sealed class Entry
    {
        internal string Path, Directory, Slot, Name;
        internal CompanyCampaign State;
        internal bool Recovered;
        internal string Details => "Leader: " + State.Characters.FirstOrDefault(c => c.Id == State.LeaderId)?.Unit.Name +
            "\nDay: " + (State.ElapsedDays + 1) + "\nGold: " + State.Gold + "\nMercenaries: " + State.Members.Count +
            "\nContracts completed: " + State.ContractsCompleted + (State.InBattle ? "\nBattle turn: " + State.Battle.Tactical.currentTurn : "\nWorld map") +
            (State.Ironman ? "\nIronman" : "") + (Recovered ? "\nRecovered from backup" : "");
    }
    internal static string Auto(CompanyCampaign state, string directory)
    {
        if (state.Ended && state.Ironman) { RemoveCompany(state.Id); return null; }
        return Write(state, directory, state.InBattle ? "battle" : "map", state.InBattle ? "Battle autosave" : "Map autosave", state.InBattle ? 2 : 1);
    }
    internal static string Manual(CompanyCampaign state, string name)
    {
        if (state.Ironman) throw new InvalidOperationException("Ironman uses autosaves only.");
        name = (name ?? "").Trim();
        if (name.Length == 0 || name.Length > 64) throw new InvalidOperationException("Enter a save name (1-64 characters).");
        string slot;
        using (var sha = SHA256.Create()) slot = "manual-" + BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(name.ToUpperInvariant()))).Replace("-", "").Substring(0, 24);
        return Write(state, DirectoryFor(state.Id), slot, name, 3);
    }
    internal static string Write(CompanyCampaign state, string directory, string slot, string name, int kind)
    {
        if (File.Exists(Path.Combine(directory, "fallen"))) throw new InvalidOperationException("This Ironman company has ended.");
        state.SaveSlot = slot; state.SaveLabel = name; state.SaveKind = kind;
        string path = CompanyCampaignSave.WriteGeneration(state, directory);
        // Publish and verify first; failed housekeeping never sacrifices the fresh save.
        if (IsOwned(directory, state.Id))
        {
            int kept = 0;
            foreach (var file in CompanyCampaignSave.Generations(directory))
            {
                CompanyCampaign saved;
                try { saved = CompanyCampaignSave.Load(file); } catch { continue; }
                if (saved.Id != state.Id || saved.SaveSlot != slot) continue;
                if (kept++ <= BackupCount) continue;
                try { RemoveFile(file); } catch (IOException) { } catch (UnauthorizedAccessException) { }
            }
        }
        return path;
    }
    internal static string DirectoryFor(string id)
    {
        Guid value;
        if (!Guid.TryParseExact(id, "N", out value)) throw new InvalidOperationException("Invalid company identity.");
        return Path.Combine(CompanyCampaignSave.DefaultDirectory, id);
    }
    static bool IsOwned(string directory, string id)
    {
        if (!string.Equals(Path.GetFullPath(directory).TrimEnd(Path.DirectorySeparatorChar), Path.GetFullPath(DirectoryFor(id)), StringComparison.OrdinalIgnoreCase)) return false;
        var current = new DirectoryInfo(directory);
        while (current != null)
        {
            if (current.Exists && (current.Attributes & FileAttributes.ReparsePoint) != 0) return false;
            current = current.Parent;
        }
        return true;
    }
    internal static List<Entry> List()
    {
        var entries = new List<Entry>();
        if (!Directory.Exists(CompanyCampaignSave.DefaultDirectory)) return entries;
        foreach (var directory in Directory.GetDirectories(CompanyCampaignSave.DefaultDirectory))
        {
            Guid id;
            if (!Guid.TryParseExact(Path.GetFileName(directory), "N", out id) || !IsOwned(directory, Path.GetFileName(directory))) continue;
            if (File.Exists(Path.Combine(directory, "fallen"))) continue;
            var seen = new HashSet<string>(); var failedSlots = new HashSet<string>(); bool skipped = false;
            foreach (var path in CompanyCampaignSave.Generations(directory))
            {
                CompanyCampaign state;
                string filename = Path.GetFileNameWithoutExtension(path);
                int separator = filename.LastIndexOf('~');
                string fileSlot = separator < 0 ? "legacy" : filename.Substring(separator + 1);
                // A newer verified generation already represents this slot; its older backups stay private.
                if (seen.Contains(fileSlot)) continue;
                try { state = CompanyCampaignSave.Load(path); } catch { skipped = true; failedSlots.Add(fileSlot); continue; }
                if (Path.GetFileName(directory) != state.Id || state.Preview || (state.Ironman && state.Ended)) continue;
                string slot = string.IsNullOrEmpty(state.SaveSlot) ? "legacy" : state.SaveSlot;
                if (!seen.Add(slot)) continue;
                entries.Add(new Entry { Path = path, Directory = directory, Slot = slot, State = state,
                    Name = state.SaveLabel ?? "Autosave", Recovered = state.Ironman ? skipped : failedSlots.Contains(slot) });
                if (state.Ironman) break;
            }
        }
        return entries.OrderByDescending(e => Path.GetFileName(e.Path), StringComparer.Ordinal).ToList();
    }
    internal static void Delete(Entry entry)
    {
        if (!IsOwned(entry.Directory, entry.State.Id)) throw new InvalidOperationException("Save location is not owned by this company.");
        if (entry.State.Ironman) { RemoveCompany(entry.State.Id); return; }
        foreach (var path in CompanyCampaignSave.Generations(entry.Directory))
        {
            string filename = Path.GetFileNameWithoutExtension(path);
            int separator = filename.LastIndexOf('~');
            if (separator >= 0)
            {
                if (filename.Substring(separator + 1) == entry.Slot) RemoveFile(path);
                continue;
            }
            // Pre-slot generations belong to the one legacy autosave group in this owned folder.
            if (entry.Slot == "legacy") RemoveFile(path);
        }
    }
    internal static void RemoveCompany(string id)
    {
        string directory = DirectoryFor(id);
        if (!Directory.Exists(directory)) return;
        if (!IsOwned(directory, id)) throw new InvalidOperationException("Invalid company save location.");
        // Durable defeat marker prevents resurrection even if a subsequent deletion fails.
        File.WriteAllText(Path.Combine(directory, "fallen"), "Ironman company ended.");
        foreach (string path in CompanyCampaignSave.Generations(directory)) RemoveFile(path);
    }
    static void RemoveFile(string path)
    {
#if UNITY_EDITOR
        // Automated checks may inspect deletion choices, but must never destroy a file.
        if (CompanyCampaignSave.DiagnosticDirectory != null)
        {
            string root = Path.GetFullPath(CompanyCampaignSave.DefaultDirectory) + Path.DirectorySeparatorChar;
            if (!Path.GetFullPath(path).StartsWith(root, StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Diagnostic path escaped its root.");
            DiagnosticRemovals.Add(path);
            return;
        }
#endif
        File.Delete(path);
    }
#if UNITY_EDITOR
    internal static readonly List<string> DiagnosticRemovals = new List<string>();
#endif
}
