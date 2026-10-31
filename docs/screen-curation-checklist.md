# Screen and Tool Curation Checklist

**Status:** Curation reference with staged implementation. Packet 02B now blocks/hides the retired tool entries and gates Race Editor; see [its implementation/review record](implementation/02b-tool-retirement-and-race-editor-gate.md). The old component files/assets remain for later dependency retirement. New campaign screens and the help rewrite are still planned work.

## Confirmed screen decisions

| Current screen or tool | Direction |
|---|---|
| Empire Traits | Remove; tied to the retired empire/4X gameplay |
| Cheat Menu | Remove from the conversion |
| Map Editor | Remove; considered cheating and not part of the intended game |
| World Settings | Remove; considered cheating and only relevant to 4X gameplay |
| Bestiary | Remove; not important to the conversion |
| Help Screens | Rewrite for the new gameplay loop as a late task |
| Race Editor | Preserve for the user only, behind their secret developer password. Keep its General Options, Trait Options, and Unit Tags Options tabs available. |
| Randomizer Trait Editor | Remove |
| Custom Trait Editor | Remove |
| Conditional Trait Editor | Remove |
| Assimilable Traits editor | Remove |
| Unit Tag Editor | Keep unit-tag editing available inside Race Editor's Unit Tags Options tab; remove standalone access elsewhere |
| Unit Editor panel | Remove |
| Variable Editor | Remove as a user-accessible editing tool |

The secret password is a convenience gate to keep Race Editor out of normal player flow; the user accepts that it may be discoverable in source code. It is not intended as strong security.

## Implementation notes for removed tools

The current Race Editor opens the Unit Tag Editor from its Unit Tags Options area. Preserve that functionality inside the password-gated Race Editor while removing standalone access elsewhere. The Variable Editor is also used by several legacy settings and trait editors, including the Assimilable Traits control. Remove its user-facing entry points as those old screens are removed; retain only any internal functionality that the surviving Race Editor demonstrably needs.

The Trait system itself remains part of character and species mechanics. Removing these authoring screens does not remove traits or species trait data.

## Pure Tactical setup

The existing tactical-game setup screen can configure attacker and defender separately, including player/AI control, tactical AI, species, vore permission, leader presence, unit count, level, ranged/heavy-weapon/magic proportions, and maximum spell level. It also has map size and auto-scaling, terrain selection, AI-battle handling, and bulk matchup testing across races.

Keep Pure Tactical as a stand-alone single-player skirmish: one battle with no campaign progression, persistent characters, retained stats, or other long-term consequences. The current setup controls have no reason to be removed; keep attacker/defender configuration, AI-versus-AI and bulk matchup testing, and map size/terrain selection. Species selectors must use only globally curated, player-enabled species.

## Related migration notes

- The regular 4X campaign and its empire, army, village, building, and diplomacy screens are retired in favor of the mercenary-company campaign and Pure Tactical.
- Pure Tactical remains a single-battle sandbox, not a way to retain or edit persistent characters.
- The new mercenary campaign gets its own leader creation, company management, town hub, contract list, battle deployment, and results screens. See [mercenary-company-campaign-plan.md](mercenary-company-campaign-plan.md).
