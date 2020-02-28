using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameManager gameManager;
    public float platformHealth = 100f;

    public enum platforms {left, right, middle}
    public platforms platform = platforms.middle;

    public SpawnEnemiesHelper spawnEnemiesHelper;

    public MeshRenderer meshRenderer;
    public BoxCollider boxCollider;

    public GameObject ShatteredPlatform;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnEnemiesHelper = GameObject.Find("GameManager").GetComponent<SpawnEnemiesHelper>();
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
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
        meshRenderer.enabled = false;
        boxCollider.isTrigger = true;
        // instantiate shattered version
        Instantiate(ShatteredPlatform, this.transform.position, Quaternion.identity);
        Debug.Log("platform");
    }

    public bool Triggerbreak = false;
     void Update()
    {
        if (Triggerbreak)
        {
            ShatterPlatform();
            Triggerbreak = false;
        }
    }
}
