using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemView : View {
    [SerializeField] private Image image;
    [SerializeField] private RectTransform rect;
    [SerializeField] private ItemType itemType;
    [SerializeField] private Button button;

    public ItemType type {
        get => itemType;
        set { itemType = value; }
    }

    public Sprite sprite {
        get => image.sprite;
        set { image.sprite = value; }
    }

    public bool interactable {
        get => button.interactable;
        set { button.interactable = value; }
    }

    public void OnClick(UnityAction action) {
        button.onClick.AddListener(action);
    }

    public void RemoveListener(UnityAction action) {
        button.onClick.RemoveListener(action);
    }

    public void RemoveAllListeners() {
        button.onClick.RemoveAllListeners();
    }
}