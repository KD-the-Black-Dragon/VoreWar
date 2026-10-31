# Narrative routing inventory

**Packet:** 01B; 2026-10-30. Source-level evidence only.

**Subsequent implementation:** [04B1](../implementation/04b1-central-combat-text-reset.md) replaces the central registry/typed prose and removes Human dummy contexts. Original counts/line anchors below are historical. [04B2](../implementation/04b2-callback-and-descriptor-reset.md) now covers inventoried free-form callbacks and descriptors/synonyms, preserving already-functional messages; runtime battle verification awaits approved species. 04A has separately retired the 4X events.

These are legacy code identifiers, not new narrative. Entry counts are lexical occurrences of new EventString within each list section, so comments/conditional compilation can affect them. They are an estimate of replacement coverage, not executed runtime counts. Existing prose is intentionally omitted.

| Message list | Source | EventString occurrences | Race-specialization tokens | Conditional tokens |
|---|---|---:|---:|---:|
| SwallowMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L199) | 460 | 598 | 162 |
| RandomDigestionMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L1260) | 470 | 278 | 384 |
| BellyRubMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L2308) | 257 | 201 | 228 |
| BreastRubMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L2875) | 39 | 22 | 39 |
| TailRubMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L2986) | 77 | 65 | 76 |
| BallMassageMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L3079) | 44 | 23 | 37 |
| BreastVoreMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L3183) | 43 | 19 | 40 |
| BladderMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L3250) | 31 | 6 | 25 |
| CockVoreMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L3297) | 38 | 17 | 20 |
| AnalVoreMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L3390) | 70 | 32 | 40 |
| UnbirthMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L3545) | 40 | 18 | 25 |
| TailVoreMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L3625) | 86 | 63 | 54 |
| DigestionDeathMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L3744) | 262 | 207 | 184 |
| AbsorptionMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L4340) | 351 | 200 | 302 |
| TransferMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L5055) | 30 | 0 | 30 |
| KissTransferMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L5131) | 13 | 0 | 13 |
| VoreStealMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L5170) | 43 | 0 | 43 |
| BreastFeedMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L5277) | 4 | 0 | 3 |
| CumFeedMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L5284) | 4 | 0 | 3 |
| RebirthMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L5291) | 79 | 24 | 79 |
| GreatEscapeKeepMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L5392) | 34 | 10 | 34 |
| GreatEscapeFleeMessages | [StoredLogTexts.cs](../../Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs#L5439) | 17 | 0 | 16 |

## Routes outside those lists

| Route | Required audit |
|---|---|
| TacticalMessageLog.EventDescription and Generate methods | Inline prose, SimpleText branches, stored-list calls, literal event Message/Extra payloads, fallback behavior, and formatting. |
| LogUtilities and PreyLocStrings | Descriptor and synonym prose, pronouns, fallback Units, and RNG use. |
| Actor_Unit / PredatorComponent / Traits / spells / equipment / tactical utilities | Callers can register free-form miscellaneous text; replacing stored lists alone does not cover them. |
| MessageLogPanel | Display/filter preference behavior; turning off fluff is not deletion of old prose. |
| EventList / CustomEventList / StreamingAssets/events.txt | Separate 4X choice-event catalog and effects, not the tactical prose registry. |
| Help / Bestiary / tooltips / species descriptions | Related player-facing text requiring coordinated retirement or later rewrite; do not accidentally clear required mechanics/stat descriptions. |

The central registry has 22 list sections and 2492 lexical EventString occurrences. All retained categories need at least one neutral usable fallback before old variants can be retired.
