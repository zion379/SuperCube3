using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBreakPoint : MonoBehaviour
{
    [Range(100f,500f)]
    public float Health = 100f;
    public enum PortalPosition {left,middle,right }
    public PortalPosition currentPortalPosition = PortalPosition.middle;

    public Portal portal;

    private void Start()
    {
        portal = this.gameObject.transform.parent.transform.Find("Portal").GetComponent<Portal>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Health -= other.gameObject.GetComponent<Bullet>().damage; // subtract damage from health
            if (Health <= 0)
            {
                portal.DestroyPortal();
                Destroy(this.gameObject, 1f);
            }
        }
    }
}
