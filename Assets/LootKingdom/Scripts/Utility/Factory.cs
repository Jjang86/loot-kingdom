using System;
using System.Collections.Generic;
using UnityEngine;

public class Factory : SingletonScriptableObject<Factory> {
    // Views - preloaded in memory
    public List<string> viewTypes = new List<string>();
    public List<View> viewPrefabs = new List<View>();
    private Dictionary<Type, View> views = new Dictionary<Type, View>();

    // Sprites - preloaded in memory
    public List<string> preloadSpriteNames = new List<string>();
    public List<Sprite> preloadSprites = new List<Sprite>();
    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();


    public static T CreateView<T>(Action<T> callback = null) where T : View {
        if (Instance.views.Count == 0) {
            try {
                for (int i = 0; i < Instance.viewTypes.Count; ++i) {
                    Instance.views.Add(Type.GetType(Instance.viewTypes[i]), Instance.viewPrefabs[i]);
                }
            }
            catch {
                throw new Exception($"Try rebuilding Factory.");
            }
        }
        T view;
        try {
            view = (T)Instantiate(Instance.views[typeof(T)]);
        }
        catch {
            throw new Exception($"{typeof(T)} Not found - you probably need to rebuild Factory.");
        }

        if (callback != null) { callback(view); }
        return view;
    }

    public static Sprite GetSprite(string spriteName) {
        if (Instance.sprites.Count == 0) {
            try {
                for (int i = 0; i < Instance.preloadSpriteNames.Count; ++i) {
                    Instance.sprites.Add(Instance.preloadSpriteNames[i], Instance.preloadSprites[i]);
                }
            }
            catch {
                throw new Exception($"Try rebuilding Factory.");
            }
        }
        Sprite sprite = null;
        try {
            sprite = Instance.sprites[spriteName];
        }
        catch {
            throw new Exception($"Sprite with name {spriteName} not found - you proably need to rebuild Factory. Ensure that your sprite is in the Assets/Sprites folder.");
        }

        return sprite;
    }
}
