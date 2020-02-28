using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool playerDead = false;
    public int playerScore = 0; // updated from other scripts

    public Player player;

    public GameObject[] enemies;

    //Level objects
    public GameObject Level;
    //Enemy Spawn Positions
    public Transform EnemyRightSpawn;
    public Transform EnemyMiddleSpawn;
    public Transform EnemyLeftSpawn;
    //Player Positions
    public Transform PlayerPosRight;
    public Transform PlayerPosMiddle;
    public Transform PlayerPosLeft;
    //Platforms
    public Platform leftPlatform;
    public Platform middlePlatform;
    public Platform rightPlatform;
    // Portals
    public Portal leftPortal;
    public Portal middlePortal;
    public Portal rightPortal;


    public bool gameOver = false;

    public enum Difficulty  {easy, ok, hard, crazy, extreme}
    public Difficulty currentDifficulty = Difficulty.easy;

    public LevelGeneration levelGeneration; // this has all of the functions for generating levels.

    // Start is called before the first frame update
    void Awake()
    {
        // Set up level stuff. 
        //Enemy spawn
        Level = GameObject.Find("Level");
        EnemyRightSpawn = Level.transform.Find("Platforms").transform.Find("Right").transform.Find("EnemySpawn");
        EnemyMiddleSpawn = Level.transform.Find("Platforms").transform.Find("Middle").transform.Find("EnemySpawn");
        EnemyLeftSpawn = Level.transform.Find("Platforms").transform.Find("Left").transform.Find("EnemySpawn");


        //Player move positions
        PlayerPosRight = Level.transform.Find("Platforms").transform.Find("Right").transform.Find("PlayerPosition");
        PlayerPosMiddle = Level.transform.Find("Platforms").transform.Find("Middle").transform.Find("PlayerPosition");
        PlayerPosLeft = Level.transform.Find("Platforms").transform.Find("Left").transform.Find("PlayerPosition");


        //Setup Player
        player = GameObject.Find("Player").GetComponent<Player>();
        // get player positions.
        player.RightPosition = PlayerPosRight;
        player.MiddlePosition = PlayerPosMiddle;
        player.LeftPosition = PlayerPosLeft;

        //assign current level platforms
        rightPlatform = Level.transform.Find("Platforms").transform.Find("Right").transform.Find("RightPlatform").GetComponent<Platform>();
        leftPlatform = Level.transform.Find("Platforms").transform.Find("Left").transform.Find("LeftPlatform").GetComponent<Platform>();
        middlePlatform = Level.transform.Find("Platforms").transform.Find("Middle").transform.Find("MiddlePlatform").GetComponent<Platform>();

        //Get Portals
        leftPortal = Level.transform.Find("Platforms").transform.Find("Left").transform.Find("Portal").GetComponent<Portal>();
        middlePortal = Level.transform.Find("Platforms").transform.Find("Middle").transform.Find("Portal").GetComponent<Portal>();
        rightPortal = Level.transform.Find("Platforms").transform.Find("Right").transform.Find("Portal").GetComponent<Portal>();

        // get Level Generation script
        levelGeneration = GetComponent<LevelGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        // update score
    }

    void FixedUpdate()
    {
    }

    public void playerDied() 
    {
        //Trigger Game Over
        playerDead = true;
    }

    public void GameOverFromDestroyedPlatforms()
    {
        if(!gameOver)
        {
            // this is called from GameLogic.
            Debug.Log("Game Over from destroyed platforms");
            // update ui 
            // load up game over menu.
            // camera should no longer follow player -- let player fall off screen.
            gameOver = true;
        }
    }

    // reset gameOver
    public void ResetGameOver()
    {
        gameOver = false;
        playerDead = false;
    }

    public void GameOver()
    {
        /// set in other scripts that game is opver and bring up gameover ui.
    }

    public void DropPlayerToNextLevel()
    {
        // generate new level and play platform animation.
        levelGeneration.GenerateNewLevel();
    }

    //Get currentPlatforms and also create and reassign gameobjects here.
    public void GetCurrentPlatforms() 
    {
        // get current platform scripts
     }

    // Change level difficulty based on how far player is.

    //setup Level object Assignment for this script and Player script
}
