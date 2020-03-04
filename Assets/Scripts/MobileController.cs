using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    public Vector2 fingerDown;
    public Vector2 fingerUp;

    public float swipeThreshold = 20f;

    public bool detectSwipeOnlyAfterRelease = true;

    public bool DebugMode = true;

    public Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        GetSwipeDistance();
    }

    private void GetSwipeDistance()
    {
        //debug pc or mac
        if (DebugMode)
        {
            if(Input.GetMouseButtonDown(0))
            {
                fingerDown = Input.mousePosition;
                fingerUp = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                fingerDown = Input.mousePosition;
                checkSwipe();
            }
        }

        // mobiile
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //detect Swipe while finger is still moving --optional
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detect swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
    {
        //check for tap
        if (verticalMove() < swipeThreshold && horizontalMove() < swipeThreshold)
        {
            // tap
            OnTap();
        } //check for vertical swipe
        else if (verticalMove() > swipeThreshold && verticalMove() > horizontalMove())
        {
            //Vertical Swipe
            if (fingerDown.y - fingerUp.y > 0)
            {
                //swiped up
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)
            {
                //swiped down
                OnSwipeDown();
            }
            fingerUp = fingerDown; // reset 
        } //check for horizontal swipe
        else if (horizontalMove() > swipeThreshold && horizontalMove() > verticalMove())
        {
            // horizontal swipe
            if (fingerDown.x - fingerUp.x > 0)
            {
                // right swipe
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)
            {
                // left swipe
                OnSwipeLeft();
            }
            fingerUp = fingerDown; // reset
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    void OnSwipeUp()
    {
        Debug.Log("Swipe Up");
        player.Jump();
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
        player.Slam();
    }

    void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
        player.MoveLeft();
    }

    void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
        player.MoveRight();
    }

    void OnTap()
    {
        Debug.Log("Tap");
        player.Shoot();
    }
}
