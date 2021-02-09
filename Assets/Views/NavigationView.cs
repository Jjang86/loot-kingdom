using System.Collections.Generic;
using UnityEngine;

public class NavigationView : View {
    private Stack<View> views = new Stack<View>();

    public void SetRoot(GameObject root) {
        transform.SetParent(root.transform, false);
    }
    
    public void Push(View view) {
        if (views.Count > 0) {
            views.Peek().gameObject.SetActive(false);
        }

        views.Push(view);
        view.transform.SetParent(gameObject.transform, false);
        view.navigationView = this;
        var viewRect = view.GetComponent<RectTransform>();
        viewRect.anchorMin = new Vector2(0.0f ,0.0f);
        viewRect.anchorMax = new Vector2(1.0f ,1.0f);
        viewRect.pivot = new Vector2(0.5f, 0.5f);
    }

    public void Pop() {
        Destroy(views.Peek().gameObject);
        views.Pop();
        views.Peek().gameObject.SetActive(true);
    }
}
