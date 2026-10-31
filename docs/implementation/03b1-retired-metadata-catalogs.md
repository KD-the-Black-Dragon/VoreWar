# Packet 03B1 — retired metadata catalogs and named-unique factory

**Status:** Implemented, 2026-10-30; final compilation and explicit scene load passed. The user confirmed checks 1 and 3, then confirmed the clarified mapping-preservation check and authorized 03B2. The user accepted the 03B dependency preflight and authorized this packet. No species was approved for gameplay.

## Source changes

| Exact source | Change |
|---|---|
| `Assets/Scripts/Core/SpeciesPolicy.cs` | Add IsRetired as a read-only query of the existing 90-entry set. IsCurated uses that query without changing eligibility. Keep curated/retired entries, saved IDs, roles and player preferences unchanged. |
| `Assets/Scripts/Subwindows/RaceEditorPanel.cs` | Build all five species/mapping catalogs from defined nonnegative, non-retired entries; use the same list for all ten bulk trait/tag traversals. Guard programmatic catalog/loading callbacks, prevent invalid/retired selection loads/saves/resets, and preserve raw mapping values through a non-enum keep option. |
| `Assets/Scripts/UI/HoveringRacePicture.cs` | Reject permanently retired species before preview construction even in an authorized developer session. Existing uncurated developer inspection and normal approved/player-enabled preview checks remain. |
| `Assets/Scripts/UI/Connectors/UnitTagMenu.cs` | Filter the two nested IsRace condition catalogs too. Preserve unavailable stored condition IDs through a neutral keep option, including copying the target into a duplicated UI row; valid choices are saved by enum identity rather than list index. Other condition types and modifier enums are unchanged. |
| `Assets/Scripts/Entities/MercenaryHouse.cs` | Retire the uncalled CreateUniqueMercenary method body/signature. Keep the file, normal recruitment and stock code, existing UniqueMercs data and existing individuals. |

The nested Unit Tags source was added after tracing its separate species dropdowns. Filtering only the parent editor would have left rejected entries selectable there. No scene/prefab changes, asset repacking, name sourcing, parameter/graphics registry retirement or new mechanics belong to this packet.

## Editor behavior and mapping preservation

The metadata catalog now contains **64** remaining nonnegative enum entries, excluding all 90 permanently rejected entries. These 64 are developer inspection candidates, not a newly approved playable roster. The global Species Availability page and both gameplay starts still use the empty curated policy. Password/session behavior and all normal editor tabs remain.

The four mapping dropdowns have a neutral **Keep existing mapping** option. A stored default (`Race.none`), retired value or unknown ID selects that option; it does not show a rejected name or pretend to be another species. Save returns that exact raw field unless the developer explicitly selects a valid remaining species. Valid stored mappings select their actual option. Default mappings are not expanded into effective species values merely by opening/saving this screen. Underlying RaceSettings fallback/sanitization behavior is unchanged and belongs to later data work.

ShowPanel and LoadRace suppress reentrant RaceChanged saves while controls are being populated. The previous loading state is restored in finally, including nested calls and early returns. RaceChanged/SaveRace also require an authorized editor session. This protects the catalog rebuild from writing partially loaded controls; it does not introduce a new save format or preference reset.

The nested tag condition catalog shows **Keep existing target** for a stored retired, sentinel or unknown target ID. A temporary UI-row dictionary preserves that integer for save or target copying; a valid explicit replacement wins. The dictionary is cleared on opening/closing and entries are removed with deleted UI rows. The serialized UnitTagModifier schema is unchanged. Filtering also replaces the old IsRace option-count selection with an exact option-name lookup, avoiding a shifted selection after the list shrinks. Other duplication behavior, tag effects and condition types are outside this change.

Bulk trait/tag operations no longer create or mutate settings for retired entries or the none sentinel. Existing unrelated settings/characters are not removed. Reset-all and normal slot persistence behavior are retained, rather than rewriting old personal files as a retirement shortcut.

## Validation

The final fresh Unity 2018.3.12f1 compilation includes all five sources: `Logs/packet-03b1-compile-final.log` (ignored). It exited 0 without C# errors/import exceptions. The earlier unsuffixed compile/scene logs cover the initial four-source group and must not be substituted for final evidence.

Final explicit scene opening with the freshly compiled assemblies passed without parse errors/exceptions, exit 0: `Logs/packet-03b1-scene-final.log` (ignored). No player build or Play-mode test was performed by the agent. Source checks are not proof of interactive editor persistence or battle behavior.

Source review verified:

- The curated/retired source sets and normal recruitment/stock code match the packet-start snapshots. CreateUniqueMercenary had no source callers before retirement and has no remaining C# declaration/call.
- All ten parent bulk loops changed only their source enumeration. Eighteen unaffected RaceEditorPanel methods, including password-session lifecycle, tab activation and close/save paths, match their snapshots.
- The nested tag's three modifier enum definitions and its non-species load/save switch branches are unchanged. Raw species mapping/condition values have explicit preserve paths; no missing option falls back to enum zero/Cats.
- Parent catalogs and nested lists use the permanent-retirement predicate, not player enablement or the empty approval registry. Hover rejection precedes Unit construction.

No automated tests/framework were added. Scene bytes match the packet-start snapshot; License.txt's Git content hash matches the tracked license. Both known Unity-removed orphan metadata files were restored byte-for-byte. Comparing against the preflight game-file hashes confirms that only the five intended C# paths changed; scene/prefabs/manifests/bundles remain unchanged. Packet-only diff and dated-notice review passed. Battle tests remain blocked by the deliberately empty approved roster. Remaining metadata descriptions, species-specific traits/spells and old providers still belong to 03B3/05; this packet does not claim every rejected identifier or resource is gone.

## GPL/source check

Reviewed License.txt sections 4 and 5. All five changed C# files carry dated conversion notices, retain meaningful prior notices and are listed by exact path in CHANGES.md. No new third-party code, asset or dependency was introduced. Existing GPL terms and upstream attribution remain unchanged. This change check is not a complete inherited-asset or release-distribution compliance audit. No files were deleted or moved.

## User review

1. Open the password-gated Race Editor. Its species list and four species-mapping lists should omit rejected entries such as Cats/Humans. The normal General, Trait and Unit Tags tabs should remain usable. Remaining species are inspection-only; do not enable a sample for gameplay.
2. If a field already shows Keep existing mapping, leave that field untouched, save/close and reopen. It should continue showing the keep label while its unavailable/default raw value is preserved. Do not first select a replacement species: that explicitly overwrites the saved mapping. Selecting Keep existing mapping afterward means keep the current saved value, not reset or restore the original value. Example: after saving Avians, choosing Keep existing mapping leaves Avians saved, and reopening shows Avians. For a tag condition already showing Keep existing target, the equivalent check leaves its target untouched. Real unavailable stored-ID round trips still need interactive confirmation; no personal files were inspected by the agent.
3. Content Settings should retain its controls and empty Species Availability page; Pure Tactical should still explain that no approved species is available. Check for new red Console errors while navigating.

After feedback, the next packet is 03B2: retire rejected name seed/import routes and exclusive built-in name pools, preserving generic names and personal files. Registry/shared graphics and old-world provider retirement remain later packets.

## Follow-up on the manual-check wording

The user confirmed checks 1 and 3 passed. They could select and save a replacement mapping, but selecting Keep existing mapping afterward returned to the saved species on reopening. Source inspection confirms this is expected: the option is a no-op on the current stored value, not a persistent enum value, reset or undo. The earlier instruction to switch away and back was misleading and is corrected above. This follow-up changes documentation only; no UI/code behavior, source notices or compilation result is newly claimed. The already-unavailable raw-value preservation path remains source-reviewed rather than directly confirmed by that report.

The user subsequently confirmed the clarified check works and authorized the next step. 03B1 is accepted; no reset/undo mechanic was added.
