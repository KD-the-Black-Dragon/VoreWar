# 04B2 — remaining miscellaneous log call sites

**Date:** 2026-10-30. Historical lexical handoff inventory after 04B1, not a runtime/AST audit. **04B2 is now implemented**; see [the source/verification record](04b2-callback-and-descriptor-reset.md). Original anchors below describe the preflight. Line anchors may shift. Inspect surrounding gameplay and retain mechanical statements while replacing only text/formatting. Do not blindly suppress all miscellaneous messages: many carry numerical/status information.

| Source | Candidate lines |
|---|---|
| `Assets/Scripts/AI/TacticalAI.cs` | 140, 156, 164, 212, 220, 1813 |
| `Assets/Scripts/AI/TacticalPathfinder.cs` | 43 |
| `Assets/Scripts/Entities/Items/EquipmentFunctions.cs` | 63, 73, 97 |
| `Assets/Scripts/Entities/Items/Potion.cs` | 40, 45, 48 |
| `Assets/Scripts/Entities/Spells/SpellList.cs` | 197, 677, 789, 796, 801, 953, 996, 1032, 1406, 1409 |
| `Assets/Scripts/Entities/Units/Actor_Unit.cs` | 221, 319, 2183, 2185, 2191, 2198, 2200, 2239, 2246, 2248, 2295, 2383, 2475, 2742, 2753, 3305, 3310, 3336, 3343, 3359, 3363, 3369, 3375, 3408, 3800, 3805, 3822, 3828, 3853, 3871, 3878, 3903, 3946, 3992, 4039 |
| `Assets/Scripts/Entities/Units/PredatorComponent.cs` | 1005, 1007, 1478, 1480, 1809, 1817, 1819, 1840, 1842, 2830, 2955, 4012, 4015, 4019, 4021, 4025, 4027, 4031, 4033, 4036, 4040, 4042, 4045, 4049, 4052, 4056, 4059, 4063, 4065, 4068, 4370, 4373, 4376, 4379, 4383, 4385, 4389, 4391, 4394, 4404, 4407, 4410, 4413, 4416, 4420, 4422, 4425, 4435, 4438, 4441, 4444, 4447, 4451, 4453, 4456, 4472, 4475, 4478, 4481, 4484, 4487, 4493, 4496, 4499, 4502, 4505, 4508, 4522, 4526, 4530, 4534, 4538, 4542, 4553, 4555, 4559, 4561, 4565, 4567, 4571, 4573, 4577, 4579, 4583, 4585, 4589, 4591, 4603, 4605, 4609, 4611, 4615, 4617, 4620, 4628, 4630, 4632, 4634, 4638, 4640, 4642, 4644, 4648, 4650, 4652, 4654, 4658, 4660, 4662, 4664, 4667, 4678, 4680, 4682, 4685, 4688, 4696, 4701, 4704, 4707, 4710, 4713, 4717, 4719, 4724, 4726, 4729, 4732, 4735, 4738, 4741, 4744, 4810, 4813, 4816, 4819, 4823, 4825, 4829, 4831, 4834, 4845, 4848, 4851, 4854, 4857, 4861, 4863, 4866, 4877, 4880, 4883, 4886, 4889, 4893, 4895, 4898, 4915, 4918, 4921, 4924, 4927, 4930, 4936, 4939, 4942, 4945, 4948, 4951, 4965, 4969, 4973, 4977, 4981, 4985, 4997, 4999, 5003, 5005, 5009, 5011, 5015, 5017, 5021, 5023, 5027, 5029, 5033, 5035, 5048, 5050, 5054, 5056, 5060, 5062, 5065, 5073, 5075, 5077, 5079, 5083, 5085, 5087, 5089, 5093, 5095, 5097, 5099, 5103, 5105, 5107, 5109, 5112, 5124, 5126, 5128, 5131, 5134, 5142, 5147, 5150, 5153, 5156, 5159, 5163, 5165, 5170, 5172, 5175, 5178, 5181, 5184, 5187, 5190 |
| `Assets/Scripts/Entities/Units/Unit.cs` | 1978, 3812, 3817 |
| `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs` | 1314, 1465, 1555, 1557 |
| `Assets/Scripts/Modes/TacticalMode.cs` | 5519 |
| `Assets/Scripts/Races/Traits.cs` | 658 |

Total lexical candidate lines: 301. Stored event Message strings already present in an old save may also need presentation policy; new campaign saves are separate. Descriptor/synonym helpers in LogUtilities and PreyLocStrings are a separate part of the 04B2 sweep.

## Completed disposition

All eleven caller files above were reviewed. Actor_Unit, PredatorComponent, SpellList, TacticalAI and TacticalUtilities received neutral text replacements; TacticalPathfinder, EquipmentFunctions, Potion, Unit, TacticalMode and Traits retain their existing functional notifications unchanged. LogUtilities' three descriptors and all 23 public PreyLocStrings methods were neutralized separately.

Current lexical call-site counts in table order: 6, 1, 3, 3, 10, 35, 36, 3, 3, 1, 1; total **102**. Registry/typed formatting remains the accepted 04B1 work. No legacy narrative wording pool remains in the covered callbacks. Unchanged messages report actual items, damage, statuses, summon/form/allegiance actions or retreat reasons; this text pass does not authorize or implement those mechanics' later retirement. Battle playtesting is deferred until species approval. Already-stored raw Message payloads in unsupported legacy saves are not rewritten.
