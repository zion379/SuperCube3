using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamDetection : MonoBehaviour
{
    [Range(1,5)]
    public float scale;
    public bool slaming = false;

    public GameObject DetectionTrigger;
    public Rigidbody rigidbody;

    private void Start()
    {
        DetectionTrigger = GameObject.Find("SlamDetectionCylinder");
        DetectionTrigger.transform.localScale = new Vector3(scale, scale, scale);

        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DetectIfEnemyIsInSlamDistance();
    }

    [Range(1,10)]
    public float RaycastDetectionDistance = 5f;
    RaycastHit hit;
    private void DetectIfEnemyIsInSlamDistance() 
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * RaycastDetectionDistance, out hit))
        {
            // check if enemy detected
            if (hit.collider.tag == "Enemy")
            {
                if (slaming)
                {
                    hit.collider.gameObject.GetComponent<Enemies>().DieFromSlam();
                }
            }
         }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * RaycastDetectionDistance, out hit))
        {
            // check if enemy detected
            if (hit.collider.tag == "Enemy")
            {
                if (slaming)
                {
                    hit.collider.gameObject.GetComponent<Enemies>().DieFromSlam();
                }
            }
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * RaycastDetectionDistance, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * RaycastDetectionDistance, Color.red);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        // slaming will be changed from true and false from player script
        if(slaming) 
        {
            if(other.gameObject.tag == "Enemy") 
            {
                other.gameObject.GetComponent<Enemies>().Die();
                Debug.Log("Enemy Died from slam");
             }
        }
    }
    */


    public float BreakPlatformEnergy = 100f;
    // slam break platform
    public void BreakPlatform()
    {

    }

    public float breakVelThreshold = -10f;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.gameObject.layer == 12 && slaming)
        {
           Platform platform = collision.transform.gameObject.GetComponent<Platform>();
            platform.ShatterPlatform();
        }
    }
}
