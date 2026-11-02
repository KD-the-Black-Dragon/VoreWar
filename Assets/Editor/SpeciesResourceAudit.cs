// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-01: Inspect sprite prefab serialization and dependencies without modifying assets (spec 03B6).
// 2026-11-01: Read-only creator/portrait/terrain scene-wiring inspection (specs 07–08).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

// Read-only inspection for staged artwork retirement; never resaves or rebuilds assets.
public static class SpeciesResourceAudit
{
    const string PrefabPath = "Assets/Prefabs/Sprite Dictionary.prefab";

    [Serializable]
    public class FieldRecord
    {
        public string name;
        public bool isStatic;
        public bool isSerialized;
        public int arraySize;
    }

    [Serializable]
    public class Snapshot
    {
        public string prefabPath;
        public string unityVersion;
        public List<FieldRecord> fields = new List<FieldRecord>();
        public List<string> objectReferences = new List<string>();
        public string[] dependencies;
    }

    public static void CaptureBefore()
    {
        Capture("packet-03b6-unity-resource-before.json");
    }

    public static void CaptureAfter()
    {
        Capture("packet-03b6-unity-resource-after.json");
    }

    [Serializable]
    public class CompanyWiring
    {
        public bool gameManager, customizer, portrait, name, gender, pronouns, buttonPanel, buttonPrefab, imagePrefabSprite;
        public int terrainSprites;
        public float creatorWidth, creatorHeight;
    }
    public static void CaptureCompanyWiring()
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/base.unity");
        var manager = UnityEngine.Object.FindObjectOfType<GameManager>();
        var c = manager == null || manager.Recruit_Mode == null ? null : manager.Recruit_Mode.CustomizerUI;
        var image = manager == null || manager.ImagePrefab == null ? null : manager.ImagePrefab.GetComponent<UnityEngine.UI.Image>();
        var report = new CompanyWiring
        {
            gameManager = manager != null, customizer = c != null,
            portrait = c != null && c.DisplayedSprite != null,
            name = c != null && c.DisplayedSprite != null && c.DisplayedSprite.Name != null,
            gender = c != null && c.Gender != null,
            pronouns = c != null && c.Nominative != null && c.Accusative != null && c.PronominalPossessive != null && c.PredicativePossessive != null && c.Reflexive != null && c.Quantification != null,
            buttonPanel = c != null && c.ButtonPanel != null, buttonPrefab = c != null && c.ButtonPrefab != null,
            imagePrefabSprite = image != null && image.sprite != null,
            terrainSprites = manager == null || manager.StrategyMode == null || manager.StrategyMode.TileSprites == null ? 0 : manager.StrategyMode.TileSprites.Length,
            creatorWidth = c == null ? 0 : c.GetComponent<RectTransform>().rect.width,
            creatorHeight = c == null ? 0 : c.GetComponent<RectTransform>().rect.height
        };
        string directory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        Directory.CreateDirectory(directory);
        File.WriteAllText(Path.Combine(directory, "people-economy-wiring.json"), JsonUtility.ToJson(report, true));
    }

    static void Capture(string filename)
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
        if (prefab == null)
            throw new InvalidOperationException("Sprite dictionary prefab is unavailable.");
        var dictionary = prefab.GetComponent<SpriteDictionary>();
        if (dictionary == null)
            throw new InvalidOperationException("Sprite dictionary component is unavailable.");
        var serialized = new SerializedObject(dictionary);
        var snapshot = new Snapshot { prefabPath = PrefabPath, unityVersion = Application.unityVersion };
        foreach (var field in typeof(SpriteDictionary).GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
        {
            var property = serialized.FindProperty(field.Name);
            snapshot.fields.Add(new FieldRecord
            {
                name = field.Name,
                isStatic = field.IsStatic,
                isSerialized = property != null,
                arraySize = property != null && property.isArray ? property.arraySize : -1
            });
        }
        var iterator = serialized.GetIterator();
        while (iterator.Next(true))
        {
            if (iterator.propertyType != SerializedPropertyType.ObjectReference)
                continue;
            var value = iterator.objectReferenceValue;
            snapshot.objectReferences.Add(iterator.propertyPath + "=" +
                (value == null ? "null" : AssetDatabase.GetAssetPath(value) + "#" + value.name));
        }
        snapshot.dependencies = AssetDatabase.GetDependencies(PrefabPath, true);
        Array.Sort(snapshot.dependencies, StringComparer.Ordinal);
        string directory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        Directory.CreateDirectory(directory);
        string output = Path.Combine(directory, filename);
        File.WriteAllText(output, JsonUtility.ToJson(snapshot, true));
        Debug.Log("Species resource inspection written to " + output);
    }
}
