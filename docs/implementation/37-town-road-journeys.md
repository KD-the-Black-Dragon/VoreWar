# Five-day town journeys and two road exits — 2026-11-05

User accepted the grassland roads and farming artwork, requested nearest-town travel around30road moves (roughly25–35), and exactly two outgoing roads from every town. At the existing6daily actions this targets approximately five travel days.

## Implementation

The120x80region, six logical zones and bottom-center start are retained. Town columns are approximately30tiles apart and the two rows approximately30tiles apart, with shared row/column offsets of±2tiles. This reduces actual settlement spacing while keeping room for off-road exploration. No additional towns/actions or new terrain/artwork were introduced.

The minimum connecting tree was replaced by one six-town perimeter circuit. Each town has two distinct physical road exits to two different neighboring towns. Short corridors do not merge into trunks or introduce junction shortcuts. Optional small outward bends add exactly2moves; total connection lengths stay within25–35moves. Length takes priority over terrain avoidance, and the accepted post-routing clearing still turns every road tile into one-action grassland.

Generation checks the resulting road graph: one connected component, exactly two adjacent road tiles at every road tile (including each town), six distinct town-to-town connections and25–35actual moves per connection. Farms, their original sprites, HP recovery10/20/30 and six daily actions remain unchanged. No narrative/Help rewrite or unrelated balance work.

Only new campaigns receive the closer placement and circuit. Existing saves retain their recorded dimensions/town locations/roads; they are not silently rerouted or moved. Existing road clearing/farm normalization continues as accepted in spec36.

## Evidence

Focused Unity world1 passed821checks across40seeds; world2 checks the final generation-time invariants as well. Evidence remains under `Logs/roads37-world1-result.json`, `Logs/roads37-runtime-result.json`, `Logs/roads37-worlds.json` and matching world logs. Tests measure road walking distances independently for the nearest other town and retain the earlier farm/fog/artwork/pooled UI/dragging/save/legacy geography checks. All data is synthetic under Logs with deletion intercepted; no personal saves are accessed.

Measured across40seeds /240connections /240towns:

| Measurement | Result |
| --- | ---: |
| Connection lengths |28–34moves|
| Mean connection length |31.24moves|
| Nearest-town road distances |28–34moves|
| Mean nearest-town road distance |30.125moves|
| Mean nearest distance /6daily actions |5.02daily action budgets|

The final row is an action-budget equivalent, not a promise of an exact calendar-day count: remaining actions and when the player ends the day affect arrival timing. Every measured town has two physical exits reaching different towns. The geometry is a single loop, rather than a two-edge logical graph whose roads merge near town. This small seed sample is not a statement about final campaign economics.

Final process/build evidence is recorded in the checkpoint and `Logs/roads37-result.json`. Player review requires a new campaign: follow either road from the starting town, inspect two exits at each town and assess the shorter travel pace. No graphical playtest is claimed by the property/mesh checks.

## Licensing and scope

Modified only `Assets/Scripts/Core/CompanyWorldGeneration.cs` and fork editor source `Assets/Editor/TraitCurationAudit.cs`. Both retain earlier notices and carry dated2026-11-05 modification notices; exact paths are in CHANGES. License4–5 reviewed, original attribution and license preserved. Original fork code only, no outside dependencies/assets, scene/prefab/package changes or file deletion/moving. Existing public-distribution/provenance/Corresponding Source obligations remain open. A private review build is not public-release clearance.


Final world2 passed821checks with actual Unity exit0; world1 report also passed821 but its waiting wrapper was stopped after native completion/editor shutdown and is not claimed as exit0. Private Windows review build `Builds/Warband-Review-20261105-17b72003/Win64/VoreWar.exe` exited0, no C#/shader errors,41inherited compiler-warning kinds and zero new messages. Seven existing notices packaged/hash-verified. [Logical overview](../../Logs/roads37-region-overview.png) is a data diagram, not a game screenshot or replacement artwork.
