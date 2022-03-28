using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPuzzle : InteractionParent
{
    public int requiredItemId;   // player candle needed               

    GameObject playerCamera;                // required references
    InteractionManager interactionTarget;

    public float distanceAbove = 0.5f;      // position of the flame about the torch

    public GameObject correctFlame;         // the correct flame to reference

    public GameObject puzzleMaster;         // target master script to communicate with
    BottomFloorPuzzle targetScript;

    /*
    public float timeActivated = -2;        // flame fizz up mechanic time
      
    */

    public float maxTimeActive = 2f;
    bool torchActive;                   
    bool torchInitiated;
    public float timeInitiated;
    bool communicateWithMaster;


    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("Main Camera");         // initialise variables         
        interactionTarget = playerCamera.GetComponent<InteractionManager>();
        firstMessage = "E - Interact";              // set the messages
        alternateMessage = "I need a flame!";
        defaultMessage = "E - Interact";

        correctFlame.SetActive(false);              // set the correct flame inactive

        targetScript = puzzleMaster.GetComponent<BottomFloorPuzzle>();      // reference the puzzle master script

        Vector3 current = transform.position;           // calculate the flame position
        current.y += distanceAbove;
        correctFlame.transform.position = current;

    }

    // Update is called once per frame
    void Update()
    {
        if (interactionTarget.flameActive)              // once player candle is available...
        {                                                           // alter messages
            firstMessage = "E - Light the torch";
            alternateMessage = "Fire!";
        }

        if (!torchActive)                            // while torch is activated  >>  make it unavailable
        {
            gameObject.layer = 3;
        }
        else
        {
            gameObject.layer = 0;

            if (torchInitiated)
            {
                float currentTime = Time.time;
                if (currentTime >= timeInitiated + maxTimeActive)
                {
                    torchInitiated = false;
                    Debug.Log("Yo");
                    if (communicateWithMaster)
                    {
                        Debug.Log("Yo1");
                        targetScript.SubmitTorch(requiredItemId);
                        communicateWithMaster = false;
                    }
                    else
                    {
                        Debug.Log("Yo2");
                        targetScript.ResetTempFlames();
                        torchActive = false;
                    }
                }
            }
        }
    }


    public override List<int> Activate(List<int> playerItems)   //dedicated to interacting with the object
    {                                                           // if player contains any flames in the inventory...
        if(playerItems.Contains(1) || playerItems.Contains(2) || playerItems.Contains(3) || playerItems.Contains(4) || playerItems.Contains(5) || playerItems.Contains(6))
        {
            torchActive = true;
            timeInitiated = Time.time;
            torchInitiated = true;

            if (playerItems.Contains(requiredItemId))    // if player contains the correct flame...
            {
                
                correctFlame.SetActive(true);
                communicateWithMaster = true;

                
            }
            else                                                    // otherwise...
            {
            //    torchInitiated = true;
                timeInitiated = Time.time;

                Vector3 current = transform.position;           // calculate the flame position
                current.y += distanceAbove;
                GameObject flame = targetScript.currentSelectedFlame;       // reference the correct temporary flame
                flame.transform.position = current;
                flame.SetActive(true);                                      // set active            
            }
        }
        else
        {                               // when no flames in inventory
            defaultMessage = alternateMessage;     // set the UI message
        }

        return playerItems;
    }

    public void ResetTorch()
    {
        torchActive = false;
        correctFlame.SetActive(false);
    }
}
