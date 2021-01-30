using System;
using UnityEngine;

public interface ITimerDelegate {
    void OnShowTimer();
    void OnHideTimer();
    void OnSetTimerText(string time);
}

public class CurrencyManager : Singleton<CurrencyManager> {
    public const int onLandGold = 20;
    private const float TIME_TO_GET_DICE_ROLL = 5.0f;

    private float timeRemaining = TIME_TO_GET_DICE_ROLL;
    private float timer;
    private float delayAmount = 0.01f;
    private bool timerIsRunning = false;

    public ITimerDelegate timerDelegate;

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
        RunDiceTimer();

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

    private void RunDiceTimer() {
        if (numRolls < 10) {
            timerIsRunning = true;
            timerDelegate?.OnShowTimer();
        }
        else {
            timerIsRunning = false;
            timerDelegate?.OnHideTimer();
        }
        if (timerIsRunning) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else {
                numRolls++;
                timeRemaining = TIME_TO_GET_DICE_ROLL;
            }
        }
    }

    private void DisplayTime(float timeToDisplay) {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerDelegate?.OnSetTimerText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
