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

    //keep track of destroyed portals
    public List<int> DestroyedPortals = new List<int>();

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

    // resets all platforms that were set to down due to deletion from explosion // this is going to be called by the game manager.
    public void ResetDownPlatforms()
    {
        leftPlatformDowm = false;
        rightPlatformDown = false;
        middlePlatformDown = false;
        // reset Array aswell.
        DestroyedPlatforms.Clear();
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

    public int[] LastTwoSpawnedPositions = new int[2] { 0, 0 }; //keeps track of last two positions
    public GameObject EnemyPrefab;
    private void RandomlyAssignEnemyPositions(float EnemySpeed, float SpawnSpeed)
    {   
        // check to make sure all platforms are not down. And portals...
        if(!(DestroyedPlatforms.Count >= 3) && !(DestroyedPortals.Count >= 3)) 
        {
            // generate new random position 
            int newSpawnPosition = 0;

            int position = Random.Range(1, 4); // 1 == left, 2 == middle, 3 == right

            if (position == LastTwoSpawnedPositions[0] && position == LastTwoSpawnedPositions[1])
            {
                Debug.Log("repeat");
                int lastrepeatedpos = LastTwoSpawnedPositions[0];
                int newpos = Random.Range(1, 4);

                if (CheckIfPlatformIsSpawnable(newpos) || CheckIfPortalIsSpawnable(newpos))
                {
                    position = newpos;
                }
                else
                {
                    // now check if choosen number is a destoyed platform. Or destroyed portal
                    while (!CheckIfPlatformIsSpawnable(newpos) || !CheckIfPortalIsSpawnable(newpos))
                    {
                        newpos = Random.Range(1, 4);
                        Debug.Log("In generated loop");
                    }
                    // set position
                    position = newpos;
                }
            }
            else
            {
                Debug.Log("position was not repeated");
                // number was not repeated

                // now check if choosen number is a destoyed platform.
                if (!CheckIfPlatformIsSpawnable(position) || !CheckIfPortalIsSpawnable(position))
                {
                    while (!CheckIfPlatformIsSpawnable(position) || !CheckIfPortalIsSpawnable(position))
                    {
                        position = Random.Range(1, 4); // run until number does not equal destroyed platform
                    }
                }
            }

            newSpawnPosition = position;

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

    private bool CheckIfPortalIsSpawnable(int pos)
    {
        Debug.Log(DestroyedPortals.Count);
        for (int i = 0; i < DestroyedPortals.Count; i++)
        {
            Debug.Log("pos : " + pos + " Destroyed portal : " + DestroyedPortals[i]);
            if (pos == DestroyedPortals[i])
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
