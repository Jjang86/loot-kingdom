using UnityEngine;
using UnityEngine.UI;

public class ItemView : View {
    [SerializeField] private Image image;
    [SerializeField] private RectTransform rect;
    [SerializeField] private ItemType itemType;

    public ItemType type {
        get => itemType;
        set { itemType = value; }
    }
}