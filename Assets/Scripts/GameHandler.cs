using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {

    private void Start() {
        Debug.Log("GameHandler.Start");

//        PlayerPrefs.SetInt("highscore", 10);
//        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetInt("highscore"));
    }

}
