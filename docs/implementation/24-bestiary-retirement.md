# Spec24 — Bestiary retirement, 2026-11-03

User explicitly authorizes moving Assets/Bestiary and its dedicated page/callers into project Recycle Bin. Removed147 scene blocks belonging to Bestiary Screen, its descendants and inactive Bestiary Button, including prefab instances/stripped references; detached their parent child-list entries. No remaining local-ID references to removed scene blocks or GUID consumers of the archived images/controller were found. Only one dedicated C# class existed; no other source callers remain. Shared prefab assets were retained.

Directly moved the remaining folder, folder metadata and controller/metadata into Recycle Bin/Retired Bestiary/Assets with15 original file paths. All post-move hashes match the pre-move manifest. Previous Spec21 archives are unchanged. No deletion or forced Git addition. Exact paths in CHANGES.md; evidence Logs/bestiary24-moves.json and scene/source snapshot Logs/bestiary24-before.json.

The source/content review is not a native Unity scene load, compile or graphical playtest. None requested/run in this packet. Current executable remains9c7ed1a4, predating both credits and Bestiary updates. Include both in the next requested build; no new gameplay workflow needs a separate broad manual tour.

GPL retirement notice added before moving controller, original notices retained; serialized/moved paths recorded in CHANGES.md. No outside code/assets/dependencies. Existing asset-provenance and distribution obligations remain open. Credits changes and all unrelated scene content preserved.
