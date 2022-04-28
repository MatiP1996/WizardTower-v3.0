using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    public CharacterController controller;
    public Camera playerCam;
    public Camera staticCam;
    public GameObject g;
    bool playerOn;
    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.FindGameObjectWithTag("Player");


        controller = g.GetComponent<CharacterController>();

        playerCam.enabled = true;
        staticCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            CamToggle();
        }
    }
    public void CamToggle()
    { 

        if (playerOn)
        {
            playerCam.enabled = false;
            staticCam.enabled = true;
            controller.inputAllowed = false;

        }
        else
        {   
            playerCam.enabled = true;
            staticCam.enabled = false;
            controller.inputAllowed = true;
        }

        
    }
}
