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
    public bool inputAllowed = true;

    public AudioSource source;
    public AudioClip footsteps;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (PauseMenu.pauseMenuVisible == false) // if pause menu isn't currently open
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // check if player is grounded

            if (inputAllowed)
            {
                if (isGrounded && velocity.y < 0) // if player is grounded and moving down, set the move down velocity to -1 (avoids excess values)
                {
                    velocity.y = -1f;
                }

                float x = Input.GetAxis("Horizontal"); // gets player AD input as a float
                float z = Input.GetAxis("Vertical"); // gets player WS input as a float

                Vector3 move = transform.right * x + transform.forward * z; // vector for the player input on horizontal plane

                controller.Move(move * speed * Time.deltaTime); //moves the player on horizontal plane using the above move vector


                if(x != 0 || z != 0)            // footsteps
                {
                    if (isGrounded)
                    {
                        int num = Random.Range(0, 250);
                        if(num == 0)
                        {
                            if (!source.isPlaying)
                            {
                                source.PlayOneShot(footsteps);
                            }
                                
                        }
                    }
                }


                if (Input.GetButtonDown("Jump") && isGrounded) // if player is grounded and jumps, change the players vertical velocity
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                if (LadderClimb.onLadder == false && VineClimb.onVine == false)  // set vertical velocity
                {
                    velocity.y += gravity * Time.deltaTime;
                }
                else
                {
                    velocity.y = -2;
                }

                controller.Move(velocity * Time.deltaTime); //moves on vertical axis
            }
        }
    }
}
