using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChickenSpawnerScript : MonoBehaviour
{
    private Transform[] spawners;
    private float time;
    private readonly List<GameObject> existingChickens = new();

    [SerializeField] private GameLogicScript gameLogicScript;

    [SerializeField] private GameObject[] chickens;

    // Start is called before the first frame update
    void Start()
    {
        // get spawners
        if (gameObject.transform.childCount != 0)
        {
            spawners = new Transform[gameObject.transform.childCount];
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                spawners[i] = gameObject.transform.GetChild(i);
            }
        }
        else
        {
            Debug.LogError("Error: Couldn't found the spawner (Needs atleast 1)!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if there aren't any chickens
        if (chickens.Length == 0)
            return;

        // increase timer
        time += Time.deltaTime;

        // check if timer hits the spawning Interval
        if (time >= gameLogicScript.SpawningInterval)
        {
            SpawnChicken();
            time = 0.0f;            // reset timer
        }
    }

    private void SpawnChicken()
    {
        // increase sorting order for existing chickens
        foreach (GameObject chicken in existingChickens)
        {
            if (chicken.TryGetComponent<SpriteRenderer>(out var sr))
                sr.sortingOrder++;
        }

        // get a random chicken
        int chickenIndex = UnityEngine.Random.Range(0, chickens.Length);
        
        // get a random spawner
        Transform spawnLocation = spawners[UnityEngine.Random.Range(0, spawners.Length)];

        // spawn chicken
        GameObject newChicken = Instantiate(chickens[chickenIndex], spawnLocation.position, Quaternion.identity);

        // set sorting order to the lowest value
        if (newChicken.TryGetComponent<SpriteRenderer>(out var newSr))
            newSr.sortingOrder = 0;

        // add chicken to the list of existing chickens
        existingChickens.Add(newChicken);
    }
}
