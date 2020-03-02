using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    public SpawnEnemiesHelper spawnEnemiesHelper;
    public LevelGeneration levelGeneration;
    public GameLogic gameLogic;

    private void Start()
    {
        spawnEnemiesHelper = GameObject.Find("GameManager").GetComponent<SpawnEnemiesHelper>();
        levelGeneration = GameObject.Find("GameManager").GetComponent<LevelGeneration>();
        gameLogic = GameObject.Find("GameManager").GetComponent<GameLogic>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            levelGeneration.AssignValues();
            spawnEnemiesHelper.pauseEnemySpawning = false;
            gameLogic.primedNewLevel = false;
            Debug.Log("player hit Next Level Trigger");
        }
    }
}
