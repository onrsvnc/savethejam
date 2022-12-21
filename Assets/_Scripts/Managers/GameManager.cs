using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    public GameState currentGameState;
    public System.Action<GameState> OnGameStateChanged;
    public int trophyCount;
    [SerializeField] private int targetTrophyCount;

    private void Start()
    {
        trophyCount = 0;
        currentGameState = GameState.Playing;
    }

    public void CheckWinState()
    {
        if (trophyCount == targetTrophyCount)
        {
            currentGameState = GameState.Win;
            OnGameStateChanged?.Invoke(currentGameState);
        }
    }

    public void CheckLoseState()
    {
        if (trophyCount == targetTrophyCount)
        {
            currentGameState = GameState.Win;
            OnGameStateChanged?.Invoke(currentGameState);
        }
        else
        {
            currentGameState = GameState.Lose;
            OnGameStateChanged?.Invoke(currentGameState);
        }
    }
}


