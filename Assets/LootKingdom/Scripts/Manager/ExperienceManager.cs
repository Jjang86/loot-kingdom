using System;
using System.Collections.Generic;
using UnityEngine;
    
public class Level {
    public const int one = 1000;
    public const int two = 3000;
    public const int three = 6000;
    public const int four = 10000;
    public const int five = 16000;
    public const int six = 21000;
    public const int seven = 28000;
    public const int eight = 36000;
    public const int nine = 45000;
    public const int ten = 55000;
}

public class ExperienceManager : Singleton<ExperienceManager> {
    public int currentLevel;
    public int nextLevelThreshold;

    private Dictionary<int, int> levelThresholds = new Dictionary<int, int>() {
        {1, Level.one},
        {2, Level.two},
        {3, Level.three},
        {4, Level.four},
        {5, Level.five},
        {6, Level.six},
        {7, Level.seven},
        {8, Level.eight},
        {9, Level.nine},
        {10, Level.ten}
    };

    public void SetCurrentLevel() {
        for (int i = 1; i <= levelThresholds.Count; i++) {
            currentLevel = i;
            if (LootKingdomDirector.user.experience <= levelThresholds[i]) {
                break;
            }
        }
    }

    public int SetNextLevelThreshold() {
        if (currentLevel > levelThresholds.Count) {
            return levelThresholds[currentLevel];
        }
        return levelThresholds[currentLevel + 1];
    }
}