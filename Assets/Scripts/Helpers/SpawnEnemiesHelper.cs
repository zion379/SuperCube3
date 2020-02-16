using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesHelper : MonoBehaviour
{
    public GameManager gameManger;

    public float EnemySpawnRate = .5f;
    private float SpawnTime; // set in start

    public bool pauseEnemySpawning = false;

    public enum Difficulty { easy, ok, hard, crazy, extreme }
    public Difficulty currentDifficulty = Difficulty.easy;

    void Start()
    {
        gameManger = GetComponent<GameManager>();

        SpawnTime = EnemySpawnRate;
    }

 

    private void FixedUpdate()
    {
        if (!pauseEnemySpawning)
        {
            SpawnDelay();
        }
    }

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

    public bool leftPlatformDowm = false;
    public bool rightPlatformDown = false;
    public bool middlePlatformDown = false;

    //Array of platforms that can  not be used due to being destroyed.
    public List<int> DestroyedPlatforms = new List<int>();

    public void LeftPlatformDown()
    {
        // 1 == left - assigned in RandomlyAssignEnemyPositions
        if (!leftPlatformDowm)
        {
            DestroyedPlatforms.Add(1); // if platform down is equal to false add it to the destroyed platforms array. To prevent adding unnecessary data to  the array
        }
        leftPlatformDowm = true;

    }

    public void RightPlatformDown()
    {
        // 3 == right - assigned in RandomlyAssignEnemyPositions
        if (!rightPlatformDown)
        {
            DestroyedPlatforms.Add(3); // if platform down is equal to false add it to the destroyed platforms array. To prevent adding unnecessary data to  the array
        }
        rightPlatformDown = true;

    }

    public void MiddlePlatformDown()
    {
        // 2 == middle - assigned in RandomlyAssignEnemyPositions
        if (!middlePlatformDown)
        {
            DestroyedPlatforms.Add(2); // if platform down is equal to false add it to the destroyed platforms array. To prevent adding unnecessary data to  the array
        }
        middlePlatformDown = true;

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
        if ( currentDifficulty == Difficulty.easy)
        {
            RandomlyAssignEnemyPositions(1f, 1f);
        }
        else if (currentDifficulty == Difficulty.ok)
        {
            //increase speed
            RandomlyAssignEnemyPositions(2f, 1f);
        }
        else if (currentDifficulty == Difficulty.hard)
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

    public bool TestingSpawnManagement = false;

    public int[] LastTwoSpawnedPositions = new int[2] { 0, 0 }; //keeps track of last two positions
    public GameObject EnemyPrefab;
    private void RandomlyAssignEnemyPositions(float EnemySpeed, float SpawnSpeed)
    {
        // generate new random position 
        int newSpawnPosition = 0;

        int position = Random.Range(1, 4); // 1 == left, 2 == middle, 3 == right

        bool platformDoesNotExist = false;



        /*
        // this does not run unless i turn it on.
        if (TestingSpawnManagement)
        {
            // if last two positions were repeated then change it to new position.
            if (position == LastTwoSpawnedPositions[0] && position == LastTwoSpawnedPositions[1])
            {
                int lastrepeatedpos = LastTwoSpawnedPositions[0];
                int newpos = Random.Range(1, 4);

                //check if randomly generated pos is equal to one of the destroyed positions.
                for (int i = 0; i < DestroyedPlatforms.Count; i++)
                {
                    if (newpos == DestroyedPlatforms[i]) { platformDoesNotExist = true; } else { platformDoesNotExist = false; }
                }

                while (newpos == lastrepeatedpos || platformDoesNotExist) // infinite loop
                {
                    newpos = Random.Range(1, 4);
                }

                // set platform Does not exist to false since we got through number generation
                platformDoesNotExist = false;

                // proceed with assigning spawn pos)
                position = newpos;
            }

            //check if randomly generated pos is equal to one of the destroyed positions. -- second check for this. for not repeated portal  spawns.
            for (int i = 0; i < DestroyedPlatforms.Count; i++)
            {
                if (newSpawnPosition == DestroyedPlatforms[i]) { platformDoesNotExist = true; } else { platformDoesNotExist = false; } // check to see if generated number is equal to  destroyed platform.
                Debug.Log("destroyed platforms = " + platformDoesNotExist);
            }

            //if platformdoesnot exist = true then generate new random number until it does not equal destroyed position.
            while (platformDoesNotExist) // loop until this equals false. -- also dont forget to check if all positions have been destroyed.
            {
                int newpos = Random.Range(1, 4);


                // since we know there are only 3 platforms we should check two positions from the array to see if there destroyed 
                //Check array size
                if (DestroyedPlatforms.Count == 2)
                {
                    if (!(newpos == DestroyedPlatforms[0]) && !(newpos == DestroyedPlatforms[1]))
                    {
                        // if new pos is not equal both platforms pos in destroyed list set platformDoesnotExist to false to exit loop.
                        platformDoesNotExist = false;
                        position = newpos;
                    }
                }
                else if (DestroyedPlatforms.Count == 1)
                {
                    if (newpos != DestroyedPlatforms[0]) { platformDoesNotExist = false;  position = newpos; }
                }
            }
        }
        */


        newSpawnPosition = position;
        Debug.Log(newSpawnPosition);

        // assign and spawn
        if (newSpawnPosition == 1)
        {
            UpdateLastPosition(1);
            GameObject Instatiated = Instantiate(EnemyPrefab, gameManger.EnemyLeftSpawn, false);
            // set enemy portal origin
            Instatiated.gameObject.GetComponent<Enemies>().portalOrigin = Enemies.PortalOrigins.left;
        }
        else if (newSpawnPosition == 2)
        {
            UpdateLastPosition(2);
            GameObject Instatiated = Instantiate(EnemyPrefab, gameManger.EnemyMiddleSpawn, false);
            // set enemy portal origin
            Instatiated.gameObject.GetComponent<Enemies>().portalOrigin = Enemies.PortalOrigins.middle;
        }
        else if (newSpawnPosition == 3)
        {
            UpdateLastPosition(3);
            GameObject Instatiated = Instantiate(EnemyPrefab, gameManger.EnemyRightSpawn, false);
            // set enemy portal origin
            Instatiated.gameObject.GetComponent<Enemies>().portalOrigin = Enemies.PortalOrigins.right;
        }
    }

    private int posCounter = 0;
    private void UpdateLastPosition(int pos)
    {
        posCounter += 1;
        if (posCounter == 3)
        {
            posCounter = 1;
        }

        if (posCounter == 1)
        {
            LastTwoSpawnedPositions[0] = pos;
        }
        else if (posCounter == 2)
        {
            LastTwoSpawnedPositions[1] = pos;
        }
    }

    private bool CheckIfPlatformIsSpawnable(int pos)
    {
        for (int i = 0; i < DestroyedPlatforms.Count; i++)
        {
            if (pos == DestroyedPlatforms[i])
            {
                return false;
            }
        }

        // if everything ran through the number is good to go
        return true;
    }

    //Get currentPlatforms and also create and reassign gameobjects here.
    public void GetCurrentPlatforms()
    {
        // get current platform scripts
    }


}
