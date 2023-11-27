using UnityEditor;
using UnityEngine;

public class GameLogicScript : MonoBehaviour
{
    // private variables
    [SerializeField] private float spawningInterval = 4f;
    public float SpawningInterval => spawningInterval;

    [SerializeField] private int level = 0;
    public float Level => level;

    [SerializeField] private int score = 0;
    public int Score => score;

    [SerializeField] public float lifeTimeChicken = 100f;

    // public methods
    public void GameOver(string customMessage = "No custom message was provided.")
    {
        // implement game over animation & screen right here

        // PLACEHOLDER
        Debug.Log($"Game Over: {customMessage}");
        EditorApplication.isPlaying = false;
    }

}
