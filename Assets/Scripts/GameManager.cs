using System;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameState CurrentGameState { get; private set; }

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        // Initalize game static
        CurrentGameState = GameState.MainMenu;
    }

    public void ChangeGameState(GameState newGameState)
    {
        CurrentGameState = newGameState;
        
        // Logic what will happen than game will paused etc
    }
}