# 34B — Developer battle and recovery lab

2026-11-04. Slice2 only. Native scene/gameplay checks completed; final build/packaging results recorded below when complete. Slice3 power scoring and later work are not started.

## Entry and isolation

Main-menu Battle Lab button sits beside Race Editor in a paired centered developer footer. Both use the same existing214214214 convenience-password gate. Cancel/wrong password cannot open the lab; closing relocks access. No source-discoverability/security guarantee is introduced.

The lab creates an independent, two-combatant Single Battle with both sides under explicit developer control. Normal GameManager scene input/turn/AI progression pauses while the lab is open. UI EventSystem input continues. The lab never installs a company battle/save session. World and RNG are temporary; WorldConfig is cloned. On close, the exact prior world/configuration/random-generator references are restored, including the previous random stream. No campaign inventory, wages, clock or personal saves are edited. Minimal battle-world construction avoids the full-world constructor's unrelated global settings changes.

## Controls

- Combatants: approved/enabled species, explicit level/base stats, lasting/temporary HP, mana, full resources/downing and paper doll. Level is an explicit override, not automatic stat-growth simulation. Resource overrides intentionally bypass healing ceilings; ordinary healing actions respect them.
- Traits: active retained traits including baseline tiers, add/upgrade, remove, clear. Existing family exclusivity applies. No retired traits are exposed.
- Equipment: all nine company slots and compatible catalog items, with Empty. Created items belong to this fixture only; no campaign stock or gold is consumed.
- Injuries/recovery: choose any body part and Bruised/Battered/Mangled severity, add/upgrade old body or systemic injuries, remove individual injuries or clear them, treat and step ordinary/camp/town recovery. Production injury/treatment/recovery formulas are reused, including hidden recovery rolls and daily complication risks. Current conditions and healing ceiling are visible. Treatment in this developer bench is free; production town validation/pricing remains unchanged.
- Actions: native melee/ranged at controlled distances, retained damaging spells, combat-round/AP stepping, temporary/lasting healing, temporary-HP expiration, vore/regurgitation/digestion and a scrollable full battle log. Rolls, damage and injuries are real game mechanics. The round counter advances only on the explicit Next combat turn command; no autonomous battle completion or AI simulation runs behind the controls.

Seeded New scenario resets generate repeatable combatants. Store fixture / Restore fixture bookmark unit configurations/resources/injuries and the RNG in memory. No fixture files or new save format are introduced. This is a fresh-round configuration bookmark, not a full mid-action/contained-prey tactical save. Release swallowed units and reset digested units before storing a fixture. Closing discards the bookmark. Ordinary Single Battle/campaign saving remains unchanged.

## Validation

The existing isolated native audit checks the actual gate (cancel/wrong/correct), main-menu entry, two native actors, all control pages, native melee/ranged/spell damage, explicit round advancement, full-log viewport, HP/injury/hidden-roll fixture restoration, treatment/recovery, repeated-seed stats, no synthetic save-file writes and exact world/config/RNG restoration/relock. It also retains prior499 regressions. Initial compile found a local Input-helper name shadowing UnityEngine.Input; corrected. The first diagnostic compile needed reflection for the internal ActiveInput field; corrected. Earlier logs remain, not reported as clean passes. Native runtime4 is the final gameplay result; a subsequent label-only correction keeps Test A/B names after species replacement.

No rendered visual acceptance or personal-save test is claimed. User review: open via main menu and password, configure one injury and store/restore it, try an action/recovery day, then close. Inspect readability/dropdowns/portraits in the user's normal window. No new narrative or later-slice mechanics were added.

## Source/licensing

New fork source DeveloperBattleLab.cs and companion .meta; modified StartMode, GameMenu, GameManager, Unit, CompanyInjuries and existing TraitCurationAudit. Per-file dated notices and exact paths in CHANGES identify this work, preserving previous legal/attribution notices. Existing Unity/Odin APIs only; no new assets/dependencies or source from outside the fork. No agent file deletion/moving. Existing public-distribution/provenance gates remain open.


## Handoff — 2026-11-04

Final native runtime4 passed524 (499prior+25lab) checks, actualexit0. Native melee/ranged/spell actions, round stepping, full-log viewport and all five control pages were exercised; passwordcancel/wrong/correct, fixture hidden-roll restoration, no save writes and exact global state restoration/relocking passed. Fresh Warband-Review-20261104-4d064098/Win64/VoreWar.exe compiled exit0. Compiler warning messages unchanged. Final post-audit change only keeps Test A/B log names after species replacement and is compiled in the build. Visual appearance/input readability remains for the user, not inferred from property checks. Evidence Logs/lab34b-runtime4-result.json, lab34b-runtime4.log, lab34b-build.log, lab34b-result.json. Seven existing notices retained and packaged; source notices/changelog reviewed. Stop pending feedback; slice3 not started.
