# Packet 03B2 — rejected name sources

**Status:** Implemented, 2026-10-30; fresh compilation, explicit scene loading and scope checks passed. The user confirmed main-menu startup and Update Namelists completed without errors, and authorized the next packet. The user accepted all 03B1 checks, including the clarified keep-mapping check, and authorized this packet. No species was approved for gameplay.

## Three changed source paths

| Exact source | Change |
|---|---|
| `Assets/Scripts/Core/State.cs` | Remove 103 rejected-owner seed basenames from the shared manifest, retaining 114 in original order. Both startup/reset seeding and the explicit name reset use that same active manifest. Replace ResetNamelists' delete-and-copy branch with a direct overwrite copy of active files. |
| `Assets/Scripts/Utility/Texts/NameGenerator.cs` | Exclude retired species from per-species personal-name imports and army-name sections. Retire 23 town pools, three creature pools, their rejected branches and the unused retired feralOrcasNames declaration. Keep generic/remaining pools, public APIs and nonretired naming rules. Return neutral labels for direct requests to name a retired species. |
| `Assets/Scripts/Subwindows/Options.cs` | Correct the two reset/reload descriptions to describe active-list handling and preservation/ignoring of inactive files. Button callbacks, preferences and unrelated options remain unchanged. |

The permanent-retirement query is used, not curated/player-enabled status. The 64 remaining metadata candidates remain unapproved; future player disabling must not erase existing individuals or their naming data. No IDs, role assignments, source registries, scenes, prefabs, bundles, shipped name files or personal files were changed. Files are retained under the checkout's no-delete rule.

## Seed and import coverage

The State manifest previously listed 217 basenames. Ownership is checked by the exact enum name after an existing male/female prefix, keeping sapient Sharks separate from retired FeralSharks, Frogs separate from FeralFrogs, and similar distinctions. The four generic basenames remain. The historical FeralSlimes basename is retained: it does not match the FeralSlime enum spelling, and that uncurated species was not rejected. Correcting that unrelated legacy alias is outside this packet.

All 103 removed manifest entries have exact permanently retired owners; all 114 retained basenames and their order match the prior manifest minus those entries. The 114 shipped source files are present. Startup and the user-data initialization path no longer seed the excluded files. Existing copies are neither deleted nor read by the revised per-species import loop, which traverses only defined, nonnegative, nonretired species.

The shared armyNames file remains a mixed source. Its parser now starts with no current species and ignores content under retired/invalid enum headers, while still recognizing the next valid header. This prevents a skipped section from leaking into the previous allowed species' name pool. The shipped file has 39 recognized species sections: 22 rejected sections are ignored, 17 retained. The original file stays intact, including any personal copy. A malformed non-enum header still follows the existing file format's behavior; no new general parser/format-validation framework was introduced.

## Built-in content and neutral fallbacks

Removed 23 rejected town-name list initializers and declarations, and 24 town branches including the special Vagrants fallback. Removed the Compy, Vagrants and ViraeUltimae creature lists and switch cases. All 28 remaining List<string> initializers match their prior text verbatim, including generic male/female/creature lists and the remaining town/creature pools. The 19 remaining town branches match their previous bodies; the first remaining branch simply starts the conditional chain.

GetName/GetMonsterName return **Unit** for a retired species, GetArmyName returns **Company**, and the town methods return **Town N**. These are functional placeholders for stale legacy requests, not new story content or species-generation approval. Existing stored names are not rewritten. Uncurated and player-disabled nonretired species retain their original naming behavior. Existing public method signatures remain.

Naming can use State.Rand. Skipping retired name selection avoids its random draws; exact same-seed behavior of unsupported legacy requests is not promised. No new RNG architecture or battle formula was introduced. Full legacy generation-provider retirement still belongs to 05.

## Name maintenance and personal data

The explicit Reset Namelists action still requires its existing confirmation and replaces active lists with shipped defaults. Its implementation now uses File.Copy with overwrite instead of deleting the destination first. It touches only the 114 active basenames. Retired and otherwise unlisted files stay untouched. This is an intentional, narrow change to the affected maintenance path; it is not a redesign of user-data management.

Update Namelists continues to reload lists into memory, now ignoring retired species data. The reset/reload descriptions explain those scopes. The separate whole-user-data reset is unchanged and was not invoked. Neither reset nor reload was invoked by the agent, and no personal name-file contents were inspected or changed during this packet. Shipped retired files remain inactive on disk; physical content/packaging retirement is not claimed here.

## Verification and limits

- Fresh Unity 2018.3.12f1 runtime/editor compilation passed with no C# errors/import exceptions, exit 0: `Logs/packet-03b2-compile.log` (ignored).
- Subsequent explicit base.unity opening with fresh assemblies passed without parse errors/exceptions, exit 0: `Logs/packet-03b2-scene.log` (ignored). No player build, agent Play-mode test or test framework added.
- Source correspondence confirms 28 retained name-list initializers, 19 nonretired town branches, generic literal lists, retained seed order and naming public signatures. State outside the seed manifest/reset method matches the packet snapshot, apart from the dated notice. Options outside the two descriptions matches its snapshot, apart from the notice.
- Scene bytes match the packet-start snapshot; License.txt's Git content hash matches tracked content. Both known Unity-removed orphan metadata files were restored byte-for-byte. Comparing against preflight game-file hashes finds only accepted 03B1 files plus these three intended sources; scenes/prefabs/manifests/bundles and shipped name files remain unchanged. Dated source notices and packet-only diff checks passed.
- Name reset has no File.Delete operation. Retired per-species imports are filtered; shared army sections have an explicit ignore path. Static evidence lives in ignored `Logs/packet-03b2-source-review.json`, `packet-03b2-retired-name-inventory.json` and `packet-03b2-army-sections.json`.

The empty approved roster prevents actual new-character/battle name checks. Compilation, scene opening and source comparisons do not prove every external personal file is well formed. No runtime reset, personal-data round trip, corruption recovery or same-seed outcome equivalence was tested. Carry actual recruitment/enemy naming coverage into approved-species curation and the first playable company loop.

## GPL/source change check

Reviewed License.txt sections 4 and 5. All three modified C# files carry dated conversion notices and exact-path entries in CHANGES.md; prior State notices and existing attribution are preserved. No new dependency, external source or art was introduced. License terms/version grants remain unchanged. This is the ongoing source-change check, not a complete inherited-asset or binary-distribution compliance audit. No files were deleted or moved.

## User review and next step

1. Enter Play mode from Assets/base.unity and confirm the main menu opens without new name-setup errors or red Console errors.
2. In General Options, use **Update Namelists** (reload), confirm it, then return to the menu. Confirm no new error appears. The new reset description can be inspected without confirming a reset; resetting personal lists is not needed for this check.

After feedback, proceed to a bounded 03B3 data/graphics-registry packet. Trace dependency order before changing definitions: stats/override metadata, eager graphics registry, shared clothing/material/customizer consumers and leader variants. Split stats and graphics work, keep IDs stable, and carry actual 4X blockers through 05. No species curation or whole-file removal is authorized by this packet.
