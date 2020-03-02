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

    public PointSystem pointSystem;

    public GameLogic gameLogic;

    public Player player;

    public float damagetoPlayer = 10f;

    public float energyToPlayer = 20f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidBody = GetComponent<Rigidbody>();

        pointSystem = GameObject.Find("GameManager").GetComponent<PointSystem>();
        if (pointSystem == null)
        {
            Debug.LogError("enemy point system is null");
        }

        gameLogic = GameObject.Find("GameManager").GetComponent<GameLogic>();

        if(gameManager.playerDead != true)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
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
        //Decrease Player health.
        Destroy(this.gameObject, .1f); // make sure to add in wait time later.

    }

    // detect collision with player - then explode

    // detect collision with end of platform then explode

    private void IncreasePlayerScore() 
    {
        pointSystem.IncreaseScore(KillPoints);
        gameLogic.KeepTrackOfKilledEnemies();
        gameLogic.IncreasePlayerSlamEnergy(energyToPlayer);

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
            IncreasePlayerScore();
            Die();
         }
    }

    [Range(1,10)]
    public float slamForce = 1f;
    public void DieFromSlam()
    {
        rigidBody.AddForce(new Vector3(0, slamForce, slamForce), ForceMode.Acceleration);
        Destroy(this.gameObject, 1f);
    }

    public void Die() 
    {
        // Play Death Animation
        Destroy(this.gameObject, .5f); // make sure to add in wait time later.
    }

    public bool appliedDamage = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !appliedDamage)
        {
            Explode();
            player.TakeDamage(damagetoPlayer);
            appliedDamage = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !appliedDamage)
        {
            Explode();
            player.TakeDamage(damagetoPlayer);
            appliedDamage = true;
        }
    }

    public enum PortalOrigins  {left, right, middle}
    public PortalOrigins portalOrigin = PortalOrigins.middle; // portal origins are set when enemy is created from GameManager 
    // portal Origin is set in GameManager when enemy is created.
}
