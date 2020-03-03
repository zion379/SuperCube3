using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject prefabLevel;
    public float Y_offset = 10f;

    // enemy spawn
    public Transform enemyRightSpawn;
    public Transform enemyMiddleSpawn;
    public Transform enemyLeftSpawn;
    // Player pos
    public Transform playerPosRight;
    public Transform playerPosMiddle;
    public Transform playerPosLeft;
    // Platforms
    public Platform leftPlatform;
    public Platform middlePlatform;
    public Platform rightPlatform;
    // Portals
    public Portal leftPortal;
    public Portal middlePortal;
    public Portal rightPortal;
    // Level
    public GameObject generatedLevel;

    public int levelCounter = 0;

    public GameManager gameManager;

    public GameLogic gameLogic;

    public SpawnEnemiesHelper spawnEnemiesHelper;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        //GenerateNewLevel(); // on run this in start if testing.

        gameLogic = GetComponent<GameLogic>();

        spawnEnemiesHelper = GetComponent<SpawnEnemiesHelper>();
    }

    public void GenerateNewLevel() 
    {
        if (!gameLogic.primedNewLevel)
        {
            PrimeNewLevel();
            gameLogic.primedNewLevel = false;
        }
        // pause enemy spawnning
        spawnEnemiesHelper.pauseEnemySpawning = true; // this will be resumed by trigger that player has droped to the next level
        AssignValues();
        
    }

    public void UpdateLevelCounter()
    {
        levelCounter += 1; // this will update from gameManager.
        gameManager.currentLevel = levelCounter;
        gameManager.ManageHowManyLevelsAreInScene();
    }

    public void PrimeNewLevel()
    {
        // UpdateLevelCounter
        UpdateLevelCounter();

        

        // generate new level. Using level prefab.  check to make sure level is not null
        // this function is also called from GameManager.
        Vector3 newPosition = new Vector3(0, Y_offset * levelCounter, 0);
        GameObject newlevel = Instantiate(prefabLevel, newPosition, Quaternion.identity);
        string levelName = "Level" + levelCounter.ToString();
        newlevel.gameObject.name = levelName;

        // get enemy spawn positions
        generatedLevel = GameObject.Find(levelName);
        enemyRightSpawn = generatedLevel.transform.Find("Platforms").transform.Find("Right").transform.Find("EnemySpawn");
        enemyMiddleSpawn = generatedLevel.transform.Find("Platforms").transform.Find("Middle").transform.Find("EnemySpawn");
        enemyLeftSpawn = generatedLevel.transform.Find("Platforms").transform.Find("Left").transform.Find("EnemySpawn");

        //get player move positions
        playerPosRight = generatedLevel.transform.Find("Platforms").transform.Find("Right").transform.Find("PlayerPosition");
        playerPosMiddle = generatedLevel.transform.Find("Platforms").transform.Find("Middle").transform.Find("PlayerPosition");
        playerPosLeft = generatedLevel.transform.Find("Platforms").transform.Find("Left").transform.Find("PlayerPosition");
    }

    public void AssignValues()
    {
        //Setup player positions
        gameManager.player.RightPosition = playerPosRight;
        gameManager.player.MiddlePosition = playerPosMiddle;
        gameManager.player.LeftPosition = playerPosLeft;

        //assign current platforms
        rightPlatform = generatedLevel.transform.Find("Platforms").transform.Find("Right").transform.Find("RightPlatform").GetComponent<Platform>();
        leftPlatform = generatedLevel.transform.Find("Platforms").transform.Find("Left").transform.Find("LeftPlatform").GetComponent<Platform>();
        middlePlatform = generatedLevel.transform.Find("Platforms").transform.Find("Middle").transform.Find("MiddlePlatform").GetComponent<Platform>();

        //Get Portals
        leftPortal = generatedLevel.transform.Find("Platforms").transform.Find("Left").transform.Find("Portal").GetComponent<Portal>();
        middlePortal = generatedLevel.transform.Find("Platforms").transform.Find("Middle").transform.Find("Portal").GetComponent<Portal>();
        rightPortal = generatedLevel.transform.Find("Platforms").transform.Find("Right").transform.Find("Portal").GetComponent<Portal>();


        // Also attach new game objects to vars in GameManager
        gameManager.EnemyRightSpawn = enemyRightSpawn;
        gameManager.EnemyMiddleSpawn = enemyMiddleSpawn;
        gameManager.EnemyLeftSpawn = enemyLeftSpawn;

        gameManager.PlayerPosRight = playerPosRight;
        gameManager.PlayerPosMiddle = playerPosMiddle;
        gameManager.PlayerPosLeft = playerPosLeft;

        gameManager.rightPlatform = rightPlatform;
        gameManager.leftPlatform = leftPlatform;
        gameManager.middlePlatform = middlePlatform;

        gameManager.leftPortal = leftPortal;
        gameManager.middlePortal = middlePortal;
        gameManager.rightPortal = rightPortal;

        // reset Portals down
        gameLogic.ResetPortalsDown();

        //reset down platforms
        spawnEnemiesHelper.ResetDownPlatforms();
    }

}
