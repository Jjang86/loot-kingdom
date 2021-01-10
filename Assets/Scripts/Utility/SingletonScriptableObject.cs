using System.Linq;
using UnityEngine;

// Based on http://wiki.unity3d.com/index.php/Singleton

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// </summary>
public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject {

    private static T _instance;
    private static object _lock = new object();
    public static T Instance {
        get {
            lock (_lock) {
                if (_instance == null) {
                    _instance = loadFromResources();
                    if (_instance == null) {
                        _instance = createRuntimeCopy();
                    }
                }
                return _instance;
            }
        }
        set {
            Instance = value;
        }
    }

    public static T loadFromResources() {
        return (T)Resources.Load($"Config/{getTypeNameSansNamespace()}");
    }

    private static T createRuntimeCopy() {
        Debug.LogWarning($"Missing {getTypeNameSansNamespace()}...  Creating a runtime only copy in its place.");
        return CreateInstance<T>();
    }

    private static string getTypeNameSansNamespace() {
        var split = typeof(T).ToString().Split('.');
        return split[split.Length - 1];
    }
}