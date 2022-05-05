using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPuzzle : InteractionParent
{
    public int requiredItemId;   // player candle needed               

    public float distanceAbove = 0.4f;      // position of the flame about the torch
    public float distanceFront = 0.07f;

    public GameObject correctFlame;         // the correct flame to reference

    GameObject puzzleMaster;         // target master script to communicate with
    BottomFloorPuzzle targetScript;


    public float maxTimeActive = 2f;        // resetting, timer and puzzle master communication purposes...
    bool torchActive;                   
    bool torchInitiated;
    public float timeInitiated;
    bool communicateWithMaster;
    GameObject tempFlame;


    // Start is called before the first frame update
    void Start()
    {
        targetPlayerScript = GameObject.Find("Main Camera").GetComponent<InteractionManager>();

        firstMessage = "E - Interact";              // set the messages
        alternateMessage = "I need a flame!";
        defaultMessage = "E - Interact";

        correctFlame.SetActive(false);              // set the correct flame inactive

        puzzleMaster = GameObject.Find("GameMaster");
        targetScript = puzzleMaster.GetComponent<BottomFloorPuzzle>();      // reference the puzzle master script



    }

    // Update is called once per frame
    void Update()
    {
        if (targetPlayerScript.flameActive)              // once player candle is available...
        {                                                           // alter messages
            firstMessage = "E - Light the torch";
            alternateMessage = "Fire!";
        }

        if (!torchActive)                            // while torch is activated  >>  make it available
        {
            gameObject.layer = 7;
        }
        else                                        // otherwise unavailable...
        {
            gameObject.layer = 0;

            if (torchInitiated)                     // once initiated  >>  monitor the time
            {
                float currentTime = Time.time;
                if (currentTime >= timeInitiated + maxTimeActive)       // past the timer...
                {
                    torchInitiated = false;                             // initiation complete...
                    if (communicateWithMaster)                          // correct flame id
                    {
                        targetScript.SubmitTorch(requiredItemId);      
                        communicateWithMaster = false;
                    }
                    else                                               // incorrect flame id
                    {
                        tempFlame.SetActive(false);
                        torchActive = false;
                    }
                }
            }
        }
    }


    public override float Activate()   //dedicated to interacting with the object
    {                                                           // if player contains any flames in the inventory...
        List<int> items = targetPlayerScript.itemIDs;
        if (items.Contains(0) || items.Contains(1) || items.Contains(2) || items.Contains(3) || items.Contains(4) || items.Contains(5))
        {
            torchActive = true;
            timeInitiated = Time.time;
            torchInitiated = true;

            if (items.Contains(requiredItemId))    // if player contains the correct flame...
            {

                Vector3 current = transform.position;           // calculate the flame position
                current.y += distanceAbove;
                current.z += distanceFront;
                correctFlame.transform.position = current;

                correctFlame.SetActive(true);
                communicateWithMaster = true;
       
            }
            else                                                    // otherwise...
            {
                timeInitiated = Time.time;                      // timer
                Vector3 current = transform.position;           // calculate the flame position
                current.y += distanceAbove;
                tempFlame = targetScript.currentSelectedFlame;       // reference the temporary flame
                tempFlame.transform.position = current;                         // place at coordinates
                tempFlame.SetActive(true);                                      // make visible          
            }
        }
        else
        {                               // when no flames in inventory
            defaultMessage = alternateMessage;     // set the UI message
        }

        return pauseTime;
    }

    public void ResetTorch()            
    {
        torchActive = false;
        correctFlame.SetActive(false);
    }
}
