# Phase 2 — Functional cleanup closure

Date: 2026-11-01. User accepted 03B8, requested larger milestones, then authorized the consolidated remaining cleanup. This is the final cleanup milestone before spec06, not a claim of an already playable new campaign.

## Final source change

Only AssetBundleLoader.cs changes in this milestone. Refresh all C# sprite-member readers after the 91 class payload and rejected projectile retirement. Clear the following 14 previously loaded arrays whose reviewed former consumers were rejected definitions/effects and whose current compiled-source readers are absent. Keep declarations and explicitly clear rather than leave stale references on repeat loading. Do not treat unrelated pre-existing unreferenced fields as an invitation to broaden cleanup.

- `NewimpBase` — previously `Impfinished`
- `Scylla` — previously `Scylla`
- `Harpies` — previously `Harpies`
- `Crypters` — previously `Crypters`
- `Bears` — previously `Bears`
- `Vipers1` — previously `Vipers1`
- `Equaleon` — previously `AnthroEqualeon`
- `CentaurParts` — previously `CentaurParts`
- `YordlesCustomization2` — previously `YordleCustomization2`
- `DryadSprites3` — previously `Dryad3`
- `DryadSprites5` — previously `Dryad5`
- `AngelProjectile` — previously `AngelProjectile`

Remaining 187 simple load expressions plus five shared concatenated bindings (199 asset requests total) and all four mixed bundle opens are unchanged. No sprite/prefab/scene/bundle/manifest edit or path deletion/move. Exact before/expected edit evidence and current reader inventory are in Logs/cleanup-closure-*.

## Consolidated disposition

| Area | Closure evidence / retained boundary |
|---|---|
| Settings and navigation (02) | User accepted the repaired settings pages, persistence and developer Race Editor gate. Retired editor/cheat/4X/bestiary entry points are blocked/hidden. Global cosmetic/content preferences retained; balance controls fixed at reviewed defaults. No new player-facing options invented. |
| Species generation and identities (03) | Code-owned curated list remains empty, permanent rejection list remains90. Generation/selection/form/copy routes use policy, no fallback playable species. Numeric IDs and existing raw override rows retained; disabling nonretired species blocks new generation, not existing individuals. |
| Species stat/render payloads (03) | 90 rejected stat prototypes, 91 eager graphic instances and their disconnected class bodies retired. Exact remaining64species stat/registry mappings preserved. Shared FairyType/FairyUtil retained after compile exposed Unit's caller. |
| Resources/serialized data (03) | 156 old plus12 newly orphaned loader requests cleared; shared187simple requests plus five concatenated bindings/mixed bundles retained. 156 obsolete prefab blocks removed only after Unity confirmed they were ignored static data; complete engine snapshots unchanged. No asset-exclusivity, memory saving or physical-art purge claim. |
| Narrative/events (04) | Accepted04A/04B1/04B2 disable old4X events and reset reviewed narrative/callback prose to functional placeholders. New road events and user-authored narrative remain later work. Battle-log checks still await curated species. |
| Legacy campaign/load entry (05) | 05A1 closes old creation/load/preview/import; State.Load reports unavailable and PreviewSave does not deserialize. No old-save conversion promised. Current cleaned menus cannot start a playable campaign until06. |
| Strategic simulation/providers (05) | 05A2–A6 disconnect old turn scheduling/input/campaign constructor, lifecycle/economy providers, battle building/empire effects and diplomacy/XP reinjection. Dormant methods/components are not approved campaign services. |
| Combat ability retirement (05) | User accepted consolidated05A3B17 and later provider work; retired resurrection/conversion/leadership effects suppressed, raw IDs preserved. Normal vore/digestion, AI, summons/disguises and tactical grid kept. New Downed/temporaryHP/injury/leader-fate systems are intentionally not implemented by cleanup. |

## Intentional residuals — not blockers to beginning06

- World/Army/Empire/Village types still provide legacy-shaped tactical item, roster, terrain and side context. New company state must own its roster; never reuse Army.Prune, old income/training/recovery or campaign scheduler as new company behavior.
- Shared sprite arrays, generic clothing, palettes, base graphics classes, FairyUtil and mixed bundles remain where retained code depends on them. File names associated with rejected species do not establish exclusive ownership.
- Numeric enum tombstones, raw mapping IDs, preference/save structures, scene component fields and inactive UI source remain for compatibility and traceability. No path deletion/move is needed to build the new loop.
- Old snapshot/writer structures remain; legacy loading is blocked. Spec06 must introduce versioned new-format state/checkpoints rather than re-open State.Load on old worlds.
- Help rewrite, new campaign saving/navigation, equipment expansion, wages/contracts, character attrition and leader revival are later specs06–12. Optional new events are13. These are planned additions, not cleanup regressions.
- Known warning from03B8: Unit.PreferredVoreType no longer explicitly assigned by removed definitions. It retains its existing default for remaining units; no balancing change was made to silence the warning.
- Artwork provenance and distribution obligations, full player build/release checks and possible future bundle repacking remain release gates. They do not require inventing rights or deleting artwork now.

## Verification / completion boundary

Final compilation PASSED exit0 (Logs/cleanup-closure-compile-complete.log), no C# errors/logged exceptions or new compiler warnings versus03B8. Explicit base.unity opening PASSED exit0 (cleanup-closure-scene-complete.log), without errors/exceptions. Exact14-edit reconstruction, retained187simple/5concat bindings and199assetcalls/4bundleopens, unchanged scene/prefab bytes, notices/CHANGES/License hash and scoped whitespace checks passed. No Unity process remains. Earlier preliminary logs retained; final-source checks use the complete logs. Prior manual screens/startup through03B8 are user-accepted; no repeat menu test requested for14 unreferenced binding removals. Actual battle behavior remains UNVERIFIED in the intentionally empty curated roster. First approved species milestone must execute retained combat, summon/disguise, digestion, retreat and log scenarios. No test species is approved merely to pass cleanup tests.

Functional cleanup scope02–05 can close on the recorded source/agent checks, with these explicit deferred gates. This is not an exhaustive assertion that every old line/asset is gone, every combat path has been playtested, or the project is release-ready. The next milestone is06 campaign foundation, with player-owned MVP species choices needed before a playable roster is introduced.

## GPL modification check

Loader carries dated2026-11-01 conversion notice; CHANGES records the exact source path and completed edits. Prior notices/attribution retained. No external source/dependencies/assets introduced. Dated notice/CHANGES coverage, unchanged License.txt hash and source scope checked; previous attribution preserved. Separate source/binary distribution and asset-license review still required before release.


Final inventory correction: ZeraBalls and ZeraFrontBelly concatenated sheet groups are also unreferenced and cleared. Crux concatenation remains because CruxClothing reads it. Historical 03B5 counts referred to simple assignments, not every asset call; preserve this distinction. Final source retains 187 simple bindings plus five concatenated bindings, totaling 199 asset requests.
