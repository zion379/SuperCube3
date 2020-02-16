using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameManager gameManager;
    public float platformHealth = 100f;

    public enum platforms {left, right, middle}
    public platforms platform = platforms.middle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                gameManager.LeftPlatformDown();
             } 
             else if(platform == platforms.middle) 
             {
                gameManager.MiddlePlatformDown();
              }
            else if(platform == platforms.right) 
            {
                gameManager.RightPlatformDown();
             }
            Destroy(this.gameObject);
         }
    }
}
