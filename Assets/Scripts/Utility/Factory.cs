using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Factory", menuName=nameof(Factory))]
public class Factory : SingletonMain<Factory> {
    public List<View> viewPrefabs;
    public BoardScriptableObject boardScriptableObject;
    private List<View> allViews;

    public T CreateView<T>() where T : View {
        ConcatViews();
        foreach (var view in allViews) {
            if (view.GetType() == typeof(T)) {
                var viewRect = view.gameObject.GetComponent<RectTransform>();
                viewRect.anchorMin = new Vector2(0, 0);
                viewRect.anchorMax = new Vector2(1, 1);
                viewRect.pivot = new Vector2(0.5f, 0.5f);
                return (T)Instantiate(view);
            }
        }
        throw new Exception($"View not found: {typeof(T)}");
    }

    private void ConcatViews() {
        allViews = new List<View>();
        allViews.AddRange(viewPrefabs);
        allViews.AddRange(boardScriptableObject.boards);
    }
}
