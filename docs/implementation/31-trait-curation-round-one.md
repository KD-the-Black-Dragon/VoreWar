# Trait curation — round 1 (2026-11-04)

Review only. Injuries deferred to phase two. No new trait retirement authorized or implemented.

HelpText.Traits already excludes TraitsMethods.IsRetired entries and custom IDs >=1000. Tags.cs IsRetired is enforced by Unit.HasTrait and trait collection paths. Therefore explicitly retired revival/allegiance traits already do not appear in Help; no redundant source change needed. Presence in Help means retained definition, not proof of availability in a curated species pool. Legacy descriptions can disagree with current mechanics.

## Explicit cheat candidates for user judgment

- Alacrity: +3 melee/vore attacks.
- LightningSpeed: +10 speed bonus, +5 melee/ranged/vore/spell attacks. Positive speed no longer increases fixed species AP, but attack bonuses remain.
- DivineBloodline: digestion/weapon damage reductions, initial digestion immunity, additional attacks, amplified passive healing, large permanent level-up bonuses and unrestricted level choices.
- GeneEater: booster applies5x vore XP and50x absorption XP; enum summary understates the latter.
- InstantDigestion / InstantAbsorption: millionfold respective rate multipliers.
- Inescapable / Irresistable (existing spelling): extreme escape suppression / swallow-odds multiplier, rather than mathematical guarantees in every exceptional case.
- UnlimitedCapacity:1000x capacity; Inedible: prevents ordinary swallowing.
- Titanic / Colossal / Huge / Tiny:3x /2.5x /2x /one-third size. Huge enum summary incorrectly says3x; booster is2x.
- AdaptiveTactics:2x action XP.
- HealingBelly: friendly-contained-prey healing, linked to Endosoma; not necessarily an automatic rejection if deliberately redesigned.
- Assimilate / AdaptiveBiology / InfiniteAssimilation: acquire traits from absorbed prey, respectively limited/self-replacing, rotating3, and uncapped.
- SynchronizedEvolution: extends acquisition to species-wide progression; especially poor fit for individual-company progression.
- KillerKnowledge / EssenceAbsorption: permanent all-stat growth from repeated kills/digestions. Weapon kill version is largely obsolete under digestion-only permanent loss.

DigestionConversion / DigestionRebirth are also labeled cheats but are already retired, so excluded from new decisions.

## Other judgment candidates

AllOutFirstStrike (5x first weapon damage or3.25x first vore odds); Extraction (prey trait theft); Annihilation (prey level theft); Endosoma (alternative non-digestion defeat logic conflicts with settled combat conclusion rules); Changeling / GreaterChangeling / InherentGlamour (identity/form copying); MutualBiology (shared health/damage/healing); Multifaceted (large highest-stat-dependent package with obsolete eight-stat descriptions); CurseOfSacrifice / CurseOfPhasing / CurseOfCraving / CurseOfPreyportaion (automatic swaps/teleports/containment); PermanentGrowth and other growth traits (potential permanent scaling beyond normal veteran advancement).

Powerful but readily framed as biology/talent, requiring tuning rather than presumed removal: AcidImmunity, Elite, Juggernaut, PeakCondition, StretchyInsides, KillingMomentum, LightFrame, WildFury, SweepingStrikes, BoundWeapon. Ordinary quirks such as Berserk, Submissive, Cowardly, PoorConstitution, FoodComaProne and Slacker should survive this thematic first pass, subject to later balance review. Some positive speed and old graze descriptions are stale after the stat/AP redesign.

## Obsolete strategic candidates

Hoarder (village gold), Cartography (legacy army terrain), Eeveeolutionist (legacy world evolution modifier), Infiltrator, Untamable, RaceLoyal and Camaraderie (old infiltration/allegiance framework). These are recommendations for formal retirement, not already marked retired by IsRetired. Some retained runtime references exist: do not merely remove their enum IDs or assume every reference is inert.

## OC provenance

No surviving trait can be confidently called an exclusive OC superpower from the inspected current definitions. Prior spec21 ownership audit confirms EntropicChaos animation/attack was gated to Tatltuae and was removed with the exclusive OC branches. That is evidence about the removed attack, not proof that any retained generic trait originated with that OC. Do not label Multifaceted, curses or other unusual traits OC-exclusive without historical evidence. They are flagged above on mechanical grounds instead.

Sources: Assets/Scripts/Enums/Tags.cs, Races/Traits.cs, Utility/Texts/HelpText.cs and HoveringTooltip.cs, Entities/Units/Unit.cs, Actor_Unit.cs and PredatorComponent.cs; prior05a3b17 and21 ownership records. Descriptions are review summaries, not a claim of exercised runtime behavior. No tests/build/playtest or source/assets changes; preserve pending outcome typography edits. Next: user judgments in grouped batches, then implement approved retirements and Help alignment together. Existing provenance/distribution obligations unchanged.


## Implemented user decisions — 2026-11-04

Authoritative retirement filter extended with these36 traits:

Alacrity, LightningSpeed, DivineBloodline, GeneEater, InstantDigestion, InstantAbsorption, Inescapable, Irresistable, UnlimitedCapacity, Inedible, HealingBelly, Assimilate, AdaptiveBiology, InfiniteAssimilation, SynchronizedEvolution, KillerKnowledge, EssenceAbsorption, Extraction, Annihilation, Endosoma, Changeling, GreaterChangeling, InherentGlamour, Multifaceted, CurseOfSacrifice, CurseOfPhasing, CurseOfCraving, CurseOfPreyportaion, PermanentGrowth, Hoarder, Cartography, Eeveeolutionist, Infiltrator, Untamable, RaceLoyal, Camaraderie.

Titanic, Colossal, Huge, Tiny retained; AdaptiveTactics grants10% extra action XP. Everything else previously listed but not explicitly rejected remains, including AllOutFirstStrike, MutualBiology and the other growth traits. Only the four explicitly listed curses retired; CurseOfImmolation and CurseOfEquivalency require new judgment. Shared GetTrait/HasTrait/GetTraits/Help/random-pool filters enforce retirement, with original IDs and dormant source definitions preserved for compatibility. No personal saves rewritten. Prior outcome styling edits preserved. Source diff Logs/traits31-source.diff; source review only, no compile/build/playtest or automated tests run.

## Round-two candidates — judgment only

- TraitBorrower / Symbiote: retained borrowing/sharing traits through containment, adjacent in concept to removed assimilation mechanics.
- Parasite / CreateSpawn / InfectiousReproduction: host infection, creation of additional creatures, including offspring from poisoned kills. Need deliberate role/species restrictions if retained.
- AstralCall / AntPheromones: automatic additional units at battle start; sapient copies can bypass a small-company deployment budget.
- CursedMark: bundled combat bonuses and penalties, inherited by the predator that digests its carrier.
- BookEater: permanently learn spellbooks as innate spells, potentially bypassing the two equipped spell slots.
- Clever:30% less XP required, considerably stronger than the newly modest AdaptiveTactics; plausible talent, but tuning overlap.
- DoubleAttack / Maul: additional attacks through reduced AP spending; not retired by this request, but worth checking against all-remaining-AP combat and anti-kiting goals. Do not re-question previously retained LightFrame/WildFury here.
- Pathfinder / SlowBreeder / ProlificBreeder: old army terrain or settlement reproduction effects, with little purpose in the conversion.
- Resourceful / BookWormI / BookWormII / BookWormIII: legacy extra item slot or starting spellbooks; need relevance review with nine-slot loadouts rather than assuming their help text describes an active company benefit.
- CurseOfImmolation / CurseOfEquivalency: two additional curses outside the four user explicitly removed; respectively periodic fire damage around/in predator, and per-turn highest-to-lowest stat redistribution.

These are candidates, not new retirements. Injuries remain phase two. No source changes to these undecided traits.


## Remaining stat/name review — 2026-11-04 (discussion only)

User asks about obsolete-stat traits such as PackWill. Four-stat compatibility mapping remains active: END->STR, DEX->AGI, WILL/Mind->INT, STM->VOR. Unit.GetTraitBonus evaluates aliases per trait and takes the strongest contribution, but distinct trait definitions still add independently. PackWill and PackMind therefore both boost INT; PackDexterity and PackDefense both boost AGI; PackStomach and PackVoracity both boost VOR. PackStrength boosts STR. Recommend four clearly named pack traits plus separate judgment on keeping PackTactics (all stats). No removal/rename implemented by this review.

Descriptions needing current-stat names: PackStat embeds old enum name; Finesse uses AGI rather than DEX, DexterousDefense AGI rather than DEX, BoundWeapon INT rather than Mind; BlessingOfNature STR rather than END, BlessingOfEarth INT rather than WILL, BlessingOfWater INT rather than Mind. This does not by itself mean broken math; the alias mapping handles old references. Blessings are mechanically elaborate automatic/reciprocal buff systems, worth thematic judgment independently of wording.

Further functional review candidates: AccuteDodge still boosts outgoing GrazeRateShift, whereas the new weapon resolution uses a stat-independent miss-rescue roll and no graze-stat check; description appears ineffective. FastMovement/VeryFastMovement/VerySlowMovement and other speed traits retain old speed claims despite fixed species AP. QuickShooter and FastCaster retain the old extra-attack/cast action-economy approach; review against DoubleShot/Maul before keeping duplicate or unrestricted equivalents. Preserve previous explicit keeps pending user decision. No source edits, new tests/build/playtest or files moved/deleted; injuries remain deferred.


## Pack decisions implemented — 2026-11-04

Retired PackWill, PackDefense, PackStomach, BlessingOfNature, BlessingOfEarth, BlessingOfWater, BlessingOfFerocity. PackDexterity now displays as PackAgility, with unchanged stored ID and AGI effect. PackStrength, PackMind, PackVoracity and PackTactics remain as requested. Pack descriptions canonicalize old stat names; Finesse/DexterousDefense/BoundWeapon tooltips use AGI/INT. No new decisions on AccuteDodge, speed traits, QuickShooter or FastCaster. No compile/build/playtest; injuries deferred.


## Remaining combat-fit review — 2026-11-04 (no implementation)

User requests another review. Current retained definitions still include TheGreatEscape (live digestion-damage exemption; tooltip promises old contained-only battle conclusion and assumed escape, conflicting with explicit digestion-only death/no assumed outcomes), Donor (passes selected traits after absorption), ExtraNutritious (RandomStatIncrease once per prey level on digestion, outside normal XP growth), Defenseless (1000x incoming vore odds, an extreme weakness), ArtfulDodge (FlatHitReduction*=.9 affecting weapon/vore odds; overlaps new Dodger's weapon-only flat10percentage-point subtraction), Temptation (tied to retired Charm/Hypnosis), PredGusher (old conversion/rebirth override), Infertile (legacy reproduction), EvasiveBattler (old special retreat claim; no direct checked modern tactical caller), WillingRace (still causes WillingPrey to persist through status handling despite name referring to a whole race).

More judgment candidates: MadScience (random spell special action), ShunGokuSatsu (named strong attack with vore), MagicSynthesis (gains single-use copy of incoming spell and restores75% of its mana cost in inspected damage-spell branch; can provide spells beyond two equipped slots), NaturalCaster (innate spells similarly outside equipped slots), Brainless (INT-compatibility multiplier0, intended minimum1; meaningful animal specialization vs questionable ordinary sapient trait). DyingStrike/ArcaneMagistrate/Frenzy retain some ordinary-kill assumptions; they may have digestion or nonkill benefits and need inspection before removal. Do not treat all as broken or retire without user judgment. Previously explicitly kept powers such as MutualBiology, AllOutFirstStrike, Growth/size traits are not silently reopened or removed.

HelpText.Traits lists retained enum definitions, not exclusively traits reachable by curated species. Current standalone README still19b7a6be predates trait curation; source/Editor reflect edits after compilation, older executable does not. No new source/assets/settings changes or tests/build/playtest in this review. Existing pending outcome/trait edits preserved; injuries remain separate.


## Final trait-fit scan — 2026-11-04 (review only)

Do not reopen explicit keeps. Remaining concrete judgments: Dazzle uses separate INT comparison to cancel attack and apply Shaken (WillCheckOdds caps20%); description still says will and this extra cancellation differs from ordinary accuracy/Dodger. DyingStrike remains active on digestion: inspected branch25% melee damage retaliation to predator, versus tooltip75% claim; ordinary weapon-death branch50%, mostly summons under current downing rules. ArcaneMagistrate gives1Focus on spell hit,5if target dead in inspected damage branch; ordinary-character death bonus largely obsolete with Downed, keep basebenefit vs redefine downing trigger is userdecision. AdeptLearner gives extra permanent+1 to2randomstats perlevel plus unrestricted choices; FocusedDevelopment only unrestricted choices, judgment on growth vs XPtalents.

Tooltip numeric mismatches: FastAbsorption actual1.5x not2x enum claim; HotBlooded/Grounded/ColdTolerance actualincomingelementaldamage*.25 (75% reduction), enum says25% reduction. Fast/Faster and Slow/Slower digestion/absorption variants represent distinct magnitudes, not necessarily duplicates. SpellBlade/ArcaneMagistrate still have obsolete Mind wording in Help override strings; current mechanics use INT. Fearless already has correct Downed description. Healing traits should clearly distinguish temporary combat healing from lasting vore/rest recovery. No new source changes/retirements/tests/build. Recommend resolve few judgments then grouped compilation/manual review before injury phase.


## Grouped compilation and gameplay review — 2026-11-04

Native Unity Play-mode audit passed151 checks (114 retirement checks and37 runtime/label/UI-property checks), exit0: `Logs/traits31-runtime6.log` and `Logs/traits31-runtime6-result.json`. Covers native company battle initialization, Help aliases/retirement gating, private action-spending helpers for standing/moved/second-action/ranged/melee restrictions, Dodger/EasyPrey/Dazzle accuracy calculations, sampled KeenEye outcomes at50% and just above51%, QuickLearner XP, Juggernaut active/skipped/active cycle, actor serialization of eligibility flags, actual spell downing with full Focus bonus, native vore/digestion invoking Vindication, native interior retreat, and outcome font/spacing/divider properties. It does not exercise a human clicking through combat or inspect rendered pixels.

Corrected actual Dazzle compile error (penalty had been placed in generic stat clash rather than GetMagicChance) and Juggernaut opportunity eligibility (future skip counter was disabling active turns). Actual skipped state is now separately serialized. Diagnostic-only compile/access mistakes were corrected. Initial runtime3 passed its checks but crashed in Unity2018 null-graphics TilemapRenderer after reporting; subsequent headless diagnostics disable drawing only and exit normally. Earlier failed logs/results retained, not counted as clean validation.

Final standalone build passed exit0 with no scanned C#/shader errors, exceptions or new compiler warnings: `Logs/traits31-final-build.log`. Current player: `Builds/Warband-Review-20261104-008d6398/Win64/VoreWar.exe`. Previous successful internal build predates Juggernaut correction. All accumulated outcome/trait changes included. Editor diagnostic is excluded from the player assembly; seven existing license/change/dependency notices packaged. Review records: `Logs/traits31-final-source.diff` and `Logs/traits31-result.json`. Source notices/exact CHANGES paths and prior attribution reviewed; no scene/prefab/art/package changes or file moves/deletions. No personal saves used as fixtures; diagnostic saves retained under unique Logs roots with deletion suppression. Existing public-distribution/provenance gates remain open.

One combined visual review remains: outcome hierarchy/readability/divider, Help/roster renamed-trait hovers and right-side Instant Retreat availability. Further balance tuning is not implied by the audit pass. Injuries remain the separate next design phase after user acceptance.
