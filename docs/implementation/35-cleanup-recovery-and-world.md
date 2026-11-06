# Cleanup, recovery and connected regions — 2026-11-05

The user authorized four follow-up items in order, continuing between stages unless design input helped. Spec35 records that direction and the subsequent capacity/self-damage answers. This batch adds no narrative, post-battle events, armor/price retuning or broad Help rewrite.

## 1. Cleanup deadlock and approved rule

The preserved seed202 starter-melee fixture stopped offensive actions at round21 and reached the200-round draw. Trace at rounds25/50/199 shows two downed Gnolls of bulk12; standing Gnoll Archer/Spitter Slug capacities7.5/11.67. Neither could fit a remaining opponent. The Lizard was downed and could not act. This was a real capacity deadlock, rather than an ignored available order.

The user approved: an empty predator can swallow a downed target regardless of its capacity. Standing targets and occupied predators retain normal capacity limits. `PredatorComponent.HasCapacityFor` is shared by resolution, primary/legacy AI, pounce checks, targeting and right-click commands. Existing anatomy/type eligibility, range and AP checks remain. Transfers/steals retain their own limits. The user separately removed overcapacity self-damage entirely. Legacy saved configuration keys remain inert, so enabling an old key cannot reintroduce damage. Actual digestion, containment and the200-round fallback remain.

Evidence: `Logs/followup35-stall-before.log`, synthetic `Logs/balance34f-1662acb716c1472ba80f7036e8a638fa/.../cleanup-trace.txt`; after rule, `Logs/followup35-stall-after.log`, `Logs/balance34f-1cad611391a3449fbf42aaf974eb0906/report.json`. The same pre-world-generation fixture completed in60rounds/~0.404s, with normal/result autosaves valid. Capacity-only native checks passed8 including setup; the final grouped run includes all7 capacity assertions. This is a reproduction/fix, not a general win-rate or battle-length claim. New-world generation changes the generation RNG sequence; future seed202 campaigns are not identical copies of this old fixture.

## 2–3. Quicker HP, longer injuries and focused comparisons

The intended distinction is general condition versus a specific lingering impairment. A full-HP mercenary may still have a serious injury and its penalties. Current provisional values:

| System | Ordinary day | Field rest | Town rest |
| --- | ---: | ---: | ---: |
| HP recovery, before Sluggish Recovery |10%|25%|35%|
| Injury calendar days advanced |1|1|1|

New Bruised/Battered/Mangled injuries take5/14/28calendar days. Existing saved remaining days are preserved. Treatment still costs10gold per injured member, removes2remaining days with at least1day left, and lowers the existing complication thresholds using the saved occurrence-time roll. Daily Moderate/Severe Damage complication chances remain5%/10%. Leader permanent-injury immunity, healing ceilings, damage/stat penalties and minimum1HP recovery remain. Surviving active contract enemies use the same ordinary10% daily HP rate. UI, developer lab and existing Help mechanical figures use the current rules; no Help prose overhaul.

The recovery question was optional tuning input; after allowing time for a reply, these provisional figures were explicitly stated as the working assumption. They remain adjustable. No unanswered choice was treated as consent to a road discount or another new mechanic.

Controlled Arm injury fixtures compare prior rate/progress/duration counterfactuals, current recovery and current treatment, across all three severities/modes:27samples. Additional permanent-complication rolls are disabled only inside these controlled timing fixtures, and the occurrence roll is0.9; the grouped gameplay checks cover the actual complication/treatment rules separately.

| Untreated current fixture | Starting HP | Ordinary HP recovery | Field-rest HP recovery | Town-rest HP recovery | Injury duration |
| --- | ---: | ---: | ---: | ---: | ---: |
| Bruised Arm |70%|3days|2days|1day|5days|
| Battered Arm |45%|5days|3days|2days|14days|
| Mangled Arm |20%|8days|4days|3days|28days|

Treated injuries took3/12/26days. A treated Bruised injury and ordinary HP can finish together; HP need not be strictly faster in every mild case. Native seven-day clock: the Mangled mercenary recovered full HP, retained21injury days, incurred weekly wages and survived canonical HP/injury serialization. No rest-based injury acceleration remains.

Evidence: `Logs/followup35-recovery3.log`; exact preserved report root in `Logs/followup35-recovery-directory.txt`. First fixture assertion was too strict about that equal-day treated Bruised case; the second run caught fixture-only immunity being rejected by canonical serialization. Both were corrected in the diagnostic setup, not by weakening production preservation checks. Earlier logs/files are retained and are not passed runs.

## 4. Larger coherent world and roads

The user selected160x120tiles with six towns. New geography uses broad elevation/moisture/climate patches of the existing grass/forest/hills/desert terrain, all traversable. Candidate maximin spacing distributes towns without a fixed visible grid. Towns precede roads; a minimum connecting network uses terrain-aware cardinal routes, with continuity validated after generation. The underlying terrain is not overwritten by roads.

Road overlays use original small UI geometry over the retained terrain sprites. They obey fog, sit beneath town/party/contract markers, do not intercept mouse input and use the existing348-tile virtualized viewport. All16connection masks have checked mesh output. Dragging remains bounded and independent of region size. Current road action costs remain the underlying terrain cost, pending user preference; off-road movement remains available.

`Roads` is an additive saved field. Old saves deserialize with null roads, keeping their dimensions, terrain, positions and towns. Roads are generated only for new campaigns. Dimensions up to256 are supported by validation; existing terrain identifiers and save schema are preserved.

Forty seeds passed spacing, all-four-terrain coverage, connected roads/no non-town dead ends, reproducibility and save round trips. Minimum town distances57–79tiles; same-terrain neighboring pairs92.7–93.9%;337–460road tiles; generation27.8–69.4ms, mean40.3ms in the measured editor run. These are diagnostics on this machine, not a performance guarantee.

The old80x60fixture opened without invented roads or geography changes. Pool/fog/geometry/dragging/movement checks passed. `Logs/followup35-world2-result.json`:571checks; `Logs/followup35-worlds.json` contains seed measurements. [Logical overview](../../Logs/followup35-region-overview.png) is a data diagram, not an in-game screenshot or replacement artwork.

## Final checks, build and scope

- `Logs/followup35-regressions-result.json`:890native grouped gameplay/traits/equipment/lab/save/capacity checks passed; actual Unity exit0.
- Final world2 run:571checks passed, actual exit0. Recovery3 report complete,27samples plus native wage/serialization assertions, actual exit0. Earlier diagnostic failures retained.
- Private Windows build `Builds/Warband-Review-20261105-ef036edd/Win64/VoreWar.exe`: actual Unity exit0, no C#/shader errors,41inherited compiler-warning kinds, no new warning messages. Seven existing license/attribution notices packaged and hash-verified.
- No graphical playtest claimed. Player checks: new campaign road visibility/dragging/travel; full-HP injured mercenary retains penalties while recovering; ordinary and auto-resolved battle save/load. Older saves intentionally keep their old map and any already-recorded shorter injury countdowns.

Exact source and `.meta` paths are recorded in CHANGES. Modification/addition notices dated2026-11-05; actual License4–5 reviewed, original attribution/legal notices retained. New code/geometry is this fork's addition using existing Unity APIs/no outside dependency or artwork. No scenes/prefabs/packages changed, files deleted/moved or personal saves accessed. All synthetic evidence and rejected generations are preserved; deletion interception stays active. Existing public-distribution/provenance/Corresponding Source gates remain open.
