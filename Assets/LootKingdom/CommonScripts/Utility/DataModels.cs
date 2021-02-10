using System.Collections.Generic;
using UnityEngine;

public class DataModels { }

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

public enum StatType {
    None,
    Crit,
    Damage
}

public class Item {
    public string id;
    public string name;
    public bool equipped;
    public ItemType type;
    public List<Stat> stats;
}

public class Stat {
    public StatType type;
    public float value;
}