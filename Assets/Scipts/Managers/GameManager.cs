using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TIME_PERIOD {
    DEFAULT,
    NIGHT,
    DAY
}

[System.Serializable]
public struct User {
    public string playerName;
    public int playerScore;
    public TIME_PERIOD timePeriod;
    public string sunsetTimeStr;
    public TimeSpan sunsetTime;
}

public class GameManager : MonoBehaviour {
    public static GameManager Instance = null;
    public GameEventListener OnGameStarted;
    public GameEventListener OnDataReady;
    public GameEventListener OnPlayerNameSetted;

    User user;
    TimeSpan midnight = new TimeSpan (24, 0, 0);
    TimeSpan now = new TimeSpan (DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

    void Awake () {
        if (Instance == null)
            Instance = this;
    }

    private void Start () {
        OnGameStarted?.OnEventRaised ();
    }

    public string GetUserName () {
        return user.playerName;
    }

    public int GetUserScore () {
        return user.playerScore;
    }

    public void SetUserScore (int _inc = 1) {
        user.playerScore += _inc;
    }

    public void SetUserName (TMPro.TMP_InputField field) {
        user.playerName = field.text;
    }

    public void SetUserTimePeriod (TIME_PERIOD period) {
        user.timePeriod = period;
    }

    public TIME_PERIOD GetUserTimePeriod () {
        return user.timePeriod;
    }

    public void SetSunsetTime (string sunset) {
        if (string.IsNullOrEmpty (sunset)) {
            user.sunsetTimeStr = sunset;
            return;
        } else {
            string[] splittedTime = sunset.Split (":");
            if (splittedTime.Length > 2) {
                SetSunsetValuesForUser (splittedTime);
            } else {
                Debug.LogWarning ("Time format is wrong");
            }
        }
    }

    public void SetSunsetValuesForUser (string[] str) {
        user.sunsetTimeStr = str[0] + ":" + str[1];
        user.sunsetTime = TimeSpan.Parse (user.sunsetTimeStr);
        user.timePeriod = GetTimePeriod ();
    }

    public string GetSunsetTime () {
        return user.sunsetTimeStr;
    }

    public TIME_PERIOD GetTimePeriod () {
        return TimeSpan.Compare (now, user.sunsetTime) == 1 && TimeSpan.Compare (now, midnight) != 1 ? TIME_PERIOD.NIGHT : TIME_PERIOD.DAY;
    }

    public string GetTimePeriodAsString () {
        return user.timePeriod == TIME_PERIOD.NIGHT ? "Night" : "Day";
    }


    public void ResetGame() {
        SceneManager.LoadScene(0);
    }

}