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

        SpawnTime = EnemySpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        // update score
    }

    public bool pauseEnemySpawning = false;

    void FixedUpdate()
    {
        if(!pauseEnemySpawning) 
        {
            SpawnDelay();
        }
    }

    public float EnemySpawnRate = .5f;

    private float SpawnTime; // set in start
    private void SpawnDelay() 
    {
        // delay spwan
        if (SpawnTime <= 0)
        {
            SpawnEnemies();
            SpawnTime = EnemySpawnRate;
        }
        else
        {
            SpawnTime -= Time.deltaTime;
        }
    }

    public void playerDied() 
    {
        //Trigger Game Over
    }

    public bool leftPlatformDowm = false;
    public bool rightPlatformDown = false;
    public bool middlePlatformDown = false;

    //Array of platforms that can  not be used due to being destroyed.
    public List<int> DestroyedPlatforms = new List<int>();

    public void LeftPlatformDown() 
    {
        // 1 == left - assigned in RandomlyAssignEnemyPositions
        leftPlatformDowm = true;
        DestroyedPlatforms.Add(1);
     }

    public void RightPlatformDown() 
    {
        // 3 == right - assigned in RandomlyAssignEnemyPositions
        rightPlatformDown = true;
        DestroyedPlatforms.Add(3);
    }

    public void MiddlePlatformDown() 
    {
        // 2 == middle - assigned in RandomlyAssignEnemyPositions
        middlePlatformDown = true;
        DestroyedPlatforms.Add(2);
    }

     // resets all platforms that were set to down due to deletion from explosion
    public void ResetDownPlatforms()
    {
        leftPlatformDowm = false;
        rightPlatformDown = false;
        middlePlatformDown = false;
    }

    // change the difficulty settings with varibles -- when possible
    private void SpawnEnemies() 
    {
        // take in account of level difficulty
        if(currentDifficulty == Difficulty.easy) 
        {
            RandomlyAssignEnemyPositions(1f, 1f);
         }
        else if (currentDifficulty == Difficulty.ok) 
        {
            //increase speed
            RandomlyAssignEnemyPositions(2f, 1f);
        }
        else if(currentDifficulty == Difficulty.hard) 
        {
            //increase speed
            RandomlyAssignEnemyPositions(3f, 1f);
        }
        else if (currentDifficulty == Difficulty.crazy)
        {
            //increase speed
            RandomlyAssignEnemyPositions(4f, 1f);
        }
        else if (currentDifficulty == Difficulty.extreme) 
        {
            //increase speed
            RandomlyAssignEnemyPositions(5f, 1f);
        }
    }

    public int[] LastTwoSpawnedPositions = new int[2] { 0, 0 }; //keeps track of last two positions
    public GameObject EnemyPrefab;
    private void RandomlyAssignEnemyPositions(float EnemySpeed, float SpawnSpeed)
    {
        int newSpawnPosition = 0;

        int position = Random.RandomRange(1, 4); // 1 == left, 2 == middle, 3 == right

        bool platformDoesNotExist = false;

        if (position == LastTwoSpawnedPositions[0] && position == LastTwoSpawnedPositions[1])
        {
            int lastrepeatedpos = LastTwoSpawnedPositions[0];
            int newpos = Random.RandomRange(1, 4);

            //check if randomly generated pos is equal to one of the destroyed positions.
            for(int i = 0; i < DestroyedPlatforms.Count; i++) 
            {
                if(newpos == DestroyedPlatforms[i]){ platformDoesNotExist = true; } else { platformDoesNotExist = false; }
            }

            while (newpos == lastrepeatedpos || platformDoesNotExist)
            {
                newpos = Random.RandomRange(1, 4);
                // add in no spawning for platforms that have exploded.
            }

            // set platform Does not exist to true since we got through number generation
            platformDoesNotExist = false;

            // proceed with assigning spawn pos)
            newSpawnPosition = newpos;
        }

        newSpawnPosition = position;
        Debug.Log(newSpawnPosition);

        // assign and spawn
        if (newSpawnPosition == 1)
        {
            GameObject Instatiated = Instantiate(EnemyPrefab, EnemyLeftSpawn, false);
            // set enemy portal origin
            Instatiated.gameObject.GetComponent<Enemies>().portalOrigin = Enemies.PortalOrigins.left;
        }
        else if (newSpawnPosition == 2)
        {
            GameObject Instatiated = Instantiate(EnemyPrefab, EnemyMiddleSpawn, false);
            // set enemy portal origin
            Instatiated.gameObject.GetComponent<Enemies>().portalOrigin = Enemies.PortalOrigins.middle;
        }
        else if (newSpawnPosition == 3)
        {
            GameObject Instatiated = Instantiate(EnemyPrefab, EnemyRightSpawn, false);
            // set enemy portal origin
            Instatiated.gameObject.GetComponent<Enemies>().portalOrigin = Enemies.PortalOrigins.right;
        }
    }

    //Get currentPlatforms and also create and reassign gameobjects here.
    public void GetCurrentPlatforms() 
    {
        // get current platform scripts
     }

    // Change level difficulty based on how far player is.

    //setup Level object Assignment for this script and Player script
}
