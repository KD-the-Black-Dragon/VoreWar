# 05A3B17 — consolidated approved ability-retirement completion

**Date:** 2026-11-01. User accepted 05A3B16 and replaced the tiny-packet cadence with one combined ability-retirement stage and one manual test. No intermediate user tests requested. This completes that approved revival/allegiance/Leadership pass; it is not completion of all strategic retirement or the new health/fate design.

## Implemented boundary

- **Enums/Tags.cs:** TraitsMethods.IsRetired is a pure 31-ID switch covering the previously excluded revival/replacement, metamorphosis, possession/corruption, Friendosoma/TurnCoat/support conversion and charm/reanimation/Bind trait families. It references existing enum names only; no IDs/order changed. It avoids adding a TraitList initialization dependency to every HasTrait call.
- **Races/Traits.cs:** GetTrait returns null for retired IDs, suppressing their callback/boost interfaces, including Possession/SpiritPossession and any shared/conditional reactivation. Concrete types/registry entries remain compatibility data. Default callback flow continues; the excluded abilities do not acquire replacement mechanics.
- **Entities/Units/Unit.cs:** HasTrait/HasSharedTrait reject retired IDs, and all GetTraits branches and active base-trait counts filter them. Both random trait pools exclude them. Raw Tags/PermanentTraits/shared/conditional data are preserved. CanBeConverted returns false. Controller ignores stored possession control. Charmed/Hypnotized/Respawns cannot be applied or observed through the standard status accessor. GetLeaderBonus returns zero and automatic Leadership growth is removed.
- **Entities/Units/Actor_Unit.cs:** AddCorruption/AddPossession/RemovePossession/CheckPossession become inert compatibility entry points; no direct ownership/stat-threshold bypass remains there.
- **Entities/Units/Prey.cs:** ChangeSide is inert; it cannot mutate fixed side, reset surrendered state or propagate corruption.
- **Modes/TacticalMode.cs:** SwitchAlignment is inert. Remove Friendosoma's special conversion/retreat branch and retain the pre-existing ordinary Endosoma result fallback. The fallback still belongs to old combat outcome handling; the agreed new Downed/digestion/stalemate system remains future work. AttackerConvert/DefenderConvert are retained for initial actor/summon roster assignment, not spontaneous conversion.
- **Modes/Strategic/StrategicUtilities.cs:** remove Leadership aura/leader stat power terms and ensure AI level-selection weight stays zero even with custom weights. No replacement leader stat bonus invented.
- **Modes/Recruit_Mode.cs:** legacy empire-leader revival and soul-imprint creation no longer mutate/pay/clone; they report retirement if invoked. The retired strategic UI is not repurposed as company revival.
- **AI/StrategicArmyCommander.cs:** old AI empire-leader resurrection returns null without cost or mutation.
- **Utility/Texts/HoveringTooltip.cs:** retired traits describe themselves as retired rather than promising their previous mechanics.
- **Entities/Items/ItemRepository.cs:** random book generation preserves existing tier limits while choosing only active spell books; a genuinely empty future tier range reports an explicit error instead of silently choosing a retired book.

All exact paths are in CHANGES.md and the ignored source-edit evidence. Existing IDs, raw saved definitions, fields, item ordering, assets, scenes and prefabs remain. The unused legacy implementations/metadata may remain in source: permanent deprecation is intentional and was authorized. They are not active abilities merely because their names persist.

## Preserved scope

Keep ordinary attacks and vore/digestion, other spells, fear/pain surrender, temporary disguises, approved animal/clone summons and unrelated traits. Temporary disguise's apparent-side handling remains distinct from persistent allegiance conversion. Assimilate/InfiniteAssimilation/AdaptiveBiology runtime is preserved: the architecture authorizes removing its old authoring tool, not every related gameplay trait. Potions, training, item slots, injuries/temporary HP and company leader-specific revival are separate conversion work. Leadership's stored stat slot remains for compatibility, but its aura/growth/power contribution is retired. New leader toughness is later tuning.

No claim that raw source definitions or all 4X providers are erased. This stage closes the current approved ability activation paths and avoids leaving a future micro-packet for each trait. Next work returns to the consolidated strategic/provider dependency review and species read boundaries.

## Verification

Unity 2018.3.12f1 completion compile PASSED exit 0 without C# errors/import exceptions: Logs/packet-05a3b17-compile-complete.log. Earlier initial/final logs are retained; the combined warning set has no new warnings versus 05A3B16 after line normalization. Explicit base.unity opening PASSED exit 0 without parse errors/exceptions: Logs/packet-05a3b17-scene.log. Scene bytes unchanged; all 11 notices and source correspondences verified. Scoped diff check passed and no Unity process remained. Exact source reconstruction passed for all 11 sources against packet-start snapshots. The retired policy preserves enum declarations. Source inventory verified all ten normal min/max book-tier ranges retain an active candidate. Logs/packet-05a3b17-source-review.json and verification.json hold exact edits/checks; source/scene snapshots and diffs are ignored artifacts. No new tests/framework, player build or agent Play-mode battle; no personal-data access. Actual battle-level checks remain blocked by the intentionally empty species roster and must be exercised after curation.

## GPL and scope check

License.txt sections 4/5 reviewed; content hash matches HEAD. All 11 changed sources have actual 2026-11-01 conversion notices, earlier notices/attribution preserved; CHANGES.md lists exact paths. No new third-party code/dependency/asset or file deletion/move. AGENTS.md's cadence update is documentation only. This per-change review does not replace a pre-distribution license/source-delivery review.

## One combined manual check

After agent compile/scene verification: open main menu, enter/leave Pure Tactical setup, then confirm Content Settings and password-gated Race Editor still open. No resets or sample species needed. Where retired trait metadata remains visible in developer controls, its tooltip should state retirement; equipping/storing its ID does not activate it. Combat tests await species curation.

User confirmed all combined screen checks passed without new red errors (2026-11-01). The consolidated ability-retirement stage is accepted; return to grouped campaign/provider cleanup rather than scheduling more tiny ability packets. Battle regressions remain pending curation.
