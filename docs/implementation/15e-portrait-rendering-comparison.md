# 15E — Temporary portrait rendering comparison

2026-11-02. User accepts recruitment/company alignment; larger portraits with more row space remain deferred. User reports Single Battle scale1too small and scaling1.4poor quality; requests investigation and temporary main-page comparison. Production portrait views and texture imports remain unchanged in this packet.

## Evidence and hypotheses

Read SpriteContainer, CompleteSprite, UIUnitSprite, Image.prefab, AssetBundleLoader and Sprites-ColorSwap.shader. UI images are100×100rectangles sampling original layered sprites. Transform scale and CanvasScaler compose before rasterization; no intermediate100pixel bitmap/render target/resizing stage was found. A160pixel sprite in a100unit rectangle is not proof of irreversible downsampling: its original UV texture is sampled at final screen size.

Actual Unity read-only bundle audit (Logs/portrait-rendering-audit.json) found Lizards1120×1280RGBA32,56frames with160×160frames, Point filtering,11mip levels; Avians1–4each1920×1920RGBA32with160×160frames,Point filtering and one mip level. Dimensions match disk PNG headers. This rules out bundle texture downscaling for these five inspected sheets, not every body/clothing layer or species. Requested Gnolls asset name yielded no sprites and is not represented as audited. PNG files for runtime D matching may be missing for other layers; panel reports matched-layer counts rather than claiming full-source replacement.

Palette shader maps source red-channel values through _SwapTex. Blindly changing texture filtering can mix palette indices before decoding. C decodes four texel-center samples at base mip level and blends premultiplied colors afterward, clamping to each sprite's UV rectangle. Lizard mipmaps and nearest sampling are plausible contributors, not a visually established diagnosis. B isolates Simple versus inherited Sliced image geometry; it may look identical when borders are zero. No artwork is regenerated or permanently filtered.

## Main-menu comparison

Temporary Portrait comparison button on the main menu opens a modal overlay. Four cards use exactly the same Actor_Unit appearance/pose and final dimensions:

- A: current layered renderer/material/texture sampling.
- B: same textures/material with Simple UI images.
- C: Simple images plus comparison-only palette-aware smoothing/base-mip sampling.
- D: C using original PNG textures where a matching file is found, otherwise retaining loaded layers. Original PNGs are read in Editor only; partial counts are shown. No bitmap edits, disk output or import-setting changes.

Previous/Next species follow the currently enabled curated skirmish roster. New appearance refreshes all cards together; Change size cycles120/180/240logical units. Prefer A/B/C/D marks a session-only review choice; it does not apply production rendering or persist preferences. Close or Escape returns to the menu. Existing menu routes/logo remain. Temporary cloned sprites/textures/materials are destroyed on redraw/destruction; original shared textures/materials are never modified. Raw matching assumes loose, unrotated source sheets (the inspected sheets use this form); D should not be treated as proof for an unmatched/packed source.

Added PortraitComparisonPanel.cs, Resources/PortraitPaletteSmooth.shader (derived from retained Sprites-ColorSwap.shader, original upgrade notice preserved), Editor/PortraitRenderingAudit.cs and matching metas; modified StartMode.cs only for the temporary button hook. No source art, bundles, scene/prefab, production portrait scale/alignment, saves, packages or global filtering changes. New shader is experimental; no production winner chosen.

## Performed checks and limits

First compile exposed a Unity2018 editor API mismatch (ShaderUtil.ShaderHasError); removed that unsupported audit call. Final Unity2018.3.12f1 compile/read-only scene/bundle inspection passed return0: Logs/portrait-compare-final-compile-and-audit.log. Audit reports comparison shader resource present, not GPU shader correctness. No Play-mode visual or interactive pass, graphics-device shader evaluation, new player build, broad mechanics regression suite or personal campaign operations. User comparison remains essential.

GPL check: actual License.txt sections4–5 reviewed; dated notices on all affected sources/shader, new fork additions and shader derivation identified, exact source/meta paths in CHANGES. Prior attribution/licensing preserved; no third-party code/assets/dependencies. Existing provenance/distribution gates remain open, including the earlier supplied logo. No deletion/move/commit/publication.

Next: open Portrait comparison in current Unity Play mode. Compare a Lizard and Avian at180and240using identical appearance, inspect colors/edges/equipment, and report preferred letter/size plus D's matched-layer count. C/D may expose graphical issues not detectable in headless compilation; do not deploy one globally before the user selects a result. Recruitment/company spacing enlargement remains deferred and accepted alignment unchanged.


Final scoped review: Logs/portrait-compare-source-review.json confirms exactly the intended StartMode change and three new source/shader/meta pairs, no missing paths or other asset changes, no new compiler warnings/errors, complete dated notices/CHANGES coverage and preserved root license hash. Unity closed after checks. No visual pass or production choice inferred.


## User choice and adaptive production rendering (2026-11-02)

User found A/B equivalent and C/D equivalent: prefers B for pixel-art detail at larger sizes, accepts C-style smoothing only for shrinking, explicitly NEVER at native1×or above. Recruitment/company alignment accepted and larger row layout remains deferred. Do not impose discrete camera zoom levels or new portrait sizes in this packet.

Modified the existing Sprites/ColorSwap shader used by ColorSwap.mat and cloned unit palettes, covering UI and tactical sprite layers. Each fragment measures the UV-to-screen Jacobian in source texels. Its smallest squared singular value must exceed1.0002 for filtering: both source dimensions must actually shrink, including rotated/nonuniform geometry. Otherwise a base-level texel-center sample is decoded directly. The small tolerance conservatively favors crisp rendering near1×. For minification only, decode four base-level samples through the palette then blend premultiplied colors, matching C's color-preserving approach without ever interpolating palette indices. Uses float UVs, explicit mip0 and shader target3.0; original blend/stencil/PixelSnap/material palette properties preserved. No new mipmaps, texture reimports or artwork edits. Four-tap filtering is not a full area filter for extreme minification; performance and other hardware are not measured.

Native means actual source pixels against display pixels, NOT Transform.scale==1. A160pixel frame drawn160screen pixels stays crisp; at80screen pixels it smooths. Existing paper-doll Image.prefab uses100logical units, and CanvasScaler changes how those units map to screen pixels. This makes the cutoff independent of arbitrary UI scale numbers. No intermediate raster buffer or upscale/downscale asset processing is added.

SpriteContainer now sets paper-doll Image.Type.Simple (B); tactical SpriteRenderers already use normal sprite geometry. Original shared shader remains the material entry, so default _SmoothWhenSmall=1 reaches both existing unit-material paths. No scene/prefab/material assets or save data changed. PortraitComparisonPanel explicitly clones A/B materials with smoothing disabled and restores A's legacy Sliced/B's Simple geometry; C/D remain always-smoothed references. Labels identify those fixed references accurately. Session choices remain nonpersistent.

Final Unity2018.3.12f1 compile/read-only scene/existing texture audit passed return0: Logs/adaptive-portrait-compile-and-audit.log. This headless pass does NOT verify GPU output, threshold transition, zoom appearance, hardware compatibility or performance. No new tests/framework or player build. Focused visual review needed: small tactical units may smooth; native-size and enlarged units should retain crisp pixels through camera zoom. Compare to B for enlargement if useful. Do not infer the fixed C/D comparison cards now obey production's cutoff.

Dated modification notices and CHANGES exact paths cover Sprites-ColorSwap.shader, SpriteContainer.cs and PortraitComparisonPanel.cs. Original shader upgrade notice/licensing/attribution retained; no external code/assets/dependencies. ActualLicense4–5 reviewed, distribution/provenance gates unchanged. Await graphical feedback before more rendering/layout changes.
