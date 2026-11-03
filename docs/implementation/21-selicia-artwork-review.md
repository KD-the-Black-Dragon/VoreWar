# Swallowed-Selicia artwork review

Recorded 2026-11-03. This is an inspection index, not an instruction to trim sprite sheets. Broad shared sheets remain intact. Original source line numbers refer to the pre-cleanup snapshot in Logs/retire21-source-before. Conditions sometimes reuse generic shapes: an entry does not prove that its frames depict Selicia.

## Dedicated filename candidates

These are archived, not deleted. Inspect the artwork before deciding whether any generic parts should be retained.
- `Recycle Bin/Retired Content/Assets/Sprites/Units/selicia_in_taurus.png`
- `Recycle Bin/Retired Content/Assets/Sprites/Units/Selicia.png`
- `Recycle Bin/Retired Content/Assets/Sprites/Units/zoey_sel.png`

## Mixed-sheet frame references

Exact conditions and source bodies are preserved in [the full audit](21-selicia-shared-frame-review.json). SpriteDictionary identifiers below must be mapped to their loader assets before judging the artwork; no frame removal is approved.

### `Assets/Scripts/Races/Graphics/DefaultRaceData.cs`
- Original line 632: `SpriteDictionary.Bellies[17]`
- Original line 638: `SpriteDictionary.Bellies[16]`
- Original line 793: `SpriteDictionary.FurryDicks[42]`
- Original line 798: `SpriteDictionary.FurryDicks[41]`
- Original line 803: `SpriteDictionary.FurryDicks[40]`
- Original line 808: `SpriteDictionary.FurryDicks[39]`
- Original line 842: `SpriteDictionary.Balls[24]`
- Original line 847: `SpriteDictionary.Balls[23]`
- Original line 852: `SpriteDictionary.Balls[22]`
- Original line 857: `SpriteDictionary.Balls[21]`

### `Assets/Scripts/Races/Graphics/MainRaces/AntQueen.cs`
- Original line 144: `Sprites2[95]`
- Original line 149: `Sprites2[94]`
- Original line 154: `Sprites2[93]`
- Original line 159: `Sprites2[92]`
- Original line 236: `Sprites2[30]`
- Original line 240: `Sprites2[29]`
- Original line 244: `Sprites2[28]`
- Original line 269: `Sprites2[61]`
- Original line 273: `Sprites2[60]`
- Original line 277: `Sprites2[59]`
- Original line 344: `Sprites2[132]`
- Original line 349: `Sprites2[131]`
- Original line 354: `Sprites2[130]`

### `Assets/Scripts/Races/Graphics/MainRaces/Avians.cs`
- Original line 312: `Sprites3[96]`
- Original line 317: `Sprites3[143]`
- Original line 322: `Sprites3[142]`
- Original line 327: `Sprites3[141]`
- Original line 384: `Sprites3[31]`
- Original line 388: `Sprites3[30]`
- Original line 392: `Sprites3[29]`
- Original line 417: `Sprites3[63]`
- Original line 421: `Sprites3[62]`
- Original line 425: `Sprites3[61]`
- Original line 492: `Sprites[137]`
- Original line 497: `Sprites[136]`
- Original line 502: `Sprites[135]`

### `Assets/Scripts/Races/Graphics/MainRaces/Bees.cs`
- Original line 308: `Sprites2[143]`
- Original line 313: `Sprites2[142]`
- Original line 318: `Sprites2[141]`
- Original line 323: `Sprites2[140]`
- Original line 403: `Sprites2[69]`
- Original line 407: `Sprites2[68]`
- Original line 411: `Sprites2[67]`
- Original line 436: `Sprites2[104]`
- Original line 440: `Sprites2[103]`
- Original line 444: `Sprites2[102]`
- Original line 525: `Sprites2[37]`
- Original line 529: `Sprites2[36]`
- Original line 533: `Sprites2[35]`

### `Assets/Scripts/Races/Graphics/MainRaces/Deer.cs`
- Original line 518: `Sprites4[99]`
- Original line 523: `Sprites4[98]`
- Original line 528: `Sprites4[97]`
- Original line 533: `Sprites4[96]`
- Original line 590: `Sprites4[31]`
- Original line 594: `Sprites4[30]`
- Original line 598: `Sprites4[29]`
- Original line 623: `Sprites4[63]`
- Original line 627: `Sprites4[62]`
- Original line 631: `Sprites4[61]`
- Original line 741: `Sprites3[139 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)]`
- Original line 746: `Sprites3[138 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)]`
- Original line 751: `Sprites3[137 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)]`

### `Assets/Scripts/Races/Graphics/MainRaces/Demiants.cs`
- Original line 242: `Sprites2[99]`
- Original line 247: `Sprites2[98]`
- Original line 252: `Sprites2[97]`
- Original line 257: `Sprites2[96]`
- Original line 314: `Sprites2[31]`
- Original line 318: `Sprites2[30]`
- Original line 322: `Sprites2[29]`
- Original line 347: `Sprites2[63]`
- Original line 351: `Sprites2[62]`
- Original line 355: `Sprites2[61]`
- Original line 422: `Sprites2[137]`
- Original line 427: `Sprites2[136]`
- Original line 432: `Sprites2[135]`

### `Assets/Scripts/Races/Graphics/MainRaces/Demibats.cs`
- Original line 307: `Sprites3[99]`
- Original line 312: `Sprites3[98]`
- Original line 317: `Sprites3[97]`
- Original line 322: `Sprites3[96]`
- Original line 408: `Sprites3[31]`
- Original line 412: `Sprites3[30]`
- Original line 416: `Sprites3[29]`
- Original line 441: `Sprites3[63]`
- Original line 445: `Sprites3[62]`
- Original line 449: `Sprites3[61]`
- Original line 559: `Sprites3[137 + ((actor.Unit.Furry && Config.FurryGenitals) ? 38 : 0)]`
- Original line 564: `Sprites3[136 + ((actor.Unit.Furry && Config.FurryGenitals) ? 38 : 0)]`
- Original line 569: `Sprites3[135 + ((actor.Unit.Furry && Config.FurryGenitals) ? 38 : 0)]`

### `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs`
- Original line 1104: `SpritesBreasts[95]`
- Original line 1108: `SpritesBreasts[94]`
- Original line 1112: `SpritesBreasts[93]`
- Original line 1141: `SpritesBreasts[127]`
- Original line 1145: `SpritesBreasts[126]`
- Original line 1149: `SpritesBreasts[125]`
- Original line 1279: `SpritesBelliesBalls[75]`
- Original line 1284: `SpritesBelliesBalls[74]`
- Original line 1289: `SpritesBelliesBalls[73]`
- Original line 1294: `SpritesBelliesBalls[72]`
- Original line 1331: `SpritesBelliesBalls[111]`
- Original line 1336: `SpritesBelliesBalls[110]`
- Original line 1341: `SpritesBelliesBalls[109]`
- Original line 1346: `SpritesBelliesBalls[108]`
- Original line 1389: `SpriteDictionary.HumansVoreSprites[105]`
- Original line 1394: `SpriteDictionary.HumansVoreSprites[104]`
- Original line 1399: `SpriteDictionary.HumansVoreSprites[103]`
- Original line 1404: `SpriteDictionary.HumansVoreSprites[102]`
- Original line 1446: `SpritesBelliesBalls[75]`
- Original line 1451: `SpritesBelliesBalls[74]`
- Original line 1456: `SpritesBelliesBalls[73]`
- Original line 1461: `SpritesBelliesBalls[72]`
- Original line 1615: `SpritesBreasts[31]`
- Original line 1619: `SpritesBreasts[30]`
- Original line 1623: `SpritesBreasts[29]`
- Original line 1643: `SpriteDictionary.HumansVoreSprites[31]`
- Original line 1647: `SpriteDictionary.HumansVoreSprites[30]`
- Original line 1651: `SpriteDictionary.HumansVoreSprites[29]`
- Original line 1679: `SpritesBreasts[63]`
- Original line 1683: `SpritesBreasts[62]`
- Original line 1687: `SpritesBreasts[61]`
- Original line 1707: `SpriteDictionary.HumansVoreSprites[63]`
- Original line 1711: `SpriteDictionary.HumansVoreSprites[62]`
- Original line 1715: `SpriteDictionary.HumansVoreSprites[61]`
- Original line 1802: `SpritesBelliesBalls[39]`
- Original line 1807: `SpritesBelliesBalls[38]`
- Original line 1812: `SpritesBelliesBalls[37]`
- Original line 1848: `SpriteDictionary.HumansVoreSprites[141]`
- Original line 1853: `SpriteDictionary.HumansVoreSprites[140]`
- Original line 1858: `SpriteDictionary.HumansVoreSprites[139]`

### `Assets/Scripts/Races/Graphics/MainRaces/Demifrogs.cs`
- Original line 220: `Sprites3A[94]`
- Original line 225: `Sprites3A[93]`
- Original line 230: `Sprites3A[92]`
- Original line 235: `Sprites3A[91]`
- Original line 266: `Sprites3[94]`
- Original line 271: `Sprites3[93]`
- Original line 276: `Sprites3[92]`
- Original line 281: `Sprites3[91]`
- Original line 368: `Sprites3A[30]`
- Original line 372: `Sprites3A[29]`
- Original line 376: `Sprites3A[28]`
- Original line 398: `Sprites3[30]`
- Original line 402: `Sprites3[29]`
- Original line 406: `Sprites3[28]`
- Original line 436: `Sprites3A[61]`
- Original line 440: `Sprites3A[60]`
- Original line 444: `Sprites3A[59]`
- Original line 466: `Sprites3[61]`
- Original line 470: `Sprites3[60]`
- Original line 474: `Sprites3[59]`
- Original line 548: `Sprites3A[132]`
- Original line 553: `Sprites3A[131]`
- Original line 558: `Sprites3A[130]`
- Original line 599: `Sprites3[132]`
- Original line 604: `Sprites3[131]`
- Original line 609: `Sprites3[130]`

### `Assets/Scripts/Races/Graphics/MainRaces/Demisharks.cs`
- Original line 388: `Sprites4[99]`
- Original line 393: `Sprites4[98]`
- Original line 398: `Sprites4[97]`
- Original line 403: `Sprites4[96]`
- Original line 469: `Sprites4[31]`
- Original line 473: `Sprites4[30]`
- Original line 477: `Sprites4[29]`
- Original line 502: `Sprites4[63]`
- Original line 506: `Sprites4[62]`
- Original line 510: `Sprites4[61]`
- Original line 577: `Sprites4[137]`
- Original line 582: `Sprites4[136]`
- Original line 587: `Sprites4[135]`

### `Assets/Scripts/Races/Graphics/MainRaces/Equines.cs`
- Original line 164: `SpritesBase[89]`
- Original line 168: `SpritesBase[88]`
- Original line 172: `SpritesBase[87]`
- Original line 305: `SpritesBase[119]`
- Original line 309: `SpritesBase[118]`
- Original line 313: `SpritesBase[117]`
- Original line 342: `SpritesBase[149]`
- Original line 346: `SpritesBase[148]`
- Original line 350: `SpritesBase[147]`
- Original line 427: `SpritesBase[59]`
- Original line 431: `SpritesBase[58]`
- Original line 435: `SpritesBase[57]`

### `Assets/Scripts/Races/Graphics/MainRaces/Gnolls.cs`
- Original line 322: ``
- Original line 327: ``
- Original line 332: ``
- Original line 337: ``
- Original line 724: ``
- Original line 728: ``
- Original line 732: ``
- Original line 757: ``
- Original line 761: ``
- Original line 765: ``
- Original line 832: ``
- Original line 837: ``
- Original line 842: ``

### `Assets/Scripts/Races/Graphics/MainRaces/Jackals.cs`
- Original line 548: `Sprites4[105]`
- Original line 553: `Sprites4[104]`
- Original line 558: `Sprites4[103]`
- Original line 563: `Sprites4[102]`
- Original line 640: `Sprites4[31]`
- Original line 644: `Sprites4[30]`
- Original line 648: `Sprites4[29]`
- Original line 673: `Sprites4[63]`
- Original line 677: `Sprites4[62]`
- Original line 681: `Sprites4[61]`
- Original line 747: `Sprites4[141]`
- Original line 752: `Sprites4[140]`
- Original line 757: `Sprites4[139]`

### `Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs`
- Original line 121: `SpriteDictionary.Kangaroos[136]`
- Original line 127: `SpriteDictionary.Kangaroos[131]`
- Original line 148: `SpriteDictionary.Kangaroos[148]`

### `Assets/Scripts/Races/Graphics/MainRaces/Lizards.cs`
- Original line 277: `SpriteDictionary.Bellies[17]`
- Original line 283: `SpriteDictionary.Bellies[16]`
- Original line 305: `SpriteDictionary.Bellies[17]`
- Original line 311: `SpriteDictionary.Bellies[16]`
- Original line 474: `SpriteDictionary.Balls[24]`
- Original line 479: `SpriteDictionary.Balls[23]`
- Original line 484: `SpriteDictionary.Balls[22]`
- Original line 489: `SpriteDictionary.Balls[21]`
- Original line 517: `SpriteDictionary.LizardsBooty[42]`
- Original line 522: `SpriteDictionary.LizardsBooty[41]`
- Original line 527: `SpriteDictionary.LizardsBooty[40]`
- Original line 532: `SpriteDictionary.LizardsBooty[39]`

### `Assets/Scripts/Races/Graphics/MainRaces/Lupine.cs`
- Original line 345: `Sprites3[99]`
- Original line 350: `Sprites3[98]`
- Original line 355: `Sprites3[97]`
- Original line 360: `Sprites3[96]`
- Original line 416: `Sprites3[31]`
- Original line 420: `Sprites3[30]`
- Original line 424: `Sprites3[29]`
- Original line 449: `Sprites3[63]`
- Original line 453: `Sprites3[62]`
- Original line 457: `Sprites3[61]`
- Original line 524: `Sprites3[139 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)]`
- Original line 529: `Sprites3[138 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)]`
- Original line 534: `Sprites3[137 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)]`

### `Assets/Scripts/Races/Graphics/MainRaces/MatronsMinions.cs`
- Original line 73: ``
- Original line 74: ``
- Original line 81: ``
- Original line 82: ``
- Original line 224: `Sprites[62]`
- Original line 226: `Sprites[113]`, `Sprites[112]`, `Sprites[111]`
- Original line 247: `Sprites[84]`
- Original line 249: `Sprites[110]`, `Sprites[109]`, `Sprites[108]`
- Original line 263: `Sprites[102]`
- Original line 265: `Sprites[116]`, `Sprites[115]`, `Sprites[114]`

### `Assets/Scripts/Races/Graphics/MainRaces/Mice.cs`
- Original line 455: `Sprites8[105]`, `Sprites9[105]`
- Original line 464: `Sprites8[104]`, `Sprites9[104]`
- Original line 473: `Sprites8[103]`, `Sprites9[103]`
- Original line 482: `Sprites8[102]`, `Sprites9[102]`
- Original line 546: `Sprites8[31]`, `Sprites9[31]`
- Original line 554: `Sprites8[30]`, `Sprites9[30]`
- Original line 562: `Sprites8[29]`, `Sprites9[29]`
- Original line 599: `Sprites8[63]`, `Sprites9[63]`
- Original line 607: `Sprites8[62]`, `Sprites9[62]`
- Original line 615: `Sprites8[61]`, `Sprites9[61]`
- Original line 737: `Sprites7[176]`, `Sprites7[139 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)]`
- Original line 744: `Sprites7[175]`, `Sprites7[138 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)]`
- Original line 751: `Sprites7[174]`, `Sprites7[137 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)]`

### `Assets/Scripts/Races/Graphics/MainRaces/Panthers.cs`
- Original line 356: `SpritesVore[35]`
- Original line 360: `SpritesVore[34]`
- Original line 364: `SpritesVore[33]`
- Original line 397: `SpritesVore[70]`
- Original line 401: `SpritesVore[69]`
- Original line 405: `SpritesVore[68]`
- Original line 486: `SpritesVore[145]`
- Original line 491: `SpritesVore[144]`
- Original line 496: `SpritesVore[143]`
- Original line 501: `SpritesVore[142]`
- Original line 506: `SpritesVore[141]`
- Original line 553: `SpritesVore[139]`
- Original line 558: `SpritesVore[138]`
- Original line 563: `SpritesVore[137]`

### `Assets/Scripts/Races/Graphics/MainRaces/Sergal.cs`
- Original line 106: `Sprites[45]`

### `Assets/Scripts/Races/Graphics/Mercs/Badgers.cs`
- Original line 215: ``
- Original line 220: ``
- Original line 225: ``
- Original line 230: ``
- Original line 297: ``
- Original line 301: ``
- Original line 305: ``
- Original line 337: ``
- Original line 341: ``
- Original line 345: ``
- Original line 419: ``
- Original line 424: ``
- Original line 429: ``

### `Assets/Scripts/Races/Graphics/Mercs/Hippos.cs`
- Original line 180: `Sprites3[94]`
- Original line 185: `Sprites3[93]`
- Original line 190: `Sprites3[92]`
- Original line 195: `Sprites3[91]`
- Original line 243: `Sprites3[31]`
- Original line 247: `Sprites3[30]`
- Original line 251: `Sprites3[29]`
- Original line 276: `Sprites3[63]`
- Original line 280: `Sprites3[62]`
- Original line 284: `Sprites3[61]`
- Original line 351: `Sprites[119]`
- Original line 356: `Sprites[118]`
- Original line 361: `Sprites[117]`

### `Assets/Scripts/Races/Graphics/Mercs/Komodos.cs`
- Original line 294: `Sprites2[90]`
- Original line 299: `Sprites2[89]`
- Original line 304: `Sprites2[88]`
- Original line 309: `Sprites2[87]`
- Original line 369: `Sprites2[29]`
- Original line 373: `Sprites2[28]`
- Original line 377: `Sprites2[27]`
- Original line 402: `Sprites2[59]`
- Original line 406: `Sprites2[58]`
- Original line 410: `Sprites2[57]`
- Original line 477: `Sprites2[127]`
- Original line 482: `Sprites2[126]`
- Original line 487: `Sprites2[125]`

### `Assets/Scripts/Races/Graphics/Mercs/Vargul.cs`
- Original line 324: `Sprites3[90]`
- Original line 329: `Sprites3[89]`
- Original line 334: `Sprites3[88]`
- Original line 339: `Sprites3[87]`
- Original line 393: `Sprites3[29]`
- Original line 397: `Sprites3[28]`
- Original line 401: `Sprites3[27]`
- Original line 426: `Sprites3[59]`
- Original line 430: `Sprites3[58]`
- Original line 434: `Sprites3[57]`
- Original line 501: `Sprites3[127]`
- Original line 506: `Sprites3[126]`
- Original line 511: `Sprites3[125]`

### `Assets/Scripts/Races/Graphics/Monsters/Ant.cs`
- Original line 23: `SpriteDictionary.Ant[19]`
- Original line 28: `SpriteDictionary.Ant[18]`

### `Assets/Scripts/Races/Graphics/Monsters/Bat.cs`
- Original line 89: `SpriteDictionary.Bat[49]`
- Original line 93: `SpriteDictionary.Bat[48]`
- Original line 97: `SpriteDictionary.Bat[47]`
- Original line 132: `SpriteDictionary.Bat[27]`
- Original line 140: `SpriteDictionary.Bat[26]`, `SpriteDictionary.Bat[25]`, `SpriteDictionary.Bat[24]`

### `Assets/Scripts/Races/Graphics/Monsters/BlackWidow.cs`
- Original line 135: `SpriteDictionary.BlackWidow[54]`
- Original line 140: `SpriteDictionary.BlackWidow[53]`
- Original line 145: `SpriteDictionary.BlackWidow[52]`
- Original line 150: `SpriteDictionary.BlackWidow[51]`
- Original line 155: `SpriteDictionary.BlackWidow[50]`
- Original line 160: `SpriteDictionary.BlackWidow[49]`
- Original line 165: `SpriteDictionary.BlackWidow[48]`
- Original line 170: `SpriteDictionary.BlackWidow[47]`
- Original line 175: `SpriteDictionary.BlackWidow[46]`
- Original line 180: `SpriteDictionary.BlackWidow[45]`

### `Assets/Scripts/Races/Graphics/Monsters/Catfish.cs`
- Original line 52: ``
- Original line 66: ``
- Original line 80: ``
- Original line 124: `Sprites[80]`
- Original line 126: `Sprites[80]`
- Original line 248: `Sprites[59]`
- Original line 250: `Sprites[58]`, `Sprites[57]`

### `Assets/Scripts/Races/Graphics/Monsters/CoralSlug.cs`
- Original line 60: `Sprites[20]`
- Original line 62: `Sprites[10 + actor.GetStomachSize(9)]`

### `Assets/Scripts/Races/Graphics/Monsters/Dragon.cs`
- Original line 262: `Sprites[69]`
- Original line 264: `Sprites[68]`, `Sprites[67]`
- Original line 311: `Sprites[91]`
- Original line 313: `Sprites[90]`, `Sprites[89]`

### `Assets/Scripts/Races/Graphics/Monsters/Dragonfly.cs`
- Original line 30: `SpriteDictionary.Dragonfly[27]`

### `Assets/Scripts/Races/Graphics/Monsters/Dratopyr.cs`
- Original line 261: `SpriteDictionary.Dratopyr[99 + shake]`
- Original line 266: `SpriteDictionary.Dratopyr[96 + shake]`, `SpriteDictionary.Dratopyr[93 + shake]`, `SpriteDictionary.Dratopyr[90 + shake]`, `SpriteDictionary.Dratopyr[87 + shake]`
- Original line 360: `SpriteDictionary.Dratopyr[168 + shake]`
- Original line 365: `SpriteDictionary.Dratopyr[165 + shake]`, `SpriteDictionary.Dratopyr[162 + shake]`, `SpriteDictionary.Dratopyr[159 + shake]`, `SpriteDictionary.Dratopyr[156 + shake]`
- Original line 374: `SpriteDictionary.Dratopyr[168 + shake]`
- Original line 379: `SpriteDictionary.Dratopyr[165 + shake]`, `SpriteDictionary.Dratopyr[162 + shake]`, `SpriteDictionary.Dratopyr[159 + shake]`, `SpriteDictionary.Dratopyr[156 + shake]`

### `Assets/Scripts/Races/Graphics/Monsters/EasternDragon.cs`
- Original line 108: `SpriteDictionary.EasternDragon[86]`
- Original line 113: `SpriteDictionary.EasternDragon[85]`
- Original line 118: `SpriteDictionary.EasternDragon[84]`
- Original line 227: `SpriteDictionary.EasternDragon[37]`
- Original line 232: `SpriteDictionary.EasternDragon[36]`
- Original line 237: `SpriteDictionary.EasternDragon[35]`
- Original line 265: `SpriteDictionary.EasternDragon[66]`
- Original line 269: `SpriteDictionary.EasternDragon[65]`
- Original line 273: `SpriteDictionary.EasternDragon[64]`

### `Assets/Scripts/Races/Graphics/Monsters/FeralFox.cs`
- Original line 319: `SpriteDictionary.FeralFox[96]`
- Original line 324: `SpriteDictionary.FeralFox[95]`
- Original line 329: `SpriteDictionary.FeralFox[94]`
- Original line 334: `SpriteDictionary.FeralFox[93]`
- Original line 339: `SpriteDictionary.FeralFox[92]`
- Original line 344: `SpriteDictionary.FeralFox[91]`
- Original line 349: `SpriteDictionary.FeralFox[90]`
- Original line 380: `SpriteDictionary.FeralFox[139]`
- Original line 385: `SpriteDictionary.FeralFox[138]`
- Original line 390: `SpriteDictionary.FeralFox[137]`
- Original line 395: `SpriteDictionary.FeralFox[136]`
- Original line 400: `SpriteDictionary.FeralFox[135]`
- Original line 405: `SpriteDictionary.FeralFox[134]`
- Original line 410: `SpriteDictionary.FeralFox[133]`

### `Assets/Scripts/Races/Graphics/Monsters/FeralHorses.cs`
- Original line 126: `SpriteDictionary.FeralHorses[54]`
- Original line 132: `SpriteDictionary.FeralHorses[53]`
- Original line 138: `SpriteDictionary.FeralHorses[52]`
- Original line 144: `SpriteDictionary.FeralHorses[51]`
- Original line 150: `SpriteDictionary.FeralHorses[50]`
- Original line 182: `SpriteDictionary.FeralHorses[79]`
- Original line 187: `SpriteDictionary.FeralHorses[78]`
- Original line 192: `SpriteDictionary.FeralHorses[77]`
- Original line 197: `SpriteDictionary.FeralHorses[76]`
- Original line 202: `SpriteDictionary.FeralHorses[75]`
- Original line 207: `SpriteDictionary.FeralHorses[74]`

### `Assets/Scripts/Races/Graphics/Monsters/FeralLions.cs`
- Original line 145: `Sprites[75]`, `Sprites[10]`
- Original line 157: `Sprites[86]`, `Sprites[25]`

### `Assets/Scripts/Races/Graphics/Monsters/FeralLizards.cs`
- Original line 191: `Sprites[54]`
- Original line 193: `Sprites[53]`, `Sprites[52]`
- Original line 220: `Sprites[84]`
- Original line 222: `Sprites[83]`, `Sprites[82]`

### `Assets/Scripts/Races/Graphics/Monsters/FeralSlime.cs`
- Original line 40: `SpriteDictionary.FeralSlime[7]`
- Original line 44: `SpriteDictionary.FeralSlime[6]`
- Original line 58: `SpriteDictionary.FeralSlime[15]`
- Original line 62: `SpriteDictionary.FeralSlime[14]`
- Original line 73: `SpriteDictionary.FeralSlime[39]`
- Original line 77: `SpriteDictionary.FeralSlime[38]`
- Original line 81: `SpriteDictionary.FeralSlime[37]`
- Original line 85: `SpriteDictionary.FeralSlime[36]`

### `Assets/Scripts/Races/Graphics/Monsters/FeralUmbreon.cs`
- Original line 62: `SpriteDictionary.FeralUmbreon[19]`
- Original line 67: `SpriteDictionary.FeralUmbreon[18]`
- Original line 72: `SpriteDictionary.FeralUmbreon[17]`

### `Assets/Scripts/Races/Graphics/Monsters/FeralWolf.cs`
- Original line 38: `SpriteDictionary.FeralWolf[16]`
- Original line 45: ``

### `Assets/Scripts/Races/Graphics/Monsters/Gazelle.cs`
- Original line 126: `Sprites2[30]`
- Original line 128: `Sprites2[29]`, `Sprites2[28]`
- Original line 155: `Sprites2[66]`
- Original line 157: `Sprites2[65]`, `Sprites2[64]`

### `Assets/Scripts/Races/Graphics/Monsters/Gryphons.cs`
- Original line 49: ``
- Original line 64: ``
- Original line 79: ``
- Original line 302: `Sprites[47]`
- Original line 313: `Sprites[47]`
- Original line 324: `Sprites[47]`
- Original line 392: `Sprites[35]`
- Original line 394: `Sprites[61]`, `Sprites[60]`
- Original line 424: `Sprites[47]`
- Original line 435: `Sprites[47]`

### `Assets/Scripts/Races/Graphics/Monsters/Harvester.cs`
- Original line 186: `SpriteDictionary.Harvester[49]`
- Original line 191: `SpriteDictionary.Harvester[48]`
- Original line 196: `SpriteDictionary.Harvester[47]`
- Original line 201: `SpriteDictionary.Harvester[46]`
- Original line 206: `SpriteDictionary.Harvester[45]`
- Original line 211: `SpriteDictionary.Harvester[44]`
- Original line 216: `SpriteDictionary.Harvester[43]`
- Original line 221: `SpriteDictionary.Harvester[42]`
- Original line 226: `SpriteDictionary.Harvester[41]`
- Original line 231: `SpriteDictionary.Harvester[40]`
- Original line 236: `SpriteDictionary.Harvester[39]`
- Original line 241: `SpriteDictionary.Harvester[38]`

### `Assets/Scripts/Races/Graphics/Monsters/Kobolds.cs`
- Original line 73: ``
- Original line 74: ``
- Original line 81: ``
- Original line 82: ``
- Original line 224: `Sprites[62]`
- Original line 226: `Sprites[113]`, `Sprites[112]`, `Sprites[111]`
- Original line 247: `Sprites[84]`
- Original line 249: `Sprites[110]`, `Sprites[109]`, `Sprites[108]`
- Original line 263: `Sprites[102]`
- Original line 265: `Sprites[116]`, `Sprites[115]`, `Sprites[114]`

### `Assets/Scripts/Races/Graphics/Monsters/Mantis.cs`
- Original line 358: `Sprites[107]`
- Original line 360: `Sprites[106]`, `Sprites[105]`

### `Assets/Scripts/Races/Graphics/Monsters/Monitors.cs`
- Original line 237: `Sprites[153]`
- Original line 242: `Sprites[152]`
- Original line 247: `Sprites[151]`
- Original line 252: `Sprites[150]`
- Original line 332: `Sprites[119]`
- Original line 337: `Sprites[118]`
- Original line 342: `Sprites[117]`

### `Assets/Scripts/Races/Graphics/Monsters/Otachi.cs`
- Original line 160: ``
- Original line 161: ``
- Original line 217: ``
- Original line 218: ``

### `Assets/Scripts/Races/Graphics/Monsters/Plant.cs`
- Original line 52: `SpriteDictionary.Plant[32]`

### `Assets/Scripts/Races/Graphics/Monsters/Raiju.cs`
- Original line 125: `SpriteDictionary.Raiju240[23]`
- Original line 130: `SpriteDictionary.Raiju240[22]`
- Original line 135: `SpriteDictionary.Raiju240[21]`
- Original line 140: `SpriteDictionary.Raiju240[20]`
- Original line 145: `SpriteDictionary.Raiju240[19]`
- Original line 150: `SpriteDictionary.Raiju240[18]`
- Original line 155: `SpriteDictionary.Raiju240[17]`
- Original line 160: `SpriteDictionary.Raiju240[16]`
- Original line 204: `SpriteDictionary.Raiju240[11]`
- Original line 209: `SpriteDictionary.Raiju240[10]`
- Original line 214: `SpriteDictionary.Raiju240[9]`
- Original line 219: `SpriteDictionary.Raiju240[8]`
- Original line 224: `SpriteDictionary.Raiju240[7]`
- Original line 229: `SpriteDictionary.Raiju240[6]`
- Original line 234: `SpriteDictionary.Raiju240[5]`
- Original line 239: `SpriteDictionary.Raiju240[4]`

### `Assets/Scripts/Races/Graphics/Monsters/RockSlug.cs`
- Original line 40: `Sprites[18]`
- Original line 42: `Sprites[6 + actor.GetStomachSize(11)]`

### `Assets/Scripts/Races/Graphics/Monsters/Salamanders.cs`
- Original line 149: `Sprites[72]`
- Original line 151: `Sprites[71]`, `Sprites[70]`

### `Assets/Scripts/Races/Graphics/Monsters/Smudger.cs`
- Original line 110: `SpriteDictionary.Smudger[143]`
- Original line 115: `SpriteDictionary.Smudger[142]`
- Original line 120: `SpriteDictionary.Smudger[141]`
- Original line 125: `SpriteDictionary.Smudger[140]`
- Original line 130: `SpriteDictionary.Smudger[139]`
- Original line 135: `SpriteDictionary.Smudger[138]`
- Original line 192: `SpriteDictionary.Smudger[179]`
- Original line 197: `SpriteDictionary.Smudger[178]`
- Original line 202: `SpriteDictionary.Smudger[177]`
- Original line 207: `SpriteDictionary.Smudger[176]`
- Original line 212: `SpriteDictionary.Smudger[175]`
- Original line 217: `SpriteDictionary.Smudger[174]`
- Original line 253: `SpriteDictionary.Smudger[73]`

### `Assets/Scripts/Races/Graphics/Monsters/SpaceCroach.cs`
- Original line 113: `SpriteDictionary.SpaceCroach[63]`
- Original line 118: `SpriteDictionary.SpaceCroach[62]`
- Original line 123: `SpriteDictionary.SpaceCroach[61]`
- Original line 128: `SpriteDictionary.SpaceCroach[60]`
- Original line 133: `SpriteDictionary.SpaceCroach[59]`
- Original line 138: `SpriteDictionary.SpaceCroach[58]`
- Original line 143: `SpriteDictionary.SpaceCroach[57]`
- Original line 148: `SpriteDictionary.SpaceCroach[56]`

### `Assets/Scripts/Races/Graphics/Monsters/SpitterSlug.cs`
- Original line 55: `Sprites[21]`
- Original line 57: `Sprites[11 + actor.GetStomachSize(9)]`

### `Assets/Scripts/Races/Graphics/Monsters/SpringSlug.cs`
- Original line 27: `Sprites[15]`
- Original line 29: `Sprites[5 + actor.GetStomachSize(9)]`
- Original line 47: `Sprites[26]`
- Original line 49: `Sprites[16 + actor.GetStomachSize(9)]`

### `Assets/Scripts/Races/Graphics/Monsters/Terminid.cs`
- Original line 90: `SpriteDictionary.Terminid[37]`
- Original line 95: `SpriteDictionary.Terminid[36]`
- Original line 100: `SpriteDictionary.Terminid[35]`
- Original line 105: `SpriteDictionary.Terminid[34]`
- Original line 110: `SpriteDictionary.Terminid[33]`
- Original line 115: `SpriteDictionary.Terminid[32]`

### `Assets/Scripts/Races/Graphics/Monsters/Terrorbird.cs`
- Original line 117: `Sprites[65]`
- Original line 119: `Sprites[64]`, `Sprites[63]`, `Sprites[62]`

### `Assets/Scripts/Races/Graphics/Monsters/Trex.cs`
- Original line 188: `SpriteDictionary.Trex[106]`
- Original line 193: `SpriteDictionary.Trex[105]`
- Original line 198: `SpriteDictionary.Trex[104]`
- Original line 203: `SpriteDictionary.Trex[103]`
- Original line 208: `SpriteDictionary.Trex[102]`
- Original line 213: `SpriteDictionary.Trex[101]`
- Original line 218: `SpriteDictionary.Trex[100]`
- Original line 223: `SpriteDictionary.Trex[99]`
- Original line 239: `SpriteDictionary.Trex[67]`
- Original line 244: `SpriteDictionary.Trex[66]`
- Original line 249: `SpriteDictionary.Trex[65]`
- Original line 254: `SpriteDictionary.Trex[64]`
- Original line 259: `SpriteDictionary.Trex[63]`
- Original line 264: `SpriteDictionary.Trex[62]`
- Original line 269: `SpriteDictionary.Trex[61]`
- Original line 274: `SpriteDictionary.Trex[60]`

### `Assets/Scripts/Races/Graphics/Monsters/Utahraptor.cs`
- Original line 497: ``
- Original line 501: ``
- Original line 505: ``
- Original line 509: ``
- Original line 513: ``
- Original line 517: ``
- Original line 521: ``
- Original line 525: ``
- Original line 529: ``
- Original line 533: ``
- Original line 542: ``
- Original line 546: ``
- Original line 550: ``
- Original line 554: ``
- Original line 558: ``
- Original line 562: ``
- Original line 566: ``
- Original line 570: ``
- Original line 574: ``
- Original line 578: ``
- Original line 636: ``
- Original line 640: ``
- Original line 644: ``
- Original line 648: ``
- Original line 652: ``
- Original line 656: ``
- Original line 660: ``
- Original line 664: ``
- Original line 668: ``
- Original line 672: ``

### `Assets/Scripts/Races/Graphics/Monsters/WarriorAnt.cs`
- Original line 101: `SpriteDictionary.WarriorAnt[49]`
- Original line 106: `SpriteDictionary.WarriorAnt[48]`
- Original line 111: `SpriteDictionary.WarriorAnt[47]`
- Original line 116: `SpriteDictionary.WarriorAnt[46]`
- Original line 121: `SpriteDictionary.WarriorAnt[45]`
- Original line 126: `SpriteDictionary.WarriorAnt[44]`
- Original line 131: `SpriteDictionary.WarriorAnt[43]`
- Original line 136: `SpriteDictionary.WarriorAnt[42]`

### `Assets/Scripts/Races/Graphics/Monsters/Wyvern.cs`
- Original line 200: `SpriteDictionary.Wyvern[50]`

### `Assets/Scripts/Races/Graphics/Monsters/WyvernMatron.cs`
- Original line 186: `SpriteDictionary.WyvernMatron[53]`
- Original line 191: `SpriteDictionary.WyvernMatron[52]`
- Original line 196: `SpriteDictionary.WyvernMatron[51]`
- Original line 201: `SpriteDictionary.WyvernMatron[50]`
- Original line 206: `SpriteDictionary.WyvernMatron[49]`
- Original line 211: `SpriteDictionary.WyvernMatron[48]`
- Original line 228: `SpriteDictionary.WyvernMatron[88]`
- Original line 233: `SpriteDictionary.WyvernMatron[87]`
- Original line 238: `SpriteDictionary.WyvernMatron[86]`
- Original line 243: `SpriteDictionary.WyvernMatron[85]`
- Original line 248: `SpriteDictionary.WyvernMatron[84]`
- Original line 253: `SpriteDictionary.WyvernMatron[83]`
- Original line 258: `SpriteDictionary.WyvernMatron[82]`
- Original line 263: `SpriteDictionary.WyvernMatron[81]`
- Original line 268: `SpriteDictionary.WyvernMatron[80]`

### `Assets/Scripts/Races/Graphics/TaurHumanHalf.cs`
- Original line 323: `SpriteDictionary.HumansVoreSprites[105]`
- Original line 328: `SpriteDictionary.HumansVoreSprites[104]`
- Original line 333: `SpriteDictionary.HumansVoreSprites[103]`
- Original line 338: `SpriteDictionary.HumansVoreSprites[102]`
- Original line 383: `SpriteDictionary.HumansVoreSprites[31]`
- Original line 387: `SpriteDictionary.HumansVoreSprites[30]`
- Original line 391: `SpriteDictionary.HumansVoreSprites[29]`
- Original line 416: `SpriteDictionary.HumansVoreSprites[63]`
- Original line 420: `SpriteDictionary.HumansVoreSprites[62]`
- Original line 424: `SpriteDictionary.HumansVoreSprites[61]`

## Atlas lookup

This maps the historical catalog names to files. Frame expressions in the sections above are runtime array indices; they are not pixel coordinates. Retained atlases are unchanged.

| Catalog field | Source atlas file(s) |
|---|---|
| `Ant` | `Assets/Sprites/Units/Ant.png` |
| `Balls` | `Assets/Sprites/Units/privates.png` |
| `Bat` | `Assets/Sprites/Units/Bat.png` |
| `Bellies` | `Assets/Sprites/Units/Sprites.png` |
| `BlackWidow` | `Assets/Sprites/Units/BlackWidow.png` |
| `Dragonfly` | `Assets/Sprites/Units/Dragonfly.png` |
| `Dratopyr` | `Assets/Sprites/Units/Dratopyr.png` |
| `EasternDragon` | `Assets/Sprites/Units/LungDragon.png` |
| `FeralFox` | `Assets/Sprites/Units/FeralFox.png` |
| `FeralHorses` | `Assets/Sprites/Units/FeralHorses.png` |
| `FeralSlime` | `Assets/Sprites/Units/FeralSlime.png` |
| `FeralUmbreon` | `Assets/Sprites/Units/Eeveelution/FeralUmbreon.png` |
| `FeralWolf` | `Assets/Sprites/Units/feralWolves.png` |
| `FurryDicks` | `Assets/Sprites/Units/FurryGenitals.png` |
| `Harvester` | `Assets/Sprites/Units/Harvester.png` |
| `HumansVoreSprites` | `Assets/Sprites/Units/humanrework/humansVoreSprites.png` |
| `Kangaroos` | `Assets/Sprites/Units/Kangaroos.png`; `Assets/Resources/ColorPalettes/Kangaroos.png` |
| `LizardsBooty` | `Assets/Sprites/Units/LizardsBooty.png` |
| `Plant` | `Assets/Sprites/Units/PlantRace.png` |
| `Raiju240` | `Assets/Sprites/Units/Raiju240.png` |
| `Smudger` | `Assets/Sprites/Units/Smudger.png` |
| `SpaceCroach` | `Assets/Sprites/Units/Spacecroach.png` |
| `Terminid` | `Assets/Sprites/Units/Terminid.png` |
| `Trex` | `Assets/Sprites/Units/Trex.png` |
| `WarriorAnt` | `Assets/Sprites/Units/WarriorAnt.png` |
| `Wyvern` | `Assets/Sprites/Units/Wyvern.png` |
| `WyvernMatron` | `Assets/Sprites/Units/WyvernMatron.png` |
