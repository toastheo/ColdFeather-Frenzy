using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicScript : MonoBehaviour
{
    [SerializeField]
    private float spawningInterval = 4f;
    public float SpawningInterval
    {
        get { return spawningInterval; }
    }
}
