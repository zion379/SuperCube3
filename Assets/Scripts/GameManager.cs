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
    public Transform EnemyRightSpawn;
    public Transform EnemyMiddleSpawn;
    public Transform EnemyLeftSpawn;
    public Transform PlayerPosRight;
    public Transform PlayerPosMiddle;
    public Transform PlayerPosLeft;
    public Platform leftPlatform;
    public Platform middlePlatform;
    public Platform rightPlatform;

    public bool GameOver = false;

    public enum Difficulty  {easy, ok, hard, crazy, extreme}
    public Difficulty currentDifficulty = Difficulty.easy;

    // Start is called before the first frame update
    void Start()
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
        player.RightPosition = PlayerPosRight;
        player.MiddlePosition = PlayerPosMiddle;
        player.LeftPosition = PlayerPosLeft;

        //assign current level platforms
        rightPlatform = Level.transform.Find("Platforms").transform.Find("Right").transform.Find("RightPlatform").GetComponent<Platform>();
        leftPlatform = Level.transform.Find("Platforms").transform.Find("Left").transform.Find("LeftPlatform").GetComponent<Platform>();
        middlePlatform = Level.transform.Find("Platforms").transform.Find("Middle").transform.Find("MiddlePlatform").GetComponent<Platform>();
    }

    // Update is called once per frame
    void Update()
    {
        // update score
    }

    public bool pauseEnemySpawning = false;

    void FixedUpdate()
    {
    }

    public void playerDied() 
    {
        //Trigger Game Over
    }

    //Get currentPlatforms and also create and reassign gameobjects here.
    public void GetCurrentPlatforms() 
    {
        // get current platform scripts
     }

    // Change level difficulty based on how far player is.

    //setup Level object Assignment for this script and Player script
}
