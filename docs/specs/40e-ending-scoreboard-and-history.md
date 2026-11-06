# 40E — Ending scoreboard and durable company history

Planning draft, 2026-11-06. Part of [module 40](40-authoring-and-campaign-progression.md).

## Existing records

`Assets/Scripts/Subwindows/CompanyMissionsUI.cs` already shows active/ended company records and a memorial. The approved statistics include reputation, total gold earned, hired mercenaries, actual losses, completed contracts, days survived and enemies defeated. Dismissals and quitters are excluded from losses. Extend these established records into a clear ending summary and persistent history.

## Workflow and data

1. Show the final leader/company identity, end reason and approved statistics after a terminal outcome. Keep the fallen-mercenary memorial accessible.
2. Store a compact immutable final record independently of resumable saves. Include campaign ID, record schema, final day, ending category, Ironman status, official rules revision and developer-override status. Avoid copying the entire save or portrait asset data.
3. Add a history viewer using the established UI style. Selecting a row shows that company's final summary. No narrative writing is required.
4. Define ordering and optional numeric scoring only after user approval. The existing recorded metrics can support an initial unranked history with sorting, but this is a proposal.

## Terminal transaction and recovery

Trace every end path, including `CompanyBattle.Complete`, immediate leader-digestion loss and upkeep insolvency. Persist the final record before Ironman invalidation/removal in `CompanySaveSlots.RemoveCompany`. Archive failure must have a recoverable state and actionable message. Reopening results or retrying cleanup cannot duplicate a terminal record.

Define the identity of divergent non-Ironman endings from multiple saves. Campaign ID alone can deduplicate a retry, but cannot distinguish a genuinely different branch. Decide whether history keeps the latest ending, the first ending or distinct terminal records under that campaign before implementing its key.

History loading must tolerate a malformed record without losing other entries. Preserve actual unknown fields/version limits deliberately. A history record is not resumable campaign state. Ironman recovery backups remain subject to the existing game-owned save-removal rules. Agent checks suppress deletion and retain all synthetic files.

## Decisions before implementation

- Ranked score formula or sortable metrics without an overall score.
- Non-Ironman branching/repeated-ending policy.
- Whether abandoned campaigns can be explicitly retired to history, and how that differs from defeat.
- Whether older ended saves can create a history entry when opened. Do not invent missing past campaigns or unrecorded accomplishments.

## Planned grouped acceptance

Exercise every terminal path, result reload, duplicate retry, archive failure, save cleanup failure and corrupt history entries. Verify Ironman history survives save removal, personal save files remain untouched by checks, and developer runs are labeled. One player handoff covers ending presentation and browsing multiple synthetic histories.
