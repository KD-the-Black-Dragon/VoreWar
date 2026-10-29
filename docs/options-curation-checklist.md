# Options and Settings Curation Checklist

**Status:** Discovery and planning. This is an inventory of current setting groups for review; no options, settings, or gameplay have been changed.

## Scope

The current project has a general **Options** screen and a multi-page **Content Settings** screen. The Content Settings page names below are taken from the current Unity scene. Individual controls are numerous, so this first pass groups them by purpose. A detailed control-by-control list can be made for any page the user wants to review.

The user wants Content Settings to remain global across the mercenary-company campaign and Pure Tactical. Global scope does not mean every legacy setting must remain; individual entries can be retained, revised, or removed as the conversion is curated.

## General Options

Current controls in `Assets/Scripts/Subwindows/Options.cs` fall into these groups:

- **Simulation and animation pace:** strategic movement delay; player, friendly-AI, and enemy-AI tactical movement delays; attack and vore animation delays; automatic advancement; watching AI battles and their reports.
- **Turn and battle flow:** ignore monster battles; stop after battles; prompt before ending a turn; scroll the camera to a battle; notifications; strategic and tactical camera-follow behavior.
- **Battle and unit display:** damage numbers; extra tactical information; level labels; alternate friendly colors; banner scale; show or hide the unit viewer and base stats.
- **Map and interaction display:** simplified farm and forest visuals; right-click menu; desaturated tiles; edge scrolling; alliance-square shading; hard-lava behavior.
- **Audio:** sound enable; combat, vore, and passive-vore volume; close digestion noises.
- **Other behavior:** automatic use of AI; continue running when the app is unfocused.
- **User-data tools:** clear saved Options and Content Settings; reset or reload namelists; delete the Userdata folder and its contents. These are maintenance actions rather than gameplay preferences.

Some controls clearly depend on retired empire-scale gameplay; others affect tactical combat, presentation, or audio and may still be useful in both modes.

## Content Settings pages

The current page selector contains these twelve groups:

| Current page | Broad subject suggested by its name and controls |
|---|---|
| Strategic Gameplay | Strategic movement, army/scout limits, turn events, diplomacy, and other empire-campaign rules — **remove this page in full** |
| Tactical Gameplay | Tactical combat rules and battle behavior — keep only the cosmetic **Surrender Flags** and **Show Unit Sides** options player-changeable; keep balance values adjustable in code |
| Tactical Misc | Additional tactical rules, terrain, and combat adjustments — remove the page from player settings; keep balance values adjustable in code |
| Merc / Monster Spawning | Species availability and legacy strategic mercenary/monster spawning — **replace with a simpler curated-species availability page** |
| Gender Settings | Character gender proportions and related generation preferences — **keep as-is; cosmetic** |
| Appearance Settings | Clothing, body size, hair, and visual-generation preferences — **keep as-is; cosmetic** |
| Fog of War settings | Fog, sight, and reveal behavior — **likely remove the legacy settings page; campaign exploration fog will be mandatory and tied to discovering towns** |
| Vore Main | Vore types and their availability or weighting — remove the specific player-facing controls listed below; leave unlisted controls at their current defaults for now |
| Vore Advanced | Digestion, absorption, escape, and other detailed vore rules — remove the player-facing controls, but retain code-level balance tuning |
| Vore Misc | Additional vore interactions, effects, and presentation settings — remove the player-facing page and retain the current desired defaults |
| Post-Vore | Outcomes and effects after vore or transfer — remove the specific controls listed below; leave unlisted controls at their current defaults for now |
| Building Settings | Empire-era building behavior — **remove this page in full** |

The user has decided to remove the Strategic Gameplay, Building Settings, and Tactical Misc pages in full, and replace Merc / Monster Spawning with the species-availability page below. Gender Settings and Appearance Settings are cosmetic and can stay as-is. The legacy Fog of War settings page will probably be removed; campaign exploration fog is mandatory and reveals towns as the company explores. Vore Main and Post-Vore are receiving selective cleanup. Vore Advanced and Vore Misc should disappear from player settings, with Vore Advanced balance values retained in code and Vore Misc using the current desired defaults. Tactical Gameplay should expose only its two cosmetic options; its balance controls remain adjustable in code.

## Curated-species availability page

Replace the legacy Merc / Monster Spawning page with a simpler global setting: **which curated species are allowed to appear in the game?**

- Use the same global species availability in the mercenary campaign and Pure Tactical.
- List only species that have already been curated and added to the game.
- Let the player disable any listed species so it will not appear in the game.
- Do not let players enable species that have not been curated and added.

## Vore Main controls marked for removal

Remove these controls from the player-facing settings page. This is menu cleanup; it does not by itself remove or change the underlying vore mechanics.

- Breast Vore
- Bladder Vore
- Vore odds
- Escape odds
- Disorient Escaped Prey
- Breast Vore Type
- Hard vore text enabled
- Edible corpses
- Surrendered Pred escape mult
- Surrendered Pred Auto Regurgitation

## Post-Vore controls marked for removal

Remove these controls from the player-facing settings page. This records menu cleanup only; it does not by itself remove or alter the underlying game mechanics.

- Fart on absorb
- Fart %
- Clean Disposal
- Bird Scat
- Scat
- Scat V2
- Bones allowed in scat
- Weight gain enabled
- Weight Gain %
- Race Size limits weight gain
- Oral alt. weight gains
- Weight loss enabled
- The three weight-loss chance-per-turn sliders: Breasts, Body, and Dick

## Other setup screens

The old strategic-game setup is replaced by mercenary campaign creation. Pure Tactical keeps its current separate battle setup, subject to the shared curated-species availability setting.

## Decisions

Record further entries here as the user identifies them:

- **Remove player-facing pages:** Strategic Gameplay; Building Settings; Tactical Misc; Vore Misc.
- **Remove player-facing controls but retain code-level tuning:** Vore Advanced; balance controls on Tactical Gameplay.
- **Keep as-is:** Gender Settings; Appearance Settings.
- **Likely remove:** legacy Fog of War settings page; replace its campaign role with mandatory exploration fog and town discovery.
- **Keep at current defaults for now:** Vore Main and Post-Vore controls not listed for removal above. The user considers the defaults generally conservative and not extreme or unbalanced.
- **Remove from Vore Main:** Breast Vore; Bladder Vore; Vore odds; Escape odds; Disorient Escaped Prey; Breast Vore Type; Hard vore text enabled; Edible corpses; Surrendered Pred escape mult; Surrendered Pred Auto Regurgitation.
- **Remove from Post-Vore:** Fart on absorb; Fart %; Clean Disposal; Bird Scat; Scat; Scat V2; Bones allowed in scat; Weight gain enabled; Weight Gain %; Race Size limits weight gain; Oral alt. weight gains; Weight loss enabled; the three weight-loss chance-per-turn sliders.
- **Replace:** Merc / Monster Spawning page with the global curated-species availability page above.
- **Keep globally:** General Options screen as it currently stands; Surrender Flags and Show Unit Sides on Tactical Gameplay; non-gameplay-impacting controls on Vore Main.
- **Keep but revise for the mercenary campaign:**
- **Review individually:**
