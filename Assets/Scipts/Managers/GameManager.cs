using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameEventListener OnGameStarted;
    public GameEventListener OnDataReady;
    public GameEventListener OnPlayerNameSetted;
    public GameEventListener OnPlayerCollide;

    private void Start() {
        OnGameStarted?.OnEventRaised();
    }
}
