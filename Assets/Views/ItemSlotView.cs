using UnityEngine;
using UnityEngine.UI;

public enum ItemSlotType {
    None,
    Any,
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

public class ItemSlotView : View {
    [SerializeField] private Image image;
    [SerializeField] private RectTransform rect;
    [SerializeField] private ItemSlotType itemSlotType;

    public ItemSlotType type {
        get => itemSlotType;
        set { itemSlotType = value; }
    }
}