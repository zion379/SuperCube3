using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    public GameManager gameManager;
    public float platformHealth = 100f;

    public enum platforms {left, right, middle}
    public platforms platform = platforms.middle;

    public SpawnEnemiesHelper spawnEnemiesHelper;

    public MeshRenderer rightMeshRenderer;
    public BoxCollider rightBoxCollider;

    public MeshRenderer leftMeshRenderer;
    public BoxCollider leftBoxCollider;

    public GameObject ShatteredPlatform;

    public GameObject rightSectionPlatform;
    public GameObject leftSectionPlatform;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnEnemiesHelper = GameObject.Find("GameManager").GetComponent<SpawnEnemiesHelper>();
        rightMeshRenderer = transform.Find("Right-Side").GetComponent<MeshRenderer>();
        rightBoxCollider = transform.Find("Right-Side").GetComponent<BoxCollider>();
        leftMeshRenderer = transform.Find("Left-Side").GetComponent<MeshRenderer>();
        leftBoxCollider = transform.Find("Left-Side").GetComponent<BoxCollider>();

        rightSectionPlatform = transform.Find("Right-Side").gameObject;
        leftSectionPlatform = transform.Find("Left-Side").gameObject;

        DOTween.Init();

        pulseTime = 0;
    }

    public void PlatformTakeDamage(float Damage) 
    {
        platformHealth -= Damage;
        if(platformHealth <= 0) 
        {
            //play destroy animation - and disable portal from spawning.
            //let game manager know that the platform should not be used to spawn.
            if(platform == platforms.left) 
            {
                spawnEnemiesHelper.LeftPlatformDown();
             } 
             else if(platform == platforms.middle) 
             {
                spawnEnemiesHelper.MiddlePlatformDown();
              }
            else if(platform == platforms.right) 
            {
                spawnEnemiesHelper.RightPlatformDown();
             }
            Destroy(this.gameObject);
         }
    }

    public void ShatterPlatform()
    {
        // turn off mesh render && turn of box collider or turn trigger on
        rightMeshRenderer.enabled = false;
        rightBoxCollider.enabled = false;
        leftMeshRenderer.enabled = false;
        leftBoxCollider.enabled = false;
        // instantiate shattered version
        Instantiate(ShatteredPlatform, this.transform.position, Quaternion.identity);
        Debug.Log("platform");
    }

    // test.
    public bool Triggerbreak = false;
    public bool TriggerPulse = false;

     void Update()
    {
        if (Triggerbreak)
        {
            ShatterPlatform();
            Triggerbreak = false;
        }

        if (TriggerPulse)
        {
            PulsePlatformOpening();
        }
    }

    [Range(1,2)]
    public float platformOpeningDistance = 1f;
    [Range(1, 10)]
    public float platformOpeningDuration = 1f;

    public void OpenPlatform()
    {
        rightSectionPlatform.transform.DOMoveX(rightSectionPlatform.transform.position.x + platformOpeningDistance, platformOpeningDuration);
        leftSectionPlatform.transform.DOMoveX(leftSectionPlatform.transform.position.x + (-platformOpeningDistance), platformOpeningDuration);
    }

    public void ClosePlatform()
    {
        rightSectionPlatform.transform.DOMoveX(rightSectionPlatform.transform.position.x + (-platformOpeningDistance), platformOpeningDuration);
        leftSectionPlatform.transform.DOMoveX(leftSectionPlatform.transform.position.x + platformOpeningDistance, platformOpeningDuration);
    }

    [Range(1, 5)]
    public float PulseFrequency = 2f;
    public float pulseTime; // set in start -- temp made public
    private bool platformOpened = false;
    public void PulsePlatformOpening()
    {
        if (pulseTime <= 0)
        {
            if (!platformOpened)
            {
                // open
                OpenPlatform();
                platformOpened = true;
            }
            else
            {
                // close
                ClosePlatform();
                platformOpened = false;
            }
            pulseTime = PulseFrequency;
        }
        else
        {
            pulseTime -= Time.deltaTime;
        }
    }
}
