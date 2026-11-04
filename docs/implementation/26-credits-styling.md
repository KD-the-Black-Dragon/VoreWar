# Spec26 — Credits styling, 2026-11-03

User requests light-touch color unification and modestly smaller text. Existing credits-only CreditsLink component now styles its containing Credits Canvas when enabled, using UITheme Background/Panel/Control/Row/Accent/Foreground. Exit button and scrollbar receive shared selectable colors; existing geometry/scrolling/wording/link behavior retained. Body and Exit label24pt, rendered Warband header30/upstream header26. No new screen design or content edits. Source scene has a separate Credits Canvas; main-menu/other screen hierarchies are outside this styling scope.

Source/hierarchy/diff review only. No Unity compile/tests/player build or graphical playtest requested or run. Include with pending Spec23 credits, Spec24 Bestiary and Spec25 naming-data cleanup in next requested build. One focused review of readable dark-blue credits/scrolling/Exit is sufficient; do not repeat broad accepted workflow testing.

Exact source Assets/Scripts/UI/CreditsLink.cs carries retained fork-addition notice plus dated modification notice; CHANGES records scope. No new source/assets/dependencies. Existing attribution/legal/provenance gates unchanged. No file moves/deletion or unrelated scene changes.

## Requested build completed — 2026-11-03

Fresh player: Builds/Warband-Review-20261103-e52dc685/Win64/VoreWar.exe. Includes pending Specs23–26. Clean native build exit0, no scanned C# errors/exceptions/missing scripts; seven legal/change/dependency notices packaged. Streaming credits/name-list hashes match current sources. Initial stale Bestiary compiler-list refresh superseded by final clean build log. No actual graphical playtest or repeat save audit. Evidence Logs/credits26-result.json and credits26-clean-build.log.

