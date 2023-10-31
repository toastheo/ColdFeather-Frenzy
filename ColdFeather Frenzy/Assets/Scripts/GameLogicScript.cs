using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicScript : MonoBehaviour
{
    // public variables
    public bool gameOver = false;

    // private variables
    [SerializeField] private float spawningInterval = 4f;
    public float SpawningInterval => spawningInterval;

    [SerializeField] private int level = 0;
    public float Level => level;

    [SerializeField] private int score = 0;
    public int Score => score;

}
