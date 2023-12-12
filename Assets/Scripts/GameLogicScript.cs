using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class GameLogicScript : MonoBehaviour
{
    public static bool isGameOver;
    public static int score;

    // private variables
    [SerializeField] private float spawningInterval = 4f;
    public float SpawningInterval => spawningInterval;

    [SerializeField] private int level = 0;
    public float Level => level;

    [SerializeField] public float lifeTimeChicken;
    [SerializeField] public float closeToDying;
    [SerializeField] public float flashAmount;



    // transfer number of Flashes
    public float FlashAmount
    {
        get { return flashAmount;  }
    }
    
    // public methods
    public void GameOver(string customMessage = "No custom message was provided.")
    {
        // implement game over animation & screen right here

        // PLACEHOLDER
        Debug.Log($"Game Over: {customMessage}");
        isGameOver = true;
    }

}
