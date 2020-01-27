using UnityEngine;
using UnityEngine.SceneManagement;

public class AccelerometerInput : MonoBehaviour
{
    // Move object using accelerometer
    public float speed;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    public GameObject cameraTarget;

    private Rigidbody rigid;
    public bool isFlat;

    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {

        Vector3 tilt = Input.acceleration;
        Vector3 flip = new Vector3(tilt.x, 0, -tilt.z);
        if (isFlat)
        {
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        }

        rigid.AddForce(flip * speed);
        cameraTarget.transform.position = transform.position;

        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.cyan);
        /*
        if (Input.touchCount == 1) // user is touching the screen with a single touch
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
                            transform.Rotate(0.0f, 90.0f, 0.0f, Space.World);
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            transform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
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
        }*/

    }
}