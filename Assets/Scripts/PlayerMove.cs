using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public UnityEngine.CharacterController controller;

    public float speed;
    public float gravity;
    public float jumpHeight;
    

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;


    Vector3 velocity;
    public bool isGrounded;


    void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
    }




    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.pauseMenuVisible == false)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -1f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;


            controller.Move(move * speed * Time.deltaTime); //moves on horizontal plane

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }



            if (LadderClimb.onLadder == false)
            {
                velocity.y += gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = -2;
            }

            controller.Move(velocity * Time.deltaTime); //moves on verical axis
        }
    }
}
