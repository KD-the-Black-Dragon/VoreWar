# Legacy 4X event retirement inventory

**Packet:** 01B; 2026-10-30. All listed content is designated for retirement, not adaptation by changing its prose.

**Subsequent implementation:** [04A](../implementation/04a-legacy-event-retirement.md) retires the catalogs, custom parser/payload, triggers and old panel callbacks recorded below. This is a historical pre-retirement inventory; its original source line anchors do not describe the current shortened files.

Per-case flags are lexical review hints, not a full semantic description of every choice. Choice counts include listener registrations in the case, not necessarily concurrently displayed choices. Source locations preserve the complete definitions/effects for the retirement review without reproducing narrative. Retirement must also cover custom-file loading, player/AI triggers, state history, and EventScreen close behavior.

## Player-choice events

| Case | Source | Choice registrations | Effect markers present |
|---:|---|---:|---|
| 0 | [EventList.cs:243](../../Assets/Scripts/Utility/EventList.cs#L243) | 3 | gold, happiness, rebels, experience, traits/status |
| 1 | [EventList.cs:284](../../Assets/Scripts/Utility/EventList.cs#L284) | 3 | gold, happiness, rebels, experience, traits/status |
| 2 | [EventList.cs:342](../../Assets/Scripts/Utility/EventList.cs#L342) | 3 | population, happiness, relations, experience, traits/status |
| 3 | [EventList.cs:414](../../Assets/Scripts/Utility/EventList.cs#L414) | 3 | population, happiness, rebels |
| 4 | [EventList.cs:474](../../Assets/Scripts/Utility/EventList.cs#L474) | 3 | population, happiness |
| 5 | [EventList.cs:540](../../Assets/Scripts/Utility/EventList.cs#L540) | 3 | gold, happiness, experience, traits/status |
| 6 | [EventList.cs:578](../../Assets/Scripts/Utility/EventList.cs#L578) | 3 | population, relations, units |
| 7 | [EventList.cs:627](../../Assets/Scripts/Utility/EventList.cs#L627) | 3 | gold, experience, traits/status |
| 8 | [EventList.cs:700](../../Assets/Scripts/Utility/EventList.cs#L700) | 3 | gold, relations |
| 9 | [EventList.cs:761](../../Assets/Scripts/Utility/EventList.cs#L761) | 3 | population, units, traits/status |
| 10 | [EventList.cs:798](../../Assets/Scripts/Utility/EventList.cs#L798) | 3 | population, happiness, units |
| 11 | [EventList.cs:841](../../Assets/Scripts/Utility/EventList.cs#L841) | 3 | population, happiness, units, traits/status |
| 12 | [EventList.cs:882](../../Assets/Scripts/Utility/EventList.cs#L882) | 3 | happiness, relations |
| 13 | [EventList.cs:918](../../Assets/Scripts/Utility/EventList.cs#L918) | 3 | population, units, traits/status |
| 14 | [EventList.cs:965](../../Assets/Scripts/Utility/EventList.cs#L965) | 3 | happiness, experience |
| 15 | [EventList.cs:1002](../../Assets/Scripts/Utility/EventList.cs#L1002) | 3 | population, happiness, units |
| 16 | [EventList.cs:1054](../../Assets/Scripts/Utility/EventList.cs#L1054) | 3 | relations, traits/status |
| 17 | [EventList.cs:1103](../../Assets/Scripts/Utility/EventList.cs#L1103) | 3 | relations |
| 18 | [EventList.cs:1146](../../Assets/Scripts/Utility/EventList.cs#L1146) | 3 | population, happiness, units, traits/status |
| 19 | [EventList.cs:1190](../../Assets/Scripts/Utility/EventList.cs#L1190) | 3 | relations |
| 20 | [EventList.cs:1242](../../Assets/Scripts/Utility/EventList.cs#L1242) | 6 | population, happiness, units, traits/status |
| 21 | [EventList.cs:1308](../../Assets/Scripts/Utility/EventList.cs#L1308) | 3 | population, relations, units, traits/status |
| 22 | [EventList.cs:1342](../../Assets/Scripts/Utility/EventList.cs#L1342) | 3 | gold, relations |
| 23 | [EventList.cs:1393](../../Assets/Scripts/Utility/EventList.cs#L1393) | 3 | relations |
| 24 | [EventList.cs:1443](../../Assets/Scripts/Utility/EventList.cs#L1443) | 3 | gold, happiness, relations |
| 25 | [EventList.cs:1515](../../Assets/Scripts/Utility/EventList.cs#L1515) | 3 | gold, population, happiness, units, bandits |
| 26 | [EventList.cs:1562](../../Assets/Scripts/Utility/EventList.cs#L1562) | 3 | population, happiness, units, rebels, traits/status |
| 27 | [EventList.cs:1622](../../Assets/Scripts/Utility/EventList.cs#L1622) | 3 | population, happiness, relations, units, experience, items, traits/status |
| 28 | [EventList.cs:1720](../../Assets/Scripts/Utility/EventList.cs#L1720) | 3 | population, happiness, units, traits/status |
| 29 | [EventList.cs:1757](../../Assets/Scripts/Utility/EventList.cs#L1757) | 3 | population, happiness, units, traits/status |
| 30 | [EventList.cs:1802](../../Assets/Scripts/Utility/EventList.cs#L1802) | 3 | experience, items, traits/status |
| 31 | [EventList.cs:1833](../../Assets/Scripts/Utility/EventList.cs#L1833) | 3 | happiness, bandits |
| 32 | [EventList.cs:1874](../../Assets/Scripts/Utility/EventList.cs#L1874) | 3 | gold, population, happiness, bandits |
| 33 | [EventList.cs:1927](../../Assets/Scripts/Utility/EventList.cs#L1927) | 6 | gold, population, happiness, experience |
| 34 | [EventList.cs:2026](../../Assets/Scripts/Utility/EventList.cs#L2026) | 3 | gold, population, experience |
| 35 | [EventList.cs:2067](../../Assets/Scripts/Utility/EventList.cs#L2067) | 3 | gold, population, happiness, units, traits/status |

## AI events

Only event-selection cases are listed here; the separate four-case frequency-setting switch is excluded. There are 32 event cases, matching the current 0–31 selector.

| Case | Source | Choice registrations | Effect markers present |
|---:|---|---:|---|
| 0 | [EventList.cs:2169](../../Assets/Scripts/Utility/EventList.cs#L2169) | 0 | relations |
| 1 | [EventList.cs:2174](../../Assets/Scripts/Utility/EventList.cs#L2174) | 0 | relations |
| 2 | [EventList.cs:2179](../../Assets/Scripts/Utility/EventList.cs#L2179) | 0 | relations |
| 3 | [EventList.cs:2184](../../Assets/Scripts/Utility/EventList.cs#L2184) | 0 | relations |
| 4 | [EventList.cs:2188](../../Assets/Scripts/Utility/EventList.cs#L2188) | 0 | relations |
| 5 | [EventList.cs:2193](../../Assets/Scripts/Utility/EventList.cs#L2193) | 0 | relations |
| 6 | [EventList.cs:2197](../../Assets/Scripts/Utility/EventList.cs#L2197) | 0 | relations |
| 7 | [EventList.cs:2202](../../Assets/Scripts/Utility/EventList.cs#L2202) | 0 | relations |
| 8 | [EventList.cs:2206](../../Assets/Scripts/Utility/EventList.cs#L2206) | 0 | relations |
| 9 | [EventList.cs:2211](../../Assets/Scripts/Utility/EventList.cs#L2211) | 0 | relations |
| 10 | [EventList.cs:2215](../../Assets/Scripts/Utility/EventList.cs#L2215) | 0 | review case source |
| 11 | [EventList.cs:2219](../../Assets/Scripts/Utility/EventList.cs#L2219) | 0 | review case source |
| 12 | [EventList.cs:2223](../../Assets/Scripts/Utility/EventList.cs#L2223) | 0 | bandits |
| 13 | [EventList.cs:2230](../../Assets/Scripts/Utility/EventList.cs#L2230) | 0 | relations |
| 14 | [EventList.cs:2235](../../Assets/Scripts/Utility/EventList.cs#L2235) | 0 | relations |
| 15 | [EventList.cs:2240](../../Assets/Scripts/Utility/EventList.cs#L2240) | 0 | relations |
| 16 | [EventList.cs:2244](../../Assets/Scripts/Utility/EventList.cs#L2244) | 0 | relations |
| 17 | [EventList.cs:2248](../../Assets/Scripts/Utility/EventList.cs#L2248) | 0 | bandits |
| 18 | [EventList.cs:2259](../../Assets/Scripts/Utility/EventList.cs#L2259) | 0 | relations |
| 19 | [EventList.cs:2264](../../Assets/Scripts/Utility/EventList.cs#L2264) | 0 | relations |
| 20 | [EventList.cs:2269](../../Assets/Scripts/Utility/EventList.cs#L2269) | 0 | relations |
| 21 | [EventList.cs:2273](../../Assets/Scripts/Utility/EventList.cs#L2273) | 0 | relations |
| 22 | [EventList.cs:2278](../../Assets/Scripts/Utility/EventList.cs#L2278) | 0 | bandits |
| 23 | [EventList.cs:2289](../../Assets/Scripts/Utility/EventList.cs#L2289) | 0 | relations |
| 24 | [EventList.cs:2293](../../Assets/Scripts/Utility/EventList.cs#L2293) | 0 | relations, bandits |
| 25 | [EventList.cs:2301](../../Assets/Scripts/Utility/EventList.cs#L2301) | 0 | relations |
| 26 | [EventList.cs:2306](../../Assets/Scripts/Utility/EventList.cs#L2306) | 0 | relations |
| 27 | [EventList.cs:2311](../../Assets/Scripts/Utility/EventList.cs#L2311) | 0 | relations, bandits |
| 28 | [EventList.cs:2324](../../Assets/Scripts/Utility/EventList.cs#L2324) | 0 | relations |
| 29 | [EventList.cs:2329](../../Assets/Scripts/Utility/EventList.cs#L2329) | 0 | relations |
| 30 | [EventList.cs:2334](../../Assets/Scripts/Utility/EventList.cs#L2334) | 0 | experience |
| 31 | [EventList.cs:2345](../../Assets/Scripts/Utility/EventList.cs#L2345) | 0 | experience |

## Custom-file route

- Source content: Assets/StreamingAssets/events.txt contains 141 literal Event markers; the CustomEventList parser uses that marker and a fixed line/CSV format. This is a source block count, not a runtime parser test.
- Startup seeds a missing UserData/events.txt from StreamingAssets; CustomEventList reads the user copy using ISO-8859-1. Existing user copies are a second source, so changing the shipped file alone does not retire the events.
- CustomEvent has prompt/choice/result strings and two sets of gold, population, happiness, global happiness, and relations effects. EventList.StartCustomEvent resolves empire/village/leader placeholders and mutates those 4X objects through button listeners.
- State constructs EventList at startup and during its user-data reset flow. Those initialization routes need deliberate disconnection; this audit did not invoke reset or read personal event files.

## Triggers, presentation, and candidate reuse

- Empire turn processing calls EventList.CheckStartEvent(this). StrategyMode end-of-round processing calls CheckStartAIEvent; StrategyMode initializes the EventScreen reference.
- The player selector draws IDs 0–35; the AI selector draws IDs 0–31. Event frequency settings and empire repeat/history fields do not constitute removal of their definitions/effects.
- EventScreen supports numeric choice keys; OnDisable schedules StrategyMode.Regenerate. Replace that strategic dependency before using the panel for company events.
- RandomEvent is a simple text/actions holder, but the source search found no use outside its own declaration file. It is not the active event framework.
- Future road-event/ambush design belongs to spec 13. Keep reusable presentation ideas only after reworking lifecycle, persistence, and effects for the company model.
