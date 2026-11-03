# Spec22 — Trait metadata and developer Unit Tags correction

2026-11-03. Implements the user-approved correction and packages the pending Race Editor hover fix.

Unknown trait names are skipped per row, including old local metadata. Valid local entries take priority and shipped metadata fills missing entries. Exactly three obsolete OC rows removed from shipped taggedTraits.json; remaining entries preserved. Invalid rows/files warn without suppressing later valid data. Local metadata backup retained and output overwritten without a preliminary deletion.

Unit Tags stays inside password-protected Race Editor. No default tags or new player balance options. Empty dropdowns are safe; species assignments and HasTag conditions use persistent IDs, existing condition choices restore at the correct indices, dropdowns refresh after editing and missing tag definitions do not crash the list. Existing tag source filenames are remembered, including legacy leading spaces; Save overwrites that same file even after renaming, while new files receive an ID suffix. The existing user-triggered tag removal behavior is retained; it was not invoked during checks. Generic conditional category comparisons and absent-actor guards corrected. This is not a redesign of every legacy developer option.

The pending hover fix maps singular/spaced labels through SpeciesPolicy.TryParseName. No art, sprite sheets, retired species or bundles restored.

## Verification

- Scoped diff/source and JSON review: only the three obsolete metadata rows removed; valid remaining rows unchanged and every surviving trait has a shipped entry.
- Unity runtime check exit0: startup/import completes without the previous TheMatron/retired-name metadata errors; all34 existing synthetic save/battle assertions pass. Logs-only synthetic data, deletion intercepted, no personal-save or user-settings inspection. An initial compiler-detected local-variable conflict was corrected; only tags22-clean-runtime.log counts as the clean run.
- Windows player build and packaged notice status recorded in Logs/tags22-result.json once completed. No new test framework or test sources added. No graphical manual playtest claimed.

## Combined manual review

1. Main menu has no Untagged Traits warning. Unlock Race Editor and hover singular/spaced species labels such as Avian, Lizard and Spitter Slug; previews should appear.
2. Unit Tags Options may start empty. Open its tag editor, Add a test definition, Modify and Save; return to the species tag page, select/assign it and confirm that reopening the game keeps the saved definition. Use a test configuration; editing developer tags deliberately changes balance. No broad repeat of previously accepted campaign screens required.

## Licensing/scope

Seven modified C# paths listed in CHANGES.md have 2026-11-03 notices, inherited attribution preserved. Exact JSON path and changes recorded there. No outside dependencies/code/art introduced; applicable license/dependency notices accompany the new private build. Existing provenance/distribution gates remain open. All previous builds/artwork/archive contents preserved; no agent filesystem deletion.
