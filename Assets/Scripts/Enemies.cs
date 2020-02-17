using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GameManager gameManager;

    public float EnemySpeed = 1f;

    public bool explode; // this will be used to explode enemies

    public int KillPoints = 1;

    public Rigidbody rigidBody;

    public float Health = 100f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        moveEnemyFoward();
    }
    public void Explode() 
    {
        // this can be called from other scripts
        //Play Exploding animation
        Destroy(this.gameObject, .1f); // make sure to add in wait time later.

    }

    // detect collision with player - then explode

    // detect collision with end of platform then explode

    private void IncreasePlayerScore() 
    {
        // increase player score in game manager
     }

    private void moveEnemyFoward() 
    {
        Vector3 foward = new Vector3(0, 0, -1);
        rigidBody.AddForce(foward * EnemySpeed);
     }

    public void takeDamage(float damage) 
    {
        Health -= damage;
        if(Health <= 0) 
        {
            Die();
         }
    }

    public void Die() 
    {
        // Play Death Animation
        Destroy(this.gameObject, .5f); // make sure to add in wait time later.
    }

    public enum PortalOrigins  {left, right, middle}
    public PortalOrigins portalOrigin = PortalOrigins.middle; // portal origins are set when enemy is created from GameManager 
    // portal Origin is set in GameManager when enemy is created.
}
