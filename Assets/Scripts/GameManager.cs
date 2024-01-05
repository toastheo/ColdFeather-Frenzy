using System;
using UnityEngine;

public enum GameState
{
    MainMenu,
    ContinuePlaying,
    Paused,
    GameOver,
    StartPlaying
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
        switch (CurrentGameState)
        {
            case GameState.MainMenu:
                print("Reached MainMenu");
                Time.timeScale = 0f;
                WorldTime.WorldTime.Instance.StopTime();
                WorldTime.WorldTime.Instance.ResetTime();
                break;
            case GameState.ContinuePlaying:
                print("Reached ContinuePlaying");
                Time.timeScale = 1f;
                WorldTime.WorldTime.Instance.StartTime();
                break;
            case GameState.Paused:
                print("Reached Pause");
                Time.timeScale = 0f;
                WorldTime.WorldTime.Instance.StopTime();
                break;
            case GameState.GameOver:
                print("Reached GameOver");
                Time.timeScale = 0f;
                WorldTime.WorldTime.Instance.StopTime();
                WorldTime.WorldTime.Instance.ResetTime();
                break;
            case GameState.StartPlaying:
                print("Reached StartPlaying");
                Time.timeScale = 1f;
                WorldTime.WorldTime.Instance.ResetTime();
                WorldTime.WorldTime.Instance.StartTime();
                break;
        }
    }
}