using System;
using UnityEngine;

public interface ITimerDelegate {
    void OnShowTimer();
    void OnHideTimer();
    void OnSetTimerText(string time);
}

public class CurrencyManager : Singleton<CurrencyManager> {
    public const int onLandExperience = 10;
    public const int onLandGold = 20;
    public const int diamondAmountOnGo = 15;
    private const float TIME_TO_GET_DICE_ROLL = 5.0f;

    private float timeRemaining = TIME_TO_GET_DICE_ROLL;
    private float timer;
    private float delayAmount = 0.01f;
    private bool timerIsRunning = false;

    public ITimerDelegate timerDelegate;

    private int _gold = 0;
    public int gold {
        get => _gold;
        set {
            _gold = value;
            NotificationCenter.Notify(Notifications.Currency.goldChanged, value);
        }
    }
    
    private int _diamonds = 0;
    public int diamonds {
        get => _diamonds;
        set {
            _diamonds = value;      
            NotificationCenter.Notify(Notifications.Currency.diamondChanged, value);
        }
    }
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
            LootKingdomDirector.user.gold = value;
        }
    }

    private int _diamondEndAmount;
    public int diamondEndAmount {
        get => _diamondEndAmount;
        set {
            _diamondEndAmount = value;
            LootKingdomDirector.user.diamonds = value;
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

        if (gold > goldEndAmount) {
            timer += Time.deltaTime;

            if (timer >= delayAmount) {
                timer = 0f;
                gold--;
            }
        }

        if (diamonds < diamondEndAmount) {
            timer += Time.deltaTime;

            if (timer >= delayAmount) {
                timer = 0f;
                diamonds++;
            }
        }

        if (diamonds > diamondEndAmount) {
            timer += Time.deltaTime;

            if (timer >= delayAmount) {
                timer = 0f;
                diamonds--;
            }
        }
    }
    
    void Start() {
        goldEndAmount = gold;
        diamondEndAmount = diamonds;
    }

    private void RunDiceTimer() {
        if (numRolls < 10) {
            if (!timerIsRunning) { timerDelegate?.OnShowTimer(); }
            timerIsRunning = true;
        }
        else {
            if (timerIsRunning) { timerDelegate?.OnHideTimer(); }
            timerIsRunning = false;
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
