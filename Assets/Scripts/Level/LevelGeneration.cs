using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject Level;
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
    public GameObject level;



    public void GenerateNewLevel() 
    {
        // generate new level. Using level prefab.  check to make sure level is not null
        // this function is also called from GameManager.
        Vector3 newPosition = new Vector3(0, Y_offset, 0);
        GameObject newlevel = Instantiate(Level, newPosition, Quaternion.identity);


        // get enemy spawn positions
        //GameObject.Find("Level");


        // Also attach new game objects to vars in GameManager

    }

    private void Start()
    {
        GenerateNewLevel();
    }
}
