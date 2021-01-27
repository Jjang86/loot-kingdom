using System;
using UnityEngine;

public static class CurrencyManager {
    public const int onLandGold = 20;

    private static int _gold = 1000;
    public static int gold {
        get => _gold;
        set {
            _gold = value;
            NotificationCenter.Notify(Notifications.Currency.goldChanged, value);
        }
    }

    public static int diamonds = 250;
    private static int _numRolls = 10;
    public static int numRolls {
        get => _numRolls;
        set {
            _numRolls = value;
            NotificationCenter.Notify(Notifications.Currency.numRollsChanged, value);
        }
    }

    private static int _goldEndAmount = gold;
    public static int goldEndAmount {
        get => _goldEndAmount;
        set {
            _goldEndAmount = value;
            Debug.Log(value);
        }
    }
}
