// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-03: Separate retained accessory pixels and build replacement bundles for approved archival (spec 21).
// See CHANGES.md and License.txt for GPL-3.0 terms; original artwork attribution remains applicable.
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public static class RetiredContentBuild
{
    const string AccessoryPath = "Assets/Sprites/Units/SharedMaskParts.png";
    [Serializable] public class Part { public int oldIndex; public string name; public Rect rectangle; public Vector2 pivot; }
    [Serializable] public class Extraction { public string source, destination; public Part[] parts; public bool exactPixels; }
    public static void ExtractAccessory()
    {
        if (File.Exists(AccessoryPath)) throw new InvalidOperationException("Refusing to overwrite an existing accessory asset.");
        var bundle = AssetBundle.LoadFromFile("Assets/StreamingAssets/AssetBundles/specialmerc");
        if (bundle == null) throw new InvalidOperationException("Original accessory bundle unavailable.");
        var sprites = bundle.LoadAssetWithSubAssets<Sprite>("Asura");
        var importer = (TextureImporter)AssetImporter.GetAtPath("Assets/Sprites/Units/Asura.png");
        var parts = new[] { 39, 40 }.Select(index =>
        {
            var slice = importer.spritesheet.Single(s => s.name == sprites[index].name);
            return new Part { oldIndex = index, name = slice.name, rectangle = slice.rect, pivot = slice.pivot };
        }).ToArray();
        var source = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        source.LoadImage(File.ReadAllBytes("Assets/Sprites/Units/Asura.png"));
        int width = parts.Sum(p => (int)p.rectangle.width), height = parts.Max(p => (int)p.rectangle.height);
        var output = new Texture2D(width, height, TextureFormat.RGBA32, false);
        output.SetPixels32(new Color32[width * height]);
        var slices = new SpriteMetaData[2]; int x = 0;
        for (int i = 0; i < parts.Length; i++)
        {
            Rect r = parts[i].rectangle;
            var pixels = source.GetPixels((int)r.x, (int)r.y, (int)r.width, (int)r.height);
            output.SetPixels(x, 0, (int)r.width, (int)r.height, pixels);
            slices[i] = new SpriteMetaData { name = "SharedMask_" + i, rect = new Rect(x, 0, r.width, r.height),
                alignment = (int)SpriteAlignment.Custom, pivot = parts[i].pivot };
            x += (int)r.width;
        }
        output.Apply();
        File.WriteAllBytes(AccessoryPath, output.EncodeToPNG());
        AssetDatabase.ImportAsset(AccessoryPath, ImportAssetOptions.ForceSynchronousImport);
        var target = (TextureImporter)AssetImporter.GetAtPath(AccessoryPath);
        target.textureType = TextureImporterType.Sprite; target.spriteImportMode = SpriteImportMode.Multiple;
        target.spritePixelsPerUnit = importer.spritePixelsPerUnit; target.spritesheet = slices;
        target.mipmapEnabled = false; target.filterMode = FilterMode.Point; target.textureCompression = TextureImporterCompression.Uncompressed;
        target.alphaIsTransparency = importer.alphaIsTransparency; target.assetBundleName = "merc";
        target.SaveAndReimport();
        var decoded = new Texture2D(2, 2, TextureFormat.RGBA32, false); decoded.LoadImage(File.ReadAllBytes(AccessoryPath));
        x = 0;
        foreach (var part in parts)
        {
            var a = source.GetPixels((int)part.rectangle.x, (int)part.rectangle.y, (int)part.rectangle.width, (int)part.rectangle.height);
            var b = decoded.GetPixels(x, 0, (int)part.rectangle.width, (int)part.rectangle.height);
            if (!a.SequenceEqual(b)) throw new InvalidOperationException("Accessory pixel preservation failed.");
            x += (int)part.rectangle.width;
        }
        File.WriteAllText("Logs/retire21-accessory-extraction.json", JsonUtility.ToJson(new Extraction
        { source = "Assets/Sprites/Units/Asura.png", destination = AccessoryPath, parts = parts, exactPixels = true }, true));
        bundle.Unload(true);
    }

    public static void BuildReplacementBundles()
    {
        string output = "Logs/retire21-bundle-build-" + Guid.NewGuid().ToString("N") + "/AssetBundles";
        Directory.CreateDirectory(output);
        File.WriteAllText("Logs/retire21-replacement-bundle-directory.txt", Path.GetFullPath(output));
        var builds = AssetDatabase.GetAllAssetBundleNames().Where(n => new[] { "empire", "merc", "monster", "specialmerc" }.Contains(n))
            .Select(n => new AssetBundleBuild { assetBundleName = n, assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(n).Where(File.Exists).ToArray() })
            .Where(b => b.assetNames.Length > 0).ToArray();
        var manifest = BuildPipeline.BuildAssetBundles(output, builds, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        if (manifest == null) throw new InvalidOperationException("Replacement bundle build failed.");
        File.WriteAllLines("Logs/retire21-replacement-bundle-names.txt", manifest.GetAllAssetBundles());
        foreach (var name in manifest.GetAllAssetBundles())
        {
            var bundle = AssetBundle.LoadFromFile(Path.Combine(output, name));
            if (bundle == null) throw new InvalidOperationException("Replacement bundle could not be loaded: " + name);
            File.WriteAllLines("Logs/retire21-replacement-" + name + "-assets.txt", bundle.GetAllAssetNames());
            bundle.Unload(true);
        }
    }

    public static void ExtractSharedEffects()
    {
        ExtractEffect("Assets/Sprites/Units/Asura.png", "Assets/Prefabs/Tactical/Specific Effects/Slash Effect.prefab", "Assets/Sprites/TacticalEffects/SharedSlashFrames.png");
        ExtractEffect("Assets/Sprites/Units/RenamonBodies.png", "Assets/Prefabs/Tactical/Specific Effects/DiamondStorm.prefab", "Assets/Sprites/TacticalEffects/SharedDiamondProjectile.png");
    }
    static void ExtractEffect(string sourcePath, string prefabPath, string destination)
    {
        if (File.Exists(destination)) throw new InvalidOperationException("Effect output already exists: " + destination);
        string original = File.ReadAllText("Logs/retire21-serialized-before/" + prefabPath);
        string oldGuid = AssetDatabase.AssetPathToGUID(sourcePath);
        var references = Regex.Matches(original, @"\{fileID: (\d+), guid: " + oldGuid + @",\s*type: 3\}").Cast<Match>().ToArray();
        var ids = references.Select(m => m.Groups[1].Value).Distinct().ToArray();
        if (ids.Length == 0) throw new InvalidOperationException("No source frames found for shared effect.");
        string metadata = File.ReadAllText(sourcePath + ".meta");
        var importer = (TextureImporter)AssetImporter.GetAtPath(sourcePath);
        var slices = ids.Select(id =>
        {
            var name = Regex.Match(metadata, @"(?m)^    " + id + @": ([^\r\n]+)").Groups[1].Value;
            return importer.spritesheet.Single(s => s.name == name);
        }).ToArray();
        var source = new Texture2D(2, 2, TextureFormat.RGBA32, false); source.LoadImage(File.ReadAllBytes(sourcePath));
        var output = new Texture2D(slices.Sum(s => (int)s.rect.width), slices.Max(s => (int)s.rect.height), TextureFormat.RGBA32, false);
        output.SetPixels32(new Color32[output.width * output.height]);
        var targets = new SpriteMetaData[slices.Length]; int x = 0;
        for (int i = 0; i < slices.Length; i++)
        {
            Rect r = slices[i].rect;
            output.SetPixels(x, 0, (int)r.width, (int)r.height, source.GetPixels((int)r.x, (int)r.y, (int)r.width, (int)r.height));
            targets[i] = new SpriteMetaData { name = "SharedEffect_" + i, rect = new Rect(x, 0, r.width, r.height), alignment = (int)SpriteAlignment.Custom, pivot = slices[i].pivot };
            x += (int)r.width;
        }
        output.Apply(); File.WriteAllBytes(destination, output.EncodeToPNG());
        AssetDatabase.ImportAsset(destination, ImportAssetOptions.ForceSynchronousImport);
        var target = (TextureImporter)AssetImporter.GetAtPath(destination);
        target.textureType = TextureImporterType.Sprite; target.spriteImportMode = SpriteImportMode.Multiple;
        target.spritePixelsPerUnit = importer.spritePixelsPerUnit; target.spritesheet = targets;
        target.mipmapEnabled = false; target.filterMode = FilterMode.Point; target.textureCompression = TextureImporterCompression.Uncompressed;
        target.alphaIsTransparency = importer.alphaIsTransparency; target.SaveAndReimport();
        var retained = AssetDatabase.LoadAllAssetsAtPath(destination).OfType<Sprite>().ToDictionary(s => s.name);
        var replacements = new Dictionary<string, string>();
        for (int i = 0; i < ids.Length; i++)
        {
            string guid; long localId;
            if (!AssetDatabase.TryGetGUIDAndLocalFileIdentifier(retained[targets[i].name], out guid, out localId)) throw new InvalidOperationException("Missing retained effect identity.");
            replacements.Add(ids[i], "{fileID: " + localId + ", guid: " + guid + ", type: 3}");
        }
        var decoded = new Texture2D(2, 2, TextureFormat.RGBA32, false); decoded.LoadImage(File.ReadAllBytes(destination));
        x = 0;
        foreach (var slice in slices)
        {
            Rect r = slice.rect;
            if (!source.GetPixels((int)r.x, (int)r.y, (int)r.width, (int)r.height).SequenceEqual(decoded.GetPixels(x, 0, (int)r.width, (int)r.height))) throw new InvalidOperationException("Shared effect pixels changed.");
            x += (int)r.width;
        }
        string updated = Regex.Replace(original, @"\{fileID: (\d+), guid: " + oldGuid + @",\s*type: 3\}", m => replacements[m.Groups[1].Value]);
        File.WriteAllText(prefabPath, updated); AssetDatabase.ImportAsset(prefabPath, ImportAssetOptions.ForceSynchronousImport);
        File.WriteAllText("Logs/retire21-effect-" + Path.GetFileNameWithoutExtension(destination) + ".json", JsonUtility.ToJson(new Extraction
        { source = sourcePath, destination = destination, parts = slices.Select((s, i) => new Part { name = s.name, oldIndex = int.Parse(ids[i]), rectangle = s.rect, pivot = s.pivot }).ToArray(), exactPixels = true }, true));
    }
}
