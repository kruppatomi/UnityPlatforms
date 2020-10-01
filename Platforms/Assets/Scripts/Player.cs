using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BoxCollider player;
    public Rigidbody rig; 
    public float Rollspead;
    public bool isRolling;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    public bool ableToMoveLeft;
    public bool ableToMoveRight;

    // Start is called before the first frame update
    void Start()
    {
        ableToMoveLeft = false;
        ableToMoveRight = false;
        player = player.GetComponent<BoxCollider>();
        dragDistance = Screen.height * 10 / 100; //dragDistance is 15% height of the screen
        rig.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRolling)
        {
            return;
        }
        if(player.transform.position.y < -2){
            Physics.gravity = new Vector3(0, -100F, 0);
        }
        TouchControl();
    }


    IEnumerator Roll(Vector3 pivot, Vector3 _vector)
    {
        for (int i = 0; i < (90 / Rollspead); i++)
        {
            transform.RotateAround(pivot, _vector, Rollspead);
            yield return new WaitForSeconds(0.01F);
        }
        isRolling = false;
        rig.useGravity = true;
        yield return null;
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name.Equals("block(Clone)")){
            ableToMoveLeft = true;
            ableToMoveRight = true;
        }
    }

    void OnCollisionExit(Collision collision){
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name.Equals("block(Clone)")){
            ableToMoveLeft = false;
            ableToMoveRight = false;
        }
    }

    public void TouchControl(){
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
                        if ((lp.x > fp.x) && ableToMoveRight.Equals(true))  //If the movement was to the right)
                        {  
                            //Right swipe
                            isRolling = true;
                            StartCoroutine(Roll(transform.position + new Vector3(0, -1, -1f), Vector3.left));
                            rig.useGravity = false;
                        }
                            //Left swipe
                        if(!(lp.x > fp.x) && ableToMoveLeft.Equals(true)) {
                            isRolling = true;
                            StartCoroutine(Roll(transform.position + new Vector3((float)-1f, (float)-1f, 0), Vector3.forward));
                            rig.useGravity = false;
                        }
                        else {
                            return;
                        }
                    }
                    else
                    {   
                        return;
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }
}
