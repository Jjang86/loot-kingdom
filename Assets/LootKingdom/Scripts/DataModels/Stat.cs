using System.Collections.Generic;
using UnityEngine;

public enum StatType {
    None,
    Crit,
    Damage
}

public class Stat {
    public StatType type;
    public float value;
}