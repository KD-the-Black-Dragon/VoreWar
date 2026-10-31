# Packet 04B1 — central combat text reset

**Status:** Implemented, 2026-10-30; fresh compilation/scene loading passed; the user subsequently confirmed the manual smoke test succeeded and authorized 04B2. The user accepted 04A's checks and authorized 04B. The intentionally empty approved roster remains unchanged.

## Scope

Replace the central legacy prose registry and typed tactical-log generators with minimal functional placeholders, retaining the event data and gameplay registration paths. Remove the two rejected-Humans dummy contexts without substituting another species. This is the first reviewed 04B packet; free-form callback messages and remaining descriptors/synonyms are 04B2.

No new story content, species, events, campaign rules or combat formulas are authored here. No scene, prefab, package or saved personal data is changed. Existing C# paths/metadata remain; no file is deleted or moved.

## Three changed source paths

| Exact source | Change |
|---|---|
| `Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs` | Replace the 2,492 lexical legacy EventString occurrences with one neutral entry per existing category (22) plus an explicit unknown-event fallback. Preserve EventString's fields/constructor, MessageTypes identity and all 22 list-field names. Add a null-safe formatter and highest-priority selection for future user-authored entries. |
| `Assets/Scripts/Modes/Tactical/TacticalMessageLog.cs` | Replace EventDescription and private legacy Generate/GetStoredMessage prose paths with explicit handling for all 46 MessageLogEvent values. Keep functional actor/target names, weapon/spell labels, damage, healing, experience and optional success odds. Remove fabricated defaultPrey and store null for six missing-prey registration fields. |
| `Assets/Scripts/Modes/Tactical/Log/LogUtilities.cs` | PotentialNextPrey returns null when no existing target is found; remove its fabricated Humans Unit fallback. The old callers in the replaced registry/generators are gone. Other helper prose remains pending 04B2. |

The shortened registry preserves upstream narrative contribution credits to **Tatltuae and Flame**, and the upstream acknowledgement of **Ted ArCon** for brainstorming assistance. TacticalMessageLog retains its upstream Tatltuae contribution attribution. These are attribution notices, not a new ownership claim.

## Functional data and formatting

- EventLog and SpellLog field/type shapes and MessageLogEvent numeric identities remain unchanged. EventString and MessageTypes shapes are retained for future narrative authoring.
- Every registration method from RegisterHit through RegisterRegurgitate is unchanged apart from exactly six `Prey = prey` assignments replacing `prey ?? defaultPrey`. Real prey references are preserved; missing context remains missing. No Unit is constructed solely to represent a missing target.
- EventValid, Clear, UpdateListing, RefreshListing and DebugDump method bodies are unchanged. Existing display filters, log clearing/truncation and UI update routes are preserved.
- Typed text is deliberately brief. Damage/HP/experience values come from the same existing event fields; success odds retain the existing positive-value threshold and two-decimal percentage rounding. Zero-damage spell effects say affected, without inventing damage. Defeat wording does not implement the later Downed rules.
- Missing actor/target/prey and an unexpected spell payload have neutral labels rather than another species or unsafe field access. Unknown categories, empty/null candidate lists and no eligible entry have functional fallbacks; unknown stored-message types no longer fall back to the swallow category.
- Legacy/removed categories keep neutral placeholders under their existing keys. This does not claim those underlying mechanics have been retired; that remains their owning combat packet.
- The old SimpleText field is retained for compatibility, but both former verbosity paths now use these neutral messages. Old flavor text cannot be restored by switching the field.
- NewTurn retains its existing functional turn-header payload. **Miscellaneous intentionally still displays its existing Message payload** until caller migration in 04B2. This packet does not claim all old prose is gone.

## Registry authoring and randomness

The implementation choice was stated in commentary before edits: retain the existing shared State.Rand system; do not add a new RNG architecture. A category with a single eligible placeholder is deterministic and needs no random draw. Future multiple highest-priority entries use State.Rand. Highest numeric priority wins; the old special priority-9 random downgrade is no longer part of text selection. All default placeholders have priority zero.

Removing thousands of text selections, synonym/helper calls and dummy-unit creation changes random consumption. Exact same-seed future battle equivalence is not claimed. Combat formulas are unchanged, but different random draws may lead to different outcomes; presentation/gameplay RNG separation remains a separately scoped future improvement.

New author-written predicates and formatters should be pure, null-safe presentation functions. This packet supplies safe defaults and known fallback cases; it does not catch arbitrary exceptions or undo side effects inside future custom C# delegates.

## Remaining coverage: 04B2

The [miscellaneous call-site handoff](04b2-miscellaneous-call-sites.md) records **301 lexical candidate lines** without copying narrative content. It includes combat/equipment/spell/trait/status notifications as well as flavor. Inspect each surrounding block and preserve mechanical statements and useful numerical feedback rather than suppressing every miscellaneous message.

Remaining LogUtilities and PreyLocStrings descriptor/synonym prose, callback-generated strings and any stale literal payloads in unsupported old saves require their own coverage. Help, species descriptions and mechanical tooltips remain separately scoped; do not indiscriminately erase useful rules text. Narrative helpers are no longer used by the central typed formatter, but other callers still need review before retirement.

The two Human narrative dummies are removed now to prepare rejected-species registry work. This does not assert all rejected-species references or legacy generation providers are retired; carry the 03A residual inventory through 03B/05 before curation.

## Validation and limits

Fresh Unity 2018.3.12f1 runtime/editor compilation passed with no C# errors or import exceptions and return code 0 (`Logs/packet-04b1-compile.log`, ignored). Subsequent explicit scene opening with fresh assemblies passed without parse/broken-reference errors or exceptions and returned 0 (`Logs/packet-04b1-scene.log`, ignored). All three sources are older than the fresh runtime assembly. Scene bytes and License.txt content are unchanged; known orphan metadata was restored byte-for-byte. Dated notices and exact change paths are checked. Static correspondence checks performed: all 46 typed enum cases and all 22 registry categories covered; event/entry enum/schema identity retained; every registration body matches the packet start after the six deliberate null-prey substitutions; the five display/filter/clear/debug method bodies match exactly. No direct gameplay-mutator calls were found in the removed formatter/registry bodies. No new tests/framework or player build.

The required compilation, explicit scene-load and source/scope/notice checks above are complete; runtime battle-text coverage remains pending. Original snapshots are in ignored Logs/packet-04b1-<filename>-before.txt plus scene snapshot.

The empty approved roster prevents actual battle-log playtesting; no species is temporarily enabled. Null/empty/unknown behavior is source-reviewed, not claimed as an executed runtime test. After curation, verify typed hit/miss/block/spell/heal/containment/escape/completion messages, filters and old saved-log data as part of integrated battle coverage.

Player review now is only a smoke check: main menu, six Content Settings pages and Pure Tactical's intentional unavailable state should still open/return without new red Console errors. This does not validate battle text. Wait for feedback before 04B2.

## GPL/source-change check

All three changed C# files carry prominent dated 04B1 notices and retain meaningful contribution attribution. Root CHANGES.md identifies each exact path. License.txt and upstream attribution/legal text are preserved; no new third-party code, dependency or asset introduced. No publishing, deletions or moves. This is the source-change workflow check, not a complete distribution or inherited-asset-license audit.

## Subsequent user confirmation

The user reported the manual test succeeded and authorized the callback/descriptor sweep, 04B2. This does not imply executed battle-text coverage with approved species.
