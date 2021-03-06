﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfPlatFormCollider : MonoBehaviour
{
    public Rigidbody rigidbody;
    public GameManager gameManager;
    // get access to platforms - set from game manager.

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //check to see if enemy belongs to current level.
            if(collision.gameObject.GetComponent<Enemies>().EnemyBelongsInCurrentLevel())
            {
                //Subtract players score or trigger platform explode.
                // check enemy orgin portal to see which platform to destroy.
                if (collision.gameObject.GetComponent<Enemies>().portalOrigin == Enemies.PortalOrigins.left && gameManager.leftPlatform != null)
                {
                    gameManager.leftPlatform.PlatformTakeDamage(100f);
                }
                else if (collision.gameObject.GetComponent<Enemies>().portalOrigin == Enemies.PortalOrigins.middle && gameManager.middlePlatform != null)
                {
                    gameManager.middlePlatform.PlatformTakeDamage(100f);
                }
                else if (collision.gameObject.GetComponent<Enemies>().portalOrigin == Enemies.PortalOrigins.right && gameManager.rightPlatform != null)
                {
                    gameManager.rightPlatform.PlatformTakeDamage(100f);
                }
            }

            collision.gameObject.GetComponent<Enemies>().Explode();

        }
        else if (collision.gameObject.tag == "Bullet") 
        {
            Destroy(collision.gameObject);
        }

    }
}
