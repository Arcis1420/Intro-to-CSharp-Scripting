using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum GameState
{
    Starting = 1,
    Playing = 10,
    Paused = 15,
    FailScreen = 20,
    VictoryDance = 25
}

public class GameManager : Singleton<GameManager>   
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    public GameState State { get; private set;  }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.Starting);
    }

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;

        switch (newState)
        {
            case GameState.Starting:
                StartCoroutine(HandleStarting());
                break;
            case GameState.Playing:
                break;
            case GameState.Paused: 
                break;
            case GameState.FailScreen: 
                break;
            case GameState.VictoryDance: 
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, message: null);
        }


        OnAfterStateChanged?.Invoke(newState);

    }

    private IEnumerator HandleStarting()
    {
        yield return new WaitForSeconds(2);
        ChangeState(GameState.Playing);
    }
}
