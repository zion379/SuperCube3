using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody rigidbody;

    public Transform RightPosition;
    public Transform MiddlePosition;
    public Transform LeftPosition;

    public float playerspeed = 1f;
    public float moveDuration = 1f;
    public bool snaptonewlocation = false;

    public float Health = 100f;

    public GameObject Bullet;
    public Transform BulletSpawn;

    public SlamDetection slamDetection;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidbody = GetComponent<Rigidbody>();
        BulletSpawn = GameObject.Find("BulletSpawn").transform;

        slamDetection = GetComponent<SlamDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            Debug.Log("Shooting");
        }

        // check id player is falling from slam
        IsPlayerFallingFromSlam();

        CheckHealth();
    }

    private void Died() 
    {
        // call player died from game manager
        //play players death animation
        Debug.Log("Player is dead");
        gameManager.playerDied();
        this.gameObject.active = false;
     }

    public void TakeDamage(float damageAmount) 
    {
        Health -= damageAmount;
    }

    private void CheckHealth()
    {
        if (Health <= 0)
        {
            Died();
        }
    }

    //Testing for player death.
    public void SimulatePlayerDeath() 
    {
        TakeDamage(100f);
     }

    private void FixedUpdate()
    {
        DevelopmentMovement();
        // Test gravity pull for jumping this should be handled by jumping script
        GetCurrentPlatform();

    }

    void DevelopmentMovement() 
    {
        // test movement
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            Jump();
         }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            Slam();
         }

        //testing
        /*
         * test out player death and it works.      
        if(Input.GetKeyDown(KeyCode.D)) 
        {
            SimulatePlayerDeath();
            Debug.Log("Simulating Death");
         }
         */
    }

    // check if positions are not null
    bool CheckPosExist() 
    {
        if(RightPosition != null && MiddlePosition != null && LeftPosition != null) 
        {
            return true;
         }
        else 
        {
            Debug.LogError("Could not find positions");
            return false;
         }
    }

    private void MoveRight() 
    {
        if(currentPos == PlayerPos.left && CheckPosExist()) 
        {
            // move to middle pos
            Vector3 rightpos = new Vector3(MiddlePosition.position.x, this.transform.position.y, RightPosition.position.z);
            transform.DOMove(rightpos, moveDuration, snaptonewlocation);
        }
        else if ( currentPos == PlayerPos.middle) 
        {
            // move to right pos 
            Vector3 rightpos = new Vector3(RightPosition.position.x, this.transform.position.y, RightPosition.position.z);
            transform.DOMove(rightpos, moveDuration, snaptonewlocation);
        }
    }

    private void MoveLeft()
    {
        if (currentPos == PlayerPos.right && CheckPosExist())
        {
            // move to middle pos
            Vector3 rightpos = new Vector3(MiddlePosition.position.x, this.transform.position.y, LeftPosition.position.z);
            transform.DOMove(rightpos, moveDuration, snaptonewlocation);
        }
        else if (currentPos == PlayerPos.middle)
        {
            // move to left pos
            Vector3 rightpos = new Vector3(LeftPosition.position.x, this.transform.position.y, LeftPosition.position.z);
            transform.DOMove(rightpos, moveDuration, snaptonewlocation);
        }
    }

    public float fallmultiplier = 2.5f;
    public float lowJumpMultiplier = 1f;
    public Vector3 JumpVector = new Vector3(0, 1f, 0f);
    [Range(1,10)]
    public float jumpVelocity = 2f;

    private void JumpFall() 
    {
        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else if (rigidbody.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space)) 
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallmultiplier - 1) * Time.deltaTime;
        }

    }

    private void Jump() 
    {
        rigidbody.velocity = Vector3.up * jumpVelocity;
     }

    [Range(1,10)]
    public float slamForce;

    private void Slam() 
    {
        rigidbody.velocity = Vector3.down * slamForce;
    }

    [Range(-10,-4)]
    public float slamTriggerDetectionvalue = -4f;
    private void IsPlayerFallingFromSlam() // this will return a bool 
    {
        if (rigidbody.velocity.y <= slamTriggerDetectionvalue) 
        {
            slamDetection.slaming = true;
         }
        else
        {
            slamDetection.slaming = false;
         }
    }

    private void Shoot() 
    {
        Vector3 newBulletPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Instantiate(Bullet, newBulletPos, transform.rotation);
     }

    private void TweenMove(Vector3 Startpos, Vector3 Endpos) 
    {

     }

    public enum PlayerPos {left, middle, right};
    public PlayerPos currentPos = PlayerPos.middle;

    /*
     * Currently not in use it is ok to delete this.  
    // Get Player Current Platform
    private void OnCollisionEnter(Collision collision)
    {
        //check tag name for pos
        if(collision.gameObject.tag == "MiddlePlatform") 
        {
            currentPos = PlayerPos.middle;
         }
        if(collision.gameObject.tag == "RightPlatform") 
        {
            currentPos = PlayerPos.right;
         }
        if(collision.gameObject.tag == "LeftPlatform")
        {
            currentPos = PlayerPos.left;
         }
    }
    */

    // use raycast to get currentplatform
    public float RaycastDetectionDistance = 10f;
    RaycastHit hit;
    private void GetCurrentPlatform() 
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * RaycastDetectionDistance,out hit)) 
        {
            if(hit.collider.gameObject.tag == "MiddlePlatform") 
            {
                currentPos = PlayerPos.middle;
            }
            if (hit.collider.gameObject.tag == "RightPlatform")
            {
                currentPos = PlayerPos.right;
            }
            if (hit.collider.gameObject.tag == "LeftPlatform")
            {
                currentPos = PlayerPos.left;
            }

        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * RaycastDetectionDistance, Color.white);
    }
}
