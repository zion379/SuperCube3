using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBreakPoint : MonoBehaviour
{
    public enum PortalPosition {left,middle,right }
    public PortalPosition currentPortalPosition = PortalPosition.middle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Destroy Portal");
        }
    }
}
