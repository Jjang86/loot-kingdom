using System;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager> {
    public const int onLandGold = 20;
    private float timer;
    private float delayAmount = 0.01f;

    private int _gold = 1000;
    public int gold {
        get => _gold;
        set {
            _gold = value;
            NotificationCenter.Notify(Notifications.Currency.goldChanged, value);
        }
    }

    public int diamonds = 250;
    private int _numRolls = 10;
    public int numRolls {
        get => _numRolls;
        set {
            _numRolls = value;
            NotificationCenter.Notify(Notifications.Currency.numRollsChanged, value);
        }
    }

    private int _goldEndAmount;
    public int goldEndAmount {
        get => _goldEndAmount;
        set {
            _goldEndAmount = value;
        }
    }

    void Update() {
        if (gold < goldEndAmount) {
            timer += Time.deltaTime;

            if (timer >= delayAmount) {
                timer = 0f;
                gold++;
            }
        }
    }
    
    void Start() {
        goldEndAmount = gold;
    }
}
