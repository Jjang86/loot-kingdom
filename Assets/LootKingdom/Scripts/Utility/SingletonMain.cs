using UnityEngine;

public abstract class SingletonMain<T> : ScriptableObject where T : ScriptableObject {
    private static T _instance;
    private static object _lock = new object();
    public static T Instance {
        get {
            lock (_lock) {
                if (_instance == null) {
                    _instance = (T)Resources.Load(typeof(T).ToString());
                }
                return _instance;
            }
        }
        set {
            Instance = value;
        }
    }
}