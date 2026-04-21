using System;
using System.Collections;
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

    public GameState State { get; private set; }

    private GameState _previousState = GameState.Starting;

    void Start()
    {
        ChangeState(GameState.Starting);
    }

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        _previousState = State;
        State = newState;

        if (_previousState == GameState.Paused)
            Time.timeScale = 1;

        switch (newState)
        {
            case GameState.Starting:
                StartCoroutine(HandleStarting());
                break;

            case GameState.Playing:
                break;

            case GameState.Paused:
                Time.timeScale = 0;
                break;

            case GameState.FailScreen:
            case GameState.VictoryDance:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
    }

    private IEnumerator HandleStarting()
    {
        yield return new WaitForSeconds(2);
        ChangeState(GameState.Playing);
    }

    public void TogglePause()
    {
        if (State == GameState.Paused)
            ChangeState(_previousState);
        else
            ChangeState(GameState.Paused);
    }
}