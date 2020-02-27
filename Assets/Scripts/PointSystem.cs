using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public int portalDestroyPoints = 10;
    public int levelCompletionPoints = 10;

    public int playerScore = 0;

    public void IncreaseScore(int points)
    {
        playerScore += points;
    }

    public void AddPortalDestroyedPoints()
    {
        playerScore += portalDestroyPoints;
    }

    public void AddLevelCompletionPoints()
    {
        playerScore += levelCompletionPoints;
    }
}
