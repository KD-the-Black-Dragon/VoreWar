# StreamingAssets text-file audit — 2026-11-03

User requested inspection for naming files associated with retired species. Read-only source/data review of123 root .txt files; no source/content edits, moves, Unity invocation or tests. Intentional empty-folder deletions by the user are accepted, not treated as corruption or restored.

## Clear archive candidates

- Assets/StreamingAssets/femaleCentaurs.txt and .meta
- Assets/StreamingAssets/maleCentaurs.txt and .meta
- Assets/StreamingAssets/GhostVillageNames.txt and .meta

Centaur and Ghosts are permanently retired. These filenames have no current source readers and are absent from State.nameTextFileNames; the species loop cannot select them. They can be moved directly to a new project Recycle Bin batch after authorization. No blanket move of the directory recommended.

## Preserve retained candidates and shared files

Bunnies was not permanently retired. maleBunnies/femaleBunnies remain seeded and read for Race.Bunnies, an uncurated future candidate. FeralSlime146 remains a distinct animal species; Slimes was the removed species. Keep its artwork/name data pending curation. Similarly FeralUmbreon is retained separately from the removed sapient Umbreon identity, consistent with the user's earlier credits clarification.

FeralSlimes.txt is seeded but the reader expects FeralSlime.txt; both files exist. femaleUtahraptors.txt is an unused plural alias alongside the live femaleUtahraptor.txt. These are spelling/seed mismatches for retained species, not evidence of species retirement. No removal inferred.

Credits.txt intentionally retains historical contributor credits; preserve the accepted Spec23 edits. customTraits.txt is empty shared legacy input; events.txt is a previously retired placeholder. males/females/monsters are generic name lists, not species catalogs. note.txt explains default copies into UserData. None selected for retirement on a word match.

## Shared armyNames.txt

22 obsolete species headings remain: Aabayx, Alraune, Bears, Cats, Crux, Crypters, Dogs, Driders, Foxes, Ghosts, Harpies, Humans, Imps, Lamia, MainlandElves, Merfolk, Scylla, Slimes, Taurus, Umbreon, Wolves, Yordles. Retained species sections are mixed in, so preserve the file and prune only the obsolete sections if cleanup is authorized.

Additional source concern: NameGenerator treats only currently parseable enum names as section headers. A removed header fails parsing; if a previous live section was selected, that removed section's lines can be appended to its army-name list. This cannot recreate a removed species, but old sections are not necessarily ignored cleanly. A narrow follow-up should prune obsolete default sections and make section boundaries safe for existing local files, without deleting or editing the user's personal data. The company campaign itself uses one company instead of the legacy many-army system.

## Recommended next packet, not implemented

Archive the three clearly obsolete text/meta pairs. Trim only retired blocks from shared armyNames.txt, preserve generic/future species. Optionally fix the FeralSlime seed mismatch while retaining both candidate files until naming-pool intent is reviewed. No source/assets moved or edited by this investigation; no GPL-source change claimed. Existing provenance/distribution gates unchanged.

## Authorized cleanup completed — 2026-11-03

User explicitly authorized archive of the three named obsolete text files and metadata, and trimming retired sections from shared armyNames. Six paths moved directly to Recycle Bin/Retired Name Lists/Assets/StreamingAssets with hashes verified. Exactly22 retired paragraphs removed from armyNames; every retained paragraph/separator unchanged, including Bunnies and historical DemiDragons. No cleanup of retained FeralSlime/Utahraptor aliases, C# parser or personal UserData was included. Earlier recommendations above remain historical, not additional authorization.

CHANGES.md records exact source/destination paths and associated data modification notice. No comments inserted into the name-file format. No filesystem deletion, new code/dependencies, Unity invocation/tests/build or manual playtest. Pending credits/Bestiary/these default names await the next requested player build. Evidence: Logs/names25-moves.json, names25-trim.json, names25-army-before.txt.
