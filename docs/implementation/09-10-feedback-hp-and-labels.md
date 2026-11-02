# 09–10 feedback — HP persistence and individual names

Implemented 2026-11-01 in response to the reserve/leader HP failures and naming feedback. The contract milestone is still awaiting runtime acceptance; this does not start spec11.

## HP cause and fix

The installed Odin serializer's Unity policy serializes fields and marked auto-properties, but not properties with custom accessors. The new Unit.Health property had custom accessors and its private `health` field was unmarked. Inspecting the actual compiled Unit confirmed that neither was selected. The same omission affected both disk checkpoints and the detached copy used for battle settlement. A reserve member therefore lost HP even though they had never deployed; a revived leader lost HP again on loading.

The backing field is now explicitly serialized, with an alias for the older `Health` field name. The ignored property attribute is removed. Checkpoint preflight/readback and detached battle settlement now compare each canonical character's HP, temporary HP, downed and digested flags before proceeding, rather than verifying only the company/day. A lossy payload is rejected before any checkpoint file is published. No generic full-heal correction is applied.

The user explicitly prioritized future games over repairing existing checkpoints. No personal checkpoints were opened, rewritten, deleted or repaired. Previously omitted HP cannot be reconstructed from that checkpoint; use a fresh company for the focused review.

## Names

- Company battles call the opposing side **Enemy forces**. Difficulty remains visible in contract and deployment views, rather than becoming the turn-log faction name.
- New company enemies use species plus generated focus: **Lizard Warrior**, **Lizard Archer**, **Lizard Mage**, and equivalent Avian/Gnoll labels. Animals use their species, such as **Spitter Slug**. Player leaders/recruits retain their personal names and nicknames.
- A shared display-name layer makes the64retained species singular and spaced. Approved examples are Avian, Lizard, Gnoll and Spitter Slug. The user confirmed **Matron's Minion**, **T. rex** and **Space Roach**; Feral Fox is also spaced correctly.
- Numeric enum values, internal names, species eligibility, preference keys and asset/name-file paths are unchanged. This does not curate additional species. Display-label parsing accepts both the internal enum name and the new label, so skirmish choices and Race Editor mappings still resolve to their original species IDs. Existing “Keep existing mapping” semantics remain intact.

Company creation/roster/recruitment, enemy/deployment lists, content availability, Pure Tactical setup, unit inspection and the developer Race Editor use the display layer. Existing campaign enemies are not retroactively renamed; new campaigns/offers use the archetype names.

## Recovery scope

Company daily HP recovery, rest bonuses, treatment, injuries and sparring remain spec11. The current recovery stage heals surviving contract enemies only. Rest presently advances days, wages and deadlines without healing company members; the reported absence of mercenary recovery is not being presented as fixed here.

## Verification

- Unity2018.3.12f1 compilation passed, return0, no C#errors/exceptions (`Logs/hp-display-compile.log`). Explicit `Assets/base.unity` opening passed, return0 (`Logs/hp-display-scene.log`). The final pre-save guard is covered by the combined handoff compilation/scene log (`Logs/hp-display-handoff-compile-and-scene.log`).
- Direct policy inspection changed from both HP members being omitted to the backing field being selected. Managed serialization with the compiled game assembly and installed Odin library preserved73HP and19temporary HP (`Logs/hp-display-framework-resolved.json`).
- A synthetic company graph using the production Encode and health guard preserved a leader at100HP, reserve at73HP, and digested character at0HP/digested. Deliberately changing restored leader HP to0 was rejected by the guard. All64retained display names were unique and parsed back to their exact numeric IDs (`Logs/hp-display-managed-roundtrip.json`). These checks ran under installed .NETFramework with Unity assembly references, not Play mode or an actual battle/revival sequence. Initial attempts under PowerShell's newer runtime failed on Unity-native logging/legacy emit APIs; the compatible framework diagnostic supersedes them.
- No new test framework or project test sources were added. No player build or Play-mode pass is claimed. The focused player workflow below remains necessary.
- Source notice/diff/resource checks are recorded in `Logs/hp-display-source-review.json`; CHANGES.md covers all13modified C# paths. Existing licensing, legal notices, scene/assets and unrelated user work are preserved. No dependency changes, file deletion/move, commit or publication.

## Focused review

In a **new campaign**, keep a healthy mercenary in reserve, trigger leader digestion with revival enabled, and check the reserve's HP through results and revival. Save after the leader is fully revived, return to the main menu and reload; both HP values should match exactly. Check Enemy forces in the turn log and archetype enemy names, and briefly confirm singular species selections still choose the intended species. No repeated full menu tour or phase11 healing test is requested.

## User acceptance

2026-11-01: The user confirmed the items requiring review appear to work and authorized the next milestone. The focused HP/naming feedback is accepted. Company recovery is implemented separately in [spec11](11-injuries-rest-and-progression.md); it was not part of the earlier HP fix.
