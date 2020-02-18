using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePortalsHelper : MonoBehaviour
{
    public Portal leftPortal;
    public Portal middlePortal;
    public Portal rightPortal;

    public GameManager gameManger;
    public SpawnEnemiesHelper spawnEnemiesHelper;

    private void Start()
    {
        gameManger = GetComponent<GameManager>();
        spawnEnemiesHelper = GetComponent<SpawnEnemiesHelper>();

        leftPortal = gameManger.leftPortal;
        middlePortal = gameManger.middlePortal;
        rightPortal = gameManger.rightPortal;
    }

    public void UpdateNewPortals()
    {
        // when new level is generated assign new portals here.
    }

    //check is platforms exist
    public bool LeftPortalExist()
    {
        if (leftPortal == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool MiddlePortalExist()
    {
        if (middlePortal == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool RightPortalExist()
    {
        if (rightPortal == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //make portals down. And prevent repetitive array entries
    public bool leftPortalDown = false;
    public bool middlePortalDown = false;
    public bool rightPortalDown = false;

    // 1== left 2 == middle 3 == right
    public void LeftPortalDown()
    {
        if (!leftPortalDown)
        {
            spawnEnemiesHelper.DestroyedPortals.Add(1);
        }
        leftPortalDown = true;
    }

    public void MiiddlePortalDown()
    {
        if (!middlePortalDown)
        {
            spawnEnemiesHelper.DestroyedPortals.Add(2);
        }
        middlePortalDown = true;
    }

    public void RightPortalDown()
    {
        if (!rightPortalDown)
        {
            spawnEnemiesHelper.DestroyedPortals.Add(3);
        }
        rightPortalDown = true;
    }

    public void resetPortals()
    {
        leftPortalDown = false;
        middlePortalDown = false;
        rightPortalDown = false;
        // Reset array aswell which lives in teh spawn Enemies Helper
        spawnEnemiesHelper.DestroyedPortals.Clear();
    }
}
