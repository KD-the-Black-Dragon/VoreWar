# Unit Tags and Untagged Traits investigation — 2026-11-03

Investigation only. No source, scene, assets, personal settings or saves changed; no Unity/build/playtest invoked. Source/data/scene wiring and existing runtime log inspected.

## Two separate systems

**Tagged traits / Untagged warning:** `ExternalTraitHandler.TaggedTraitParser` and `TaggedTraitUpdater` load `taggedTraits.json` containing rarity tiers and searchable category strings. `State` builds `UntaggedTraits` for missing/empty metadata, and `StartMode.RunTaggedTraitCheck` shows the main-menu alert if any exist. The alert now routes to password-protected Race Editor. This is a developer trait-catalog warning, not a report of missing Unit Tags or missing sprite assets. `TaggedTraitEditor` uses categories/tiers for searching/selection; `TaggedTraitUtilities` can select random traits by rarity. Base trait effects are implemented separately.

**Unit Tags:** `UnitTag` is a named ID with modifiers and associated traits. Species receive tag IDs via Race Editor; traits can confer tags as well. `TagConditionChecker` gathers these and applies conditional multipliers in melee/ranged/magic damage, hit modifiers, critical/graze behavior, vore/escape odds and digestion-related calculations. Example possible configuration: a tag increases ranged damage against targets carrying a ranged weapon. This is a developer balance/modding rules system, not decorative labels or Unity GameObject tags.

## Confirmed warning regression

`Assets/StreamingAssets/taggedTraits.json` retains TheMatron, QueenOfFrost and DestroyingAngel, removed from the trait enum in Spec21. Both parser/updater wrap the entire iteration in one catch; encountering the first missing name aborts the remaining rows instead of skipping that row. The current shipped file contains category rows for all surviving enum traits; the incomplete load creates apparent missing metadata. The updater does not actually remove the bad row as the old parser message claims. Existing Logs/retire21-runtime.log explicitly logs TheMatron parse/update errors, despite the independent synthetic save assertions passing.

Correction to prior verification report: compilation/build and all34 save assertions passed, but the earlier broad no-errors summary missed these caught metadata errors. This is a known unresolved Spec21 defect, not evidence the category loader passed. Do not treat successful compilation as validation of that subsystem.

## Why Unit Tags can appear inoperable

- No UnitTag JSON definitions are shipped in Assets/StreamingAssets or the current review player's StreamingAssets. `State.UnitTagList` starts empty and UnitTagParser only reads local UnitTags/*.json; there are no built-in definitions. RaceEditor's dropdown therefore has nothing to assign on a fresh setup. Existing user-created definitions, if any, were not inspected.
- Scene wiring is present: Unit Tags Options calls RaceEditorPanel.ActivateTags; Tag Editor calls ModifyUnitTags, then password-gated GameMenu.OpenUnitTags and UnitTagEditor.Open. The editor is intended to allow Add, then Modify, configure and Save. No runtime click/visual reproduction performed; absence of shipped definitions does not prove every reported click symptom.
- Existing bugs: RaceEditorPanel.AddTag uses dropdown index as persistent ID (fails when IDs have gaps); ALL/remove paths index an empty dropdown without a guard. UnitTagSaver writes a leading-space filename while UnitTagRemover looks for an unspaced filename. UnitTagParser suppresses exceptions and can silently leave a partial list. TagConditionChecker's historical empire/mercenary/monster range checks also need review before relying on those conditions. These are separate from the trait metadata warning and are not fixed by this investigation.

## Recommended scope, not implemented

Keep Unit Tags developer-only as requested for Race Editor. Correct removed-name handling in trait-category imports (including old local files), remove the three obsolete shipped metadata rows and move/hide the developer warning from the normal main menu. If Unit Tags is to be used for balancing, repair its empty-list/ID/save handling in the same developer-tool packet; do not remove combat tag support or invent new tags without user direction. No additional player workflows or UI overhaul required.

## Selicia artwork decision recorded

User confirms the three dedicated image candidates are safe to archive. Existing Recycle Bin moves stand; no further move/deletion needed. Broad sprite sheets containing historical silhouette gag frames remain unchanged; no trimming, blacking out or pixel removal requested. Selicia identity/rendering gates remain removed. The user's practical acceptance does not itself change inherited artwork attribution/terms or close existing release-provenance gates.
## Implemented following user authorization

The metadata and retained developer-tool corrections are now implemented and built. See [Spec22 correction report](22-unit-tags-correction.md). This supersedes the investigation-only status above; no graphical playtest is claimed.

