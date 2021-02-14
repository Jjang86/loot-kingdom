using System.Collections.Generic;
using UnityEngine;

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

public class Item {
    public string id;
    public string name;
    public bool equipped;
    public ItemType type;
    public List<Stat> stats;
}