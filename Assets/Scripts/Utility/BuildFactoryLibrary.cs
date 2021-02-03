#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

public class BuildFactoryLibrary : MonoBehaviour {


    [MenuItem("LootKingdom/Build Factory")]
    static void BuildFactory() {

        Factory asset = ScriptableObject.CreateInstance<Factory>();
        var path = "Assets/Resources/Factory.asset";
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.CreateAsset(asset, path);

        BuildViews(asset);
        BuildSprites(asset);

        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

    private static void BuildViews(Factory asset) {
        var paths = new List<string>();

        var folders = BuildFactoryLibrary.GetSubFoldersRecursive("Assets");
        if (folders.Length > 0) {
            foreach (var folder in GetSubFoldersRecursive("Assets")) {
                paths.Add(folder);
            }
        }

        string[] guids = AssetDatabase.FindAssets("t:Object", paths.ToArray());


        foreach (string guid in guids) {
            string myObjectPath = AssetDatabase.GUIDToAssetPath(guid);
            Object[] objects = AssetDatabase.LoadAllAssetsAtPath(myObjectPath);

            // How do we find EXACTLY what we're looking for and no more? It's difficult because we will be searching through
            // every object in the AssetDatabase - which includes nested prefabs/game objects in side other prefabs.
            // It's very important that we prevent multiple entries by checking that the asset.types array
            // does not already contain a type. This is because Unity is walking all serialized connections
            // and you can have nested prefabs. That means we'll see the same types more than once. So filter. In addition,
            // it's possible that a prefab contains the nested prefab as a reference and we don't want that containing prefab.
            // That's why we only take the asset in which the top level game object is the same name as the prefab path.
            foreach (Object o in objects) {
                if (o) {
                    var typeName = o.GetType().ToString();
                    var pathParts = myObjectPath.Split(new char[] { '/' });
                    if (pathParts[pathParts.Length - 1] == o.name + ".prefab") {
                        if (o is View && !asset.viewTypes.Contains(typeName)) {
                            asset.viewTypes.Add(typeName);
                            asset.viewPrefabs.Add(o as View);
                        }
                    }
                }
            }
        }
    }

    private static void BuildSprites(Factory asset) {
        var paths = new List<string>() { "Assets/SpritesFactory" };

        string[] guids = AssetDatabase.FindAssets("t:Sprite", paths.ToArray());

        foreach (string guid in guids) {
            string myObjectPath = AssetDatabase.GUIDToAssetPath(guid);
            Object[] objects = AssetDatabase.LoadAllAssetsAtPath(myObjectPath);

            foreach (Object o in objects) {
                if (o) {

                    if (o is Sprite && !asset.preloadSpriteNames.Contains(o.name)) {
                        asset.preloadSpriteNames.Add(o.name);
                        asset.preloadSprites.Add(o as Sprite);
                    }

                }
            }
        }
    }

    private static string[] GetSubFoldersRecursive(string root) {
        var paths = new List<string>();

        // If there are no further subfolders then AssetDatabase.GetSubFolders returns 
        // an empty array => foreach will not be executed
        // This is the exit point for the recursion
        foreach (var path in AssetDatabase.GetSubFolders(root)) {
            // add this subfolder itself
            paths.Add(path);

            // If this has no further subfolders then simply no new elements are added
            paths.AddRange(GetSubFoldersRecursive(path));
        }

        return paths.ToArray();
    }
}
#endif
