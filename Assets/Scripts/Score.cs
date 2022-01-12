
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score {

    public static void Start() {
        //ResetHighscore();
        Bird.GetInstance().OnDied += Bird_OnDied;
    }

    private static void Bird_OnDied(object sender, System.EventArgs e) {
        TrySetNewHighscore(Level.GetInstance().GetPipesPassedCount());
    }

    public static int GetHighscore() {
        return PlayerPrefs.GetInt("highscore");
    }

    public static bool TrySetNewHighscore(int score) {
        int currentHighscore = GetHighscore();
        if (score > currentHighscore) {
            // New Highscore
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            Debug.Log("New high score");
            return true;
        } else {
            Debug.Log("No new high score :(");
            return false;
        }
    }

    public static void ResetHighscore() {
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.Save();
    }
}
