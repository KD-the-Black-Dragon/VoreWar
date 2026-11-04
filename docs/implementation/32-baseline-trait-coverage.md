# Baseline trait coverage review

2026-11-04. Source review only; no gameplay, Help source or build changes in this packet. This is a proposal awaiting names and scope decisions. Prior further-pruning source batch remains uncompiled.

## Requested standard

Three tiers I / II / III. Flat damage adjustments 1 / 2 / 4; final accuracy adjustments 10 / 20 / 30 percentage points. Preserve existing special-purpose traits independently. Do not revive retired traits to fill gaps.

| Family | Positive candidate | Negative candidate |
| --- | --- | --- |
| Melee accuracy | New name | Clumsy, currently also reduces damage |
| Ranged accuracy | New name | New name |
| Magic accuracy | MagicProwess | New name |
| Vore accuracy | New name | New name |
| Melee damage | StrongMelee | WeakAttack |
| Ranged damage | KeenShot | RangedIneptitude |
| Magic damage | New name | New name |
| Incoming damage | New name for increased damage | Resilient for reduced damage |
| Incoming attack accuracy | New name for easier to hit | Dodger for harder to hit |
| Incoming vore accuracy | EasyPrey | AwkwardShape |

Candidates require retuning, not merely adding suffixes. MagicProwess currently shifts the stat ratio. StrongMelee/WeakAttack/KeenShot/RangedIneptitude are multipliers with legacy virtual-stat adjustments. Clumsy bundles melee accuracy and damage; propose accuracy-only with WeakAttack supplying damage penalty. Dodger currently applies to melee/ranged only. EasyPrey is currently +15 percentage points, AwkwardShape multiplies incoming vore chance by0.75. Resilient subtracts1 from melee/ranged weapon damage only, not magic. HardPrey has a nutrition drawback and should remain a separate biological trait. Ravenous/EasilySatisfied are conditional vore chance multipliers and should remain distinct.

## Decisions needed before implementation

User chooses ten missing family names from table. Clarify whether incoming damage affects melee/ranged/magic only (proposed) or also digestion/status effects; whether Dodger incoming accuracy includes resistible magic (proposed); whether damage reductions floor successful damaging hits at1 (proposed) or allow0. Proposed families use strongest tier only, opposite signs cannot coexist within a family. Acquisition/upgrades are separate future design; no automatic trait evolution implied by I/II/III.

## Help proposal

Combat Bonuses; Combat Penalties; Personal Development; Vore and Digestion; Special Abilities; Species and Biology. Alphabetical within each category with tiers adjacent; one primary category per trait. Current Help simply iterates enum IDs and filters retired traits/IDs>=1000. Prefer explicit category metadata, preserving the retirement filter and the canonical descriptions.

## Other optional foundations

Consider maximum HP/mana, daily recovery, critical chance, injury resistance/recovery and escape/containment odds. Existing ManaRich, Regeneration, QuickLearner/Foolish, Talented, EscapeArtist/Lethargic and IronGut/Nauseous partially cover these. Keep fixed AP/species balance and injury review separate.


## Approved implementation — 2026-11-04

This section supersedes the proposal's unnamed families and pending scope decisions above. Source implementation is complete; compilation/runtime/graphical verification has not been run for this packet. The current standalone build008d6398 predates this work and the preceding pruning batch.

28 families, each with exclusive I / II / III tiers (84 canonical traits). Added73 explicit unused enum IDs600–672; reused11 canonical base IDs. Existing numeric IDs remain intact. MetalBody is a compatibility alias for canonical AwkwardShape (HardPrey I), not a second active trait. Both lose the old nutrition drawback. Display labels and hover parsing support tier names and older names. The ordinary trait lookup returns canonical tier descriptions instead of old multiplier/bundled definitions.

| Effect | Positive family | Negative family | I / II / III |
| --- | --- | --- | --- |
| Melee accuracy | Precise | Clumsy | 10 / 20 / 30 percentage points |
| Ranged accuracy | Deadeye | Nearsighted | 10 / 20 / 30 percentage points |
| Magic accuracy | MagicFinesse | ErrantMage | 10 / 20 / 30 percentage points |
| Vore accuracy | WideMaw | WeakGullet | 10 / 20 / 30 percentage points |
| Melee damage | StrongMelee | WeakAttack | 1 / 2 / 4 |
| Ranged damage | KeenShot | RangedIneptitude | 1 / 2 / 4 |
| Magic damage | PowerCaster | MagicIneptitude | 1 / 2 / 4 |
| Physical damage received | WeakBones (more) | Resilient (less) | 1 / 2 / 4 |
| Magic damage received | MagicVulnerable (more) | MagicResistance (less) | 1 / 2 / 4 |
| Enemy attack accuracy | Flatfooted (more) | Dodger (less) | 10 / 20 / 30 percentage points |
| Enemy vore accuracy | EasyPrey (more) | HardPrey (less) | 10 / 20 / 30 percentage points |
| Maximum HP | Hearty | Frail | 1 / 2 / 4 per character level |
| Maximum mana | ManaBattery | Fizzler | 1 / 2 / 4 per character level |
| Critical chance | Assassin | Brutish | 5 / 10 / 15 percentage points |

Dodger/Flatfooted cover hostile melee, ranged and resistible magic, not the separate vore defense family. Physical damage modifiers cover melee/ranged weapon attacks (including opportunity attacks); magic modifiers cover damaging spell hits. These do not modify ongoing digestion or status ticks. No AP, movement, critical damage multiplier, injury resistance or injury recovery changes were added. MagicResistance is repurposed from spell accuracy resistance to flat spell damage reduction. Other biological/conditional traits stay separate.

Accuracy is additive at final odds and clamped0–100%; existing downed/friendly automatic vore cases remain automatic. Flat outgoing weapon damage participates in normal critical/graze multipliers; flat physical resistance/vulnerability is applied afterward. Successful weapon/damaging spell hits floor at1 after block/resistance, before absorption by an existing barrier. HP floors at1; mana at0. Maximum-resource decreases clamp current resources without adding healing. Per-level caps update dynamically and level-up clamps any over-cap current values.

Explicit trait grants replace an earlier tier or opposite trait of the same effect; lower-tier grants do not downgrade the same family. Shared/persistent/conditional grants use the same path. Legacy or directly assigned conflicts resolve to the highest tier; equal tiers use stable numeric IDs. No automatic acquisition/upgrade schedule was invented. Cached resolved modifiers are rebuilt with existing trait recalculation and after loading via existing InitializeTraits; no new serialized cache is needed.

Help retains the retirement filter, folds the legacy MetalBody entry into HardPrey and groups traits into Combat Bonuses, Combat Penalties, Personal Development, Vore and Digestion, Special Abilities, Species and Biology. Families are alphabetized with I/II/III adjacent. Shared tooltips and Help use the same tier descriptions.

## Next grouped verification

No tests/framework were added. Update the existing authorized trait audit's stale label expectations before the next requested grouped compilation/runtime review. Cover all28families at each tier; final odds/clamps, minimum hit damage after graze/block/elemental resistance, critical adjustment and opportunity evaluation, per-level HP/mana and resource clamping, grants/upgrades/opposite exclusivity/shared cleanup, Odin round-trip/recalculation, MetalBody compatibility and one HardPrey Help entry. Preserve synthetic files and suppress deletion as required by AGENTS.md.

After a build is actually produced, one player-facing review should check Help categories/tier descriptions, a character's tier label and hover text, and displayed weapon/spell odds/damage versus battle log. No manual test is requested before that grouped validation. Injuries remain deferred.


## Combined validation completed — 2026-11-04

The combined spec33 milestone validates all28families at I/II/III, exclusive upgrades, final accuracy and metadata parsing, along with injury rules. Final runtime6 passed457 checks; standalone handoff build Warband-Review-20261104-1b8b2f8d passed0. See spec33 and Logs/traits-injuries33-result.json. Earlier uncompiled notices above describe earlier checkpoints, not the final handoff.
