using UnityEngine;
using UnityEngine.UI;

public enum ItemType {
    Helm,
    Chest,
    Legs,
    Gloves,
    Boots,
    Pet,
    MainHand,
    OffHand,
    Trap
}

public class ItemView : View {
    [SerializeField] private Image image;
    [SerializeField] private RectTransform rect;
    [SerializeField] private ItemType itemType;

    public ItemType type {
        get => itemType;
        set { itemType = value; }
    }
}