# Compact zones, cleared roads and village fields — 2026-11-05

User feedback supersedes the previous160x120/maximin geography and25%/35% rest healing. The user explicitly selected120x80tiles after the intermediate size was proposed. Daily6actions is a stated provisional tuning choice to implement the requested smaller allowance; the optional6/8/10question did not receive a number before that assumption was stated, and no approval is inferred.

## Behavior

- New regions are120x80tiles divided logically into three columns/two rows of40x40zones. Each zone has one town, jittered six tiles around its center. The first town and starting position are in zone4: bottom-center, since map rows increase downward on screen. No visible zone borders or new factions were added.
- New companies receive6daily actions. Existing campaigns retain their stored daily allowance and remaining actions; partial days are not reset on load.
- Generation order: terrain, town locations, terrain-aware roads, road clearing to grassland, then cultivation of town neighbors. Every road tile has actual grassland terrain and costs1action. Forest/hill tiles outside the cleared/cultivated area retain their ordinary cost.
- Up to8adjacent tiles around each town become farming fields, excluding roads and town centers. The saved `Farms` mask selects the original4X`StrategicTileType.field` sprite variants4/22/23/24 over cultivated grassland. This is visual land use, not a return of4Xfarm production or settlement management. Tactical battles on cultivated grassland use the existing grass battle provider.
- Existing road-bearing saves retain their dimensions/towns/road routes, but canonical validation clears road tiles and supplies the farm mask. Unrelated terrain and partial-day progress are preserved. Older pre-road saves keep their original geography, with no invented road network. Existing in-progress battle snapshots keep their saved battlefield.
- HP recovery is exactly10%daily,20%during field rest and30%during town rest, rounded up before existing Sluggish Recovery and capped at the healing ceiling. Injuries remain5/14/28calendar days; treatment, complication odds, leader immunity and sparring eligibility are unchanged. The developer lab uses the shared production rate helper; UI/Help mechanical figures are corrected without rewriting Help.

## Scope and evidence

Modified sources: `Assets/Scripts/Core/CompanyWorldGeneration.cs`, `Assets/Scripts/Core/CompanyCampaign.cs`, `Assets/Scripts/Core/CompanyInjuries.cs`, `Assets/Scripts/Subwindows/CompanyCampaignView.cs`, `Assets/Scripts/Subwindows/CompanyRecoveryUI.cs`, `Assets/Scripts/Utility/Texts/HelpText.cs`, `Assets/Editor/TraitCurationAudit.cs` and `Assets/Editor/CompanyBalanceAudit.cs`. No new artwork/assets/dependencies, scene/prefab/package rewrites or file deletion/moving. All earlier pending work preserved.

Focused native world1 passed701checks across40seeds; world2 adds explicit six-action new-campaign checks, passing741. Covers connectivity, one town per zone/bottom-center start, compact spacing, terrain coherence, deterministic generation, actual cleared-road/field costs, exact non-road farm neighbors, farm serialization, original farm sprites in the native UI, fog, pooled map geometry/dragging and old-save road normalization. Town minimum separations31–42tiles rather than the previous57–79. World1 generation averaged13.3ms, maximum35.3ms on this machine; this is diagnostic timing, not a guarantee.

Reports: `Logs/compact36-world-result.json`, `Logs/compact36-world2-result.json`, `Logs/compact36-worlds.json`; logs `compact36-world1.log` and `compact36-world2.log`. Existing160x120road fixture keeps dimensions/towns and its partial4-of10action day, while rough road tiles clear; old80x60pre-road fixture remains unchanged. No personal saves were used. All synthetic files are preserved with deletion intercepted.

Recovery mode explicitly asserts10/20/30rates and retains27controlled timeline/treatment comparisons plus the native seven-day wage/serialization check. First recovery run completed without errors; final recovery2 includes the provisional six-action default. Final recovery2 report completed27samples without errors; its root is recorded in `Logs/compact36-recovery-directory.txt`. World runs and the private build exited0. The recovery2 waiting shell was interrupted only after the completed native report and editor shutdown; that shell is not reported as a successful exit. Exact evidence is in `Logs/compact36-result.json`. No new framework or broad unrelated regression repetition.

Player review: start a new campaign to see the compact layout/start and six-action day; follow roads and inspect nearby fields; compare field/town rest; load an older save to confirm its map size and stored allowance remain intact. No graphical playtest claimed by the native property checks.

Modification notices dated2026-11-05 and exact CHANGES paths accompany all eight source edits. License sections4–5 reviewed, license hash and original attribution retained. Original fork code and already-shipped farm sprites only. Existing provenance/public-distribution/Corresponding Source gates remain open; private build validation is not public-release clearance.


Private review build: `Builds/Warband-Review-20261105-b3fdf245/Win64/VoreWar.exe`, actual Unity exit0, no C#/shader errors,41inherited compiler-warning kinds and zero new messages. Seven existing license/attribution notices packaged and hash-verified. [Logical overview](../../Logs/compact36-region-overview.png) illustrates the geography only; it is not a game screenshot or replacement art.
