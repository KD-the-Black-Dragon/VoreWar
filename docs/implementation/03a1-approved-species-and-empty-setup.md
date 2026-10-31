# Packet 03A1 — approved-species policy and safe empty setup

**Status:** Implemented, 2026-10-30; fresh compilation and scene loading passed; the user confirmed the requested manual checks succeeded and authorized 03A2. The user confirmed the repaired 02C settings tests succeeded and authorized the next step. Species curation has not started.

## Scope and visible result

This is the first of two reviewable eligibility packets under 03A. It establishes the code-owned approval boundary and the player-facing empty-roster state. No species is selected for the user or temporarily enabled to keep battles available.

Content Settings retains its five existing pages and adds **Species Availability** as the sixth page at runtime. With the intentionally empty approved list, it explains that species will be added individually during curation. Once species are approved in code, it displays only those entries, marked sapient/animal, with enable/disable toggles. It never presents the old enum-wide mercenary/monster balance controls. Existing characters are not removed when new generation is disabled.

Pure Tactical setup remains accessible. Its selectors contain a disabled “No approved species” entry, with no placeholder creature preview. Single and bulk launch requests stop with an explanation before creating a world. The still-present old campaign setup/creation entry also stops before entering its enum-wide setup while the sapient pool is empty. Old campaign-loop retirement belongs to 05, not this packet.

The new page is built using the retained panel dimensions, font and cosmetic toggle styling; it includes a clipped scrollable list for future approved entries. `Assets/base.unity` and shared prefabs are unchanged in this packet. No files are deleted or moved.

## Approval, roles and preferences

`Assets/Scripts/Core/SpeciesPolicy.cs` defines:

- An intentionally empty `CuratedSpecies` dictionary. Approval and sapient/animal classification can only be changed by a source edit. No role is inferred from old empire/monster numeric ranges.
- All 90 entries in the user's rejected-species list as explicit retired enum values, verified against the existing source/name map. All current named uniques are covered by that set. They cannot be enabled through player preferences or a preset. This is eligibility retirement, not physical source/asset removal.
- Uses for leader, mercenary, enemy, skirmish and generic creature summoning. Leaders/mercenaries require sapients; generic creature summons require animals; enemy/skirmish pools can use both kinds. Other special summon/copy routes require the follow-up audit.
- Global per-species player choices using new `CuratedSpeciesEnabled <numeric ID>` keys. An enabled preference cannot approve an absent/retired entry. All-disabled and zero-approved pools are valid states; random selection returns an unavailable result instead of a fallback species.

ContentSettingsData loads/saves these choices alongside retained preferences. Ordinary Exit applies choices for the current session; Save persists them. Its legacy Merc compatibility keys and WorldConfig.ResetDictionary now mirror approved enablement rather than setting every species true. Old Merc/spawner settings and Race Editor slots cannot add entries to the code-owned approved dictionary. Numeric enum identities and saved individuals remain untouched.

## Source boundaries changed

| Source | Change |
|---|---|
| `Assets/Scripts/Core/SpeciesPolicy.cs` and `.meta` | New policy, role filters, global choices and empty-pool selection result. |
| `Assets/Scripts/Core/ContentSettingsData.cs` | Load/save global species choices and replace temporary true Merc defaults with policy results. |
| `Assets/Scripts/Core/WorldConfig.cs` | Reset legacy availability keys from policy rather than enabling every species. |
| `Assets/Scripts/Subwindows/ContentSettings.cs` | Runtime sixth page, approved-only toggle bindings, explicit empty message, apply/save integration. |
| `Assets/Scripts/Subwindows/CreateTacticalGame.cs` | Rebuild selectors from approved pools, remove Cats preview and numeric random fallback, validate single/bulk launch before world mutation, filter batch participants by the explicit taxonomy. The old broad-species batch checkbox is labeled Include animals. Also resolve the defender's own dropdown for repeated-match selection. |
| `Assets/Scripts/Modes/StartMode.cs` | Guard empty sapient pool before showing old strategic setup. |
| `Assets/Scripts/Subwindows/CreateStrategicGame.cs` | Guard the direct CreateWorld callback before configuration/world mutation. |
| `Assets/Scripts/Entities/MercenaryHouse.cs` | Approved sapient candidates only, no enum-wide cached pool, no new named uniques. Empty pools preserve already offered candidates and existing unique records. |
| `Assets/Scripts/Entities/Spells/SpellList.cs` | Generic creature summon uses approved enabled animals, with no Vagrants fallback. Check pool availability before spell resources are spent; select the animal only after a successful cast. |

## Explicit follow-up boundary: 03A2

This packet is not a claim that every legacy generation/mutation site has been retired or filtered. Before approving any species or declaring 03A complete, finish the existing creation/write inventory, including:

- Special summons, clones, AstralCall/ant pheromones, spawn mappings/reproduction, transformation/disguise/direct Race writes, and loaded appearance/preset destinations. Temporary sapient copies remain distinct from generic animal summons.
- Existing-world village/army/event/monster factories and old creation assumptions. Old saves are not migrated into the new campaign; no saved individual is deleted here. The old simulation still needs retirement in its scheduled packets.
- Metadata/art helpers, hover previews and the Humans narrative dummies. Metadata resolution and saved-individual restoration must stay distinct from generation eligibility; do not add a blanket GetRace/constructor exception that breaks startup or existing identities. Narrative dummies are coordinated with 04B before rejected registry removal in 03B.
- Explicit enforcement for every retained runtime route before future approved content is made playable. The empty code registry and entry guards are not substitutes for that audit.

No rejected class, shared bundle, sprite, namelist or registry implementation is physically removed here. Source/resources remain for those dependency packets. The gated Race Editor still supports its normal tabs for developer inspection; editing species stats does not approve generation.

## Validation and review

Fresh Unity 2018.3.12f1 compilation of both runtime/editor assemblies passed with no C# errors and return code 0 (`Logs/packet-03a1-compile.log`, ignored). A subsequent explicit scene open using the new assemblies passed with no parse errors, broken-reference messages or exceptions and return code 0 (`Logs/packet-03a1-scene-final.log`, ignored). The earlier combined open/import attempt exited successfully but did not compile the new code, so it was not counted as the compile check. Verified the runtime assembly is newer than all changed source files. No automated tests/framework or player build added. The previous 02C repair taught that a successful source compile does not establish successful scene loading; both checks are required here. Verified the scene remains byte-for-byte unchanged relative to this packet's starting point. Restored the two known Unity-removed orphan metadata files from tracked bytes after editor checks. Static source review confirmed the exact 90 rejected IDs, dated notices and the intentionally empty approval set. No runtime Play-mode or nonempty-pool check was performed.

Manual checks after compilation:

1. Open Content Settings: the five retained pages still show controls, and Species Availability shows the intentional empty-list explanation. Save/reopen and confirm Show Unit Sides still persists.
2. Open Pure Tactical: both species selectors have no approved choices and no creature previews. Try a single battle and available bulk launch buttons; each should explain that no species are approved, without red Console errors or entering a battle.
3. Return to the main menu. The old campaign-start entry should explain why setup is unavailable. Options and password-gated Race Editor must remain accessible; changing a Race Editor slot must not populate the approved-species page.

Actual battles, nonempty availability toggles, animal-only summoning and company-level save/identity behavior remain untested until approved content/new campaign state exist. Do not mark spec 03 or its full eligibility work complete from empty-menu checks.

## GPL/source-change check

The eight modified C# files retain earlier notices and carry dated 03A1 notices. The new policy is marked as this fork's addition. Root CHANGES.md identifies all exact source paths and its associated non-commentable `.meta` path/date. License.txt and existing upstream attribution/legal text are retained; no third-party code/dependency/assets introduced. No deletion/move is used. This is the packet's source-change review, not a complete distribution or inherited-asset-license audit.

## Subsequent user confirmation

The user confirmed that 03A1 works properly and authorized the next step. Actual battles/nonempty species pools remain untested until curation. Work now proceeds to 03A2, with interruption notes in [the current checkpoint](resume-checkpoint.md).
