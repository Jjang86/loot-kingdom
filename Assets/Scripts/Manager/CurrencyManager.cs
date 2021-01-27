using System;
using UnityEngine;

public static class CurrencyManager {
    public static int gold = 1000;
    public static int diamonds = 250;
    private static int _numRolls = 10;
    public static int numRolls {
        get => _numRolls;
        set {
            _numRolls = value;
            NotificationCenter.Notify(Notifications.Currency.numRollsChanged, value);
        }
    }
}
