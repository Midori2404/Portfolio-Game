using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public GameState CurrentState { get; private set; } = GameState.Playing;

    public event Action<GameState> OnStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject); // Keep alive between scenes
    }

    public void SetState(GameState newState)
    {
        if (newState == CurrentState) return;

        CurrentState = newState;
        OnStateChanged?.Invoke(CurrentState); // Notify listeners
    }

    public bool IsState(GameState state) => CurrentState == state;
}

public enum GameState
{
    Playing,
    Dialogue,
    UI,
    Loading,
    Paused
}