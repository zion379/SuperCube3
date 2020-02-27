using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public enum PortalPos {left, middle, right}
    public PortalPos Position = PortalPos.middle;

    public ManagePortalsHelper managePortalsHelper;
    public PointSystem pointSystem;

    private void Start()
    {
        managePortalsHelper = GameObject.Find("GameManager").GetComponent<ManagePortalsHelper>();
        pointSystem = GameObject.Find("GameManager").GetComponent<PointSystem>();
    }

    public void DestroyPortal()
    {
        switch (Position)
        {
            case PortalPos.left :
                managePortalsHelper.LeftPortalDown();
                break;
            case PortalPos.middle:
                managePortalsHelper.MiiddlePortalDown();
                break;
            case PortalPos.right:
                managePortalsHelper.RightPortalDown();
                break;
        }

        //Increase Player score
        pointSystem.AddPortalDestroyedPoints();
        // Add in destroy animation
        // Add in destroy Sound Effects
        Destroy(this.gameObject, 1f);
    }
}
