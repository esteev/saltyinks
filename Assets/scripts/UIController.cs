using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class UIController : MonoBehaviour {

    private float invokeCallRate = 0.05f;
    private float initLoginSpeed = 150f;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    public GameObject animatables, dotdot;
    private int animState = 1;

    void Start () {
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }

    void Update()
    {
    /*    if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");

                            right();
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            left();
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
        */
    }

    Vector3[] target = { Vector3.zero, new Vector3(-600f, 0f, 0f), new Vector3(-1200f, 0f, 0f) };

    void getToPos()
    {
        if (animatables.GetComponent<Transform>().localPosition != target[animState-1])
        {
            animatables.GetComponent<Transform>().localPosition = Vector3.MoveTowards(animatables.GetComponent<Transform>().localPosition, target[animState - 1], initLoginSpeed);
        }
        else
        {
            CancelInvoke();
        }
    }

    public void righty()
    {
        print(animState);
   //     enableAnimation();
        switch (animState)
        {
            case 1:
                animState = 2;
                InvokeRepeating("getToPos", invokeCallRate, invokeCallRate);
            //    animatables.GetComponent<Animation>().Play("12");
                dotdot.GetComponent<Animation>().Play("dot12");
                break;
            case 2:
                animState = 3;
                InvokeRepeating("getToPos", invokeCallRate, invokeCallRate);
                dotdot.GetComponent<Animation>().Play("dot23");
                break;
        }
    //    disableAnimation();
    }

    public void lefty()
    {
        print(animState);
        //     enableAnimation();
        switch (animState)
        {
            case 3:
                animState = 2;
                InvokeRepeating("getToPos", invokeCallRate, invokeCallRate);
                dotdot.GetComponent<Animation>().Play("dot32");
                break;
            case 2:
                animState = 1;
                InvokeRepeating("getToPos", invokeCallRate, invokeCallRate);
                dotdot.GetComponent<Animation>().Play("dot21");
                break;
        }
    //    disableAnimation();
    }

    void enableAnimation()
    {
        animatables.GetComponent<Animation>().enabled = true;
        dotdot.GetComponent<Animation>().enabled = true;
    }

    void disableAnimation()
    {
        animatables.GetComponent<Animation>().enabled = false;
        dotdot.GetComponent<Animation>().enabled = false;
    }

    private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}
		} else {
			Debug.Log("User cancelled login");
		}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	public void fbLogin()
	{
		if (kaChow.Instance.fbLogin == 0) {
			var perms = new List<string> (){ "public_profile", "email" };
			FB.LogInWithReadPermissions (perms, AuthCallback);
		}
	}

	public void googleLogin()
	{
		
	}

}
