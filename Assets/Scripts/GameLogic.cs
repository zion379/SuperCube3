using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameLogic : MonoBehaviour
{
    public int NeededEnemyKills = 10;
    public int KilledEnemies = 0;

    public SpawnEnemiesHelper spawnEnemiesHelper;
    public GameManager gameManager;
    public LevelGeneration levelGeneration;

    private void Start()
    {
        spawnEnemiesHelper =  GetComponent<SpawnEnemiesHelper>();
        gameManager = GetComponent<GameManager>();
        levelGeneration = GetComponent<LevelGeneration>();
    }

    private void Update()
    {
        PortalLogic();
        PlatformLogic();
        CheckPlayerEnergy();
        updateSlamUI();

    }

    // Portal Logic
    bool leftPortalDown = false;
    bool rightPortalDown = false;
    bool middlePortalDown = false;
    bool awareOfWhichPortalisLeft = false;
    bool triggeredLevelGeneration = false;
    public void PortalLogic()
    {
        //get which portals are down.
        // 1 == left, 2 == middle, 3 == right
        for (int i = 0; i < spawnEnemiesHelper.DestroyedPortals.Count; i++)
        {
            if (spawnEnemiesHelper.DestroyedPortals[i] == 1)
            {
                leftPortalDown = true;
            }

            switch (spawnEnemiesHelper.DestroyedPortals[i])
            {
                case 1:
                    leftPortalDown = true;
                    break;
                case 2:
                    middlePortalDown = true;
                    break;
                case 3:
                    rightPortalDown = true;
                        break;
            }
        }

        // check for 2/3 portals down
        if (spawnEnemiesHelper.DestroyedPortals.Count == 2 && !awareOfWhichPortalisLeft)
        {
            if (leftPortalDown && rightPortalDown)
            {
                // player has to destroy middle portal
                Debug.Log("player has to destroy middle portal");
            }
            else if (leftPortalDown && middlePortalDown)
            {
                // player has to destroy right portal
                Debug.Log("player has to destroy right portal");
            }
            else if (rightPortalDown && middlePortalDown)
            {
                // player has to destroy left portal
                Debug.Log("player has to destroy left portal");
            }
            awareOfWhichPortalisLeft = true; // this prevents this portion from running more than it needs to.
        }

        // check to see if all portals are down
        if (spawnEnemiesHelper.DestroyedPortals.Count == 3 && !triggeredLevelGeneration)
        {
            // trigger platform drop && check which platform the player is on.
            Debug.Log("trigger platform drop");
            gameManager.DropPlayerToNextLevel();
            triggeredLevelGeneration = true;
        }
    }

    // this will reset from Level Generation.
    public void ResetPortalsDown()
    {
        leftPortalDown = false;
        rightPortalDown = false;
        middlePortalDown = false;
        awareOfWhichPortalisLeft = false;
        triggeredLevelGeneration = false;
    }

    // Platform Logic
    public void PlatformLogic()
    {
        // check to see if all platforms are down.
        if(spawnEnemiesHelper.DestroyedPlatforms.Count == 3)
        {
            // trigger game over
            gameManager.GameOverFromDestroyedPlatforms();
        }
    }

    public void KeepTrackOfKilledEnemies()
    {
        KilledEnemies += 1;
        if (KilledEnemies >= NeededEnemyKills)
        {
            gameManager.DropPlayerToNextLevel();
            //reset enemy kills
            KilledEnemies = 0;
        } 
    }

    [Range(0, 100)]
    public float slamEnergy = 0f;

    //Player Energy Functionalities
    public void IncreasePlayerSlamEnergy(float energy)
    {
        slamEnergy += energy;
    }

    public void ResetPlayerSlamEnergy()
    {
        slamEnergy = 0f;
    }

    public bool primedNewLevel = false;
    public void CheckPlayerEnergy()
    {
        if (slamEnergy >= 100f && !primedNewLevel)
        {
            // generate new level (prime new level)
            levelGeneration.PrimeNewLevel();
            // notify level generation that a new level has been primed.
            gameManager.SafeToBreakPlatforms();
            primedNewLevel = true; // this reset player enters next level. by trigger.
            Debug.Log("primed new level");
            ResetPlayerSlamEnergy();
        }
    }

    public float SlamMeterHeight = 60f;
    public RectTransform Meter;
    public void updateSlamUI()
    {
        // get ratio
        float newHeightRatio = slamEnergy / 100;
        Meter.DOScaleX(SlamMeterHeight * newHeightRatio, 1f);
    }
}

// testing can delete.