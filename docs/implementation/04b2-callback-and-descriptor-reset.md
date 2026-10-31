# Packet 04B2 — callback and descriptor text reset

**Status:** Implemented, 2026-10-30. Unity compilation and explicit scene loading passed. The user confirmed the menu smoke check passed and authorized 03B; battle-log playtesting remains deferred until approved species exist. The user accepted 04B1 and authorized this packet. No species was enabled for testing.

## Scope and source paths

| Exact source | Completed change |
|---|---|
| `Assets/Scripts/Entities/Units/PredatorComponent.cs` | Collapse 22 presentation-only random wording switches across ForceConsume and ForceConsumeAuto; replace transfer-finalization narrative and direct release/spawn/counterattack wording with functional notices. Preserve the actual choice dialog and both placement callbacks. |
| `Assets/Scripts/Entities/Units/Actor_Unit.cs` | Neutralize introduction, counterattack, surrender, teleport, recovery, retreat and binding notices. Replace support-action wording arrays while retaining their existing animation, allegiance and paralysis effects. Keep the delayed introduction callback's original timing. |
| `Assets/Scripts/Entities/Spells/SpellList.cs` | Replace six prose arguments with functional freeze, release, Net and teleport notices. Preserve all spell effects and existing functional summon/capture messages. |
| `Assets/Scripts/AI/TacticalAI.cs` | Replace one retreat-flavor argument with an objective-based notice; retain AI conditions and actions. |
| `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs` | Replace self-consumption/failure prose and the failure-only string pool in ForceFeed; retain route selection, actual consumption and movement costs, including the final failure movement assignment. |
| `Assets/Scripts/Modes/Tactical/Log/LogUtilities.cs` | Replace GetPreyDesc/GetPredDesc with a neutral unit label and GetRaceDescSingl with a null-safe species identifier. Preserve the other helpers, pronouns, functional weapon names and 04B1's missing-target behavior. |
| `Assets/Scripts/Modes/Tactical/Log/PreyLocStrings.cs` | Retain all 23 public string-method signatures with deterministic neutral terms; remove synonym collections and their presentation random selection. Compatibility names for retired categories remain; no retired mechanic is re-enabled. |

The [call-site inventory](04b2-miscellaneous-call-sites.md) is a historical preflight plus final disposition. Its 301 candidate lines are now 102 call-site lines across the same eleven caller files. This is a lexical coverage measure, not a runtime test count. Existing functional notices in TacticalPathfinder, EquipmentFunctions, Potion, Unit, TacticalMode and Traits were reviewed and retained without editing those files in this packet. Useful damage, HP/mana, status, item, summon and retreat feedback remains available.

## Mechanical boundary and randomness

This packet changes presentation. Containment placement, sound calls, visibility/targetability, movement, experience, traits, health, alignment and other effects outside the identified wording spans remain. Existing revival, conversion, transformation and surrender mechanics are not silently retired here; their approved changes remain in specs 05/10. Labels describing those current effects are temporary functional text, not a reversal of the design decisions.

The forced-consumption methods keep left/right placement, dialog labels and callbacks, fullness updates, route selection and their mechanical random choices. The local breastSide/bS variables used only by removed text are gone. Transfer finalization keeps its real pending stat-boost change, prey removal, corruption, alignment and subsequent state updates. Source correspondence checks compare against this packet's snapshots, not HEAD, which predates earlier accepted packets.

As agreed for 04B1, removing presentation random draws changes consumption of the shared random stream. Identical future outcomes from an identical seed are not promised. No new RNG architecture or combat formula was introduced. These helper placeholders consume no randomness; future narrative authoring remains the user's work.

New miscellaneous events still use the existing Message payload route, now supplied by reviewed functional callbacks. Unsupported legacy saves may contain already-stored narrative payloads; this packet does not migrate or sanitize them. Help, species descriptions, mechanical tooltips, sounds and art are separately scoped. This is coverage of the inventoried tactical narrative routes, not a claim that every string in the game has been replaced.

## Verification and limits

- Fresh Unity 2018.3.12f1 runtime/editor compilation passed with no C# errors or import exceptions, return code 0: `Logs/packet-04b2-compile.log` (ignored).
- Subsequent explicit opening of `Assets/base.unity` with the compiled assemblies passed with no scene parse errors/exceptions, return code 0: `Logs/packet-04b2-scene.log` (ignored). This is a scene-load check, not a Play-mode test or player build.
- Source correspondence passed for all surrounding spell/AI/actor/ForceFeed code after removing the identified text spans. PredatorComponent's entire remaining source matched after stripping its 22 presentation switches, transfer wording segment, miscellaneous log statements, comments/whitespace and presentation-only locals. Both dialog callbacks and their placement/fullness updates are preserved. During editing, this check caught a lost dialog suffix; it was restored from the packet snapshot before compilation.
- Both helper APIs are retained; all other LogUtilities code matches the packet start. The six reviewed unchanged caller files match their snapshots. Evidence is in ignored `Logs/packet-04b2-correspondence.json` and the presentation-block audit.
- Scene bytes match the packet-start snapshot. License.txt's Git content hash matches the tracked license. The two known Unity-removed orphan metadata files were restored byte-for-byte. No files were deleted/moved, no new dependencies/assets were introduced, and no tests/framework were added. Packet source/diff checks pass; unrelated prior work remains.

The empty approved roster prevents real battle and text-display playtests. Compilation, scene loading and source correspondence do not replace those checks. Carry real callback formatting, action resolution and log-filter checks into the first approved-species battle review. No gameplay/save round trip or same-seed equivalence was tested.

## GPL change check

Read License.txt sections 4 and 5. All seven modified C# paths carry dated conversion notices and are listed in CHANGES.md. Existing notices and upstream credits are retained, including Tatltuae's additional callback-text attribution in the shortened PredatorComponent/TacticalUtilities code. No copied external code, new asset or dependency was added. License.txt and existing version grants remain unchanged. This source-change check is not a complete third-party redistribution or binary-release compliance audit.

## User review and next packet

Open `Assets/base.unity` in the recorded Unity version and enter Play mode. Confirm the main menu opens, Content Settings still shows its retained controls, and Pure Tactical still explains the empty approved roster without starting an invalid battle. No sample species is needed.

After feedback, proceed to 03B's rejected-species dependency/ownership preflight. Use the 90-name removal map, keep serialized numeric identities stable, and review eager registries and shared bundles before changing their use. No whole-file deletion or move is authorized. Carry legacy providers into 05 before claiming full generation closure; curation and the new campaign remain later phases.
