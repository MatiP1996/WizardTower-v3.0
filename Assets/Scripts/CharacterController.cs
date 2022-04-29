using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody rb;
    public float velocity;
    public float jumpForce;
    public float gravityForce;

    Vector3 playerMove;

    public float vertical;
    public float horizontal;
    public bool jump;
    public bool grounded;

    public Transform groundCheckLocation;
    public Transform playerHands;

    // ground check data
    public LayerMask groundMask;
    public float groundDistance;
    public float jumpDelay;
    public float timeJumped;

    public bool movementAllowed;
    public bool inputAllowed = true;

    // player grab purposes
    //   public GameObject playerCamera;
    //    public CameraControl cameraScript;

    public bool spaceAllowed;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
     //   cameraScript = playerCamera.GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {

        //  determine inputs
        if (inputAllowed)
        {
            horizontal = Input.GetAxis("Horizontal"); // player movement choices
            vertical = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space))     // check jump
            {
                jump = true;
            }
        }

    }

    private void FixedUpdate()  // every physics tick
    {

        // execute gravity
        rb.AddForce(new Vector3(0, -gravityForce, 0));


        //check if grounded
        grounded = Physics.CheckSphere(groundCheckLocation.position, groundDistance, groundMask);

            
        // execute movement
        if(timeJumped + jumpDelay > Time.time)
        {
            movementAllowed = false;
        }
        else
        {
            movementAllowed = true;
        }

        if (grounded && movementAllowed)
        {
            if (jump)
            {
                rb.AddForce(new Vector3(0, jumpForce, 0));
                jump = false;
                timeJumped = Time.time;
            }
            else
            {
                playerMove = transform.right * horizontal * velocity + transform.forward * vertical * velocity;
                rb.velocity = playerMove;
            }

        }

    }


}


/*
private void OnCollisionEnter(Collision collision)
{
    if(collision.gameObject.CompareTag("Wall"))
    {
        rb.freezeRotation = true;
    }

}

private void OnCollisionExit(Collision collision)
{
    if (collision.gameObject.CompareTag("Wall"))
    {
        rb.freezeRotation = false;

        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }
}
*/