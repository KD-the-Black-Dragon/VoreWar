// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-02: Read-only inspection of portrait bundle texture dimensions against source PNGs (spec 15E).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PortraitRenderingAudit
{
    [Serializable] public class Entry
    {
        public string asset, texture, format, filter, source;
        public int width, height, mipmaps, rawWidth, rawHeight, frames;
        public float frameWidth, frameHeight;
    }
    [Serializable] public class Report { public List<Entry> entries = new List<Entry>(); public bool comparisonShaderPresent; }
    public static void Capture()
    {
        SpeciesResourceAudit.CaptureCompanyWiring();
        var report = new Report();
        var bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "AssetBundles/empire"));
        foreach (string name in new[] { "Lizards", "Avians1", "Avians2", "Avians3", "Avians4", "Gnolls" })
        {
            var sprites = bundle.LoadAssetWithSubAssets<Sprite>(name);
            if (sprites == null || sprites.Length == 0) continue;
            var s = sprites[0]; var t = s.texture;
            var e = new Entry { asset = name, texture = t.name, width = t.width, height = t.height,
                frames = sprites.Length, frameWidth = s.rect.width, frameHeight = s.rect.height,
                format = t.format.ToString(), filter = t.filterMode.ToString(), mipmaps = t.mipmapCount };
            foreach (string path in Directory.GetFiles(Path.Combine(Application.dataPath, "Sprites"), "*.png", SearchOption.AllDirectories))
                if (string.Equals(Path.GetFileNameWithoutExtension(path), t.name, StringComparison.OrdinalIgnoreCase))
                {
                    byte[] bytes = File.ReadAllBytes(path); e.source = path;
                    e.rawWidth = bytes[16] << 24 | bytes[17] << 16 | bytes[18] << 8 | bytes[19];
                    e.rawHeight = bytes[20] << 24 | bytes[21] << 16 | bytes[22] << 8 | bytes[23]; break;
                }
            report.entries.Add(e);
        }
        var shader = Resources.Load<Shader>("PortraitPaletteSmooth");
        report.comparisonShaderPresent = shader != null;
        Directory.CreateDirectory("Logs");
        File.WriteAllText("Logs/portrait-rendering-audit.json", JsonUtility.ToJson(report, true));
        bundle.Unload(true);
    }
}
