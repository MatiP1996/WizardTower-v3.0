using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPuzzle : InteractionParent
{
    public int requiredItemId = 0;      // player candle needed               

    GameObject playerCamera;                // required references
    InteractionManager interactionTarget;

    public float distanceAbove = 0.5f;

    public bool initiated;
    public float timeInitiated;

    public GameObject correctFlame;

    public GameObject puzzleMaster;
    BottomFloorPuzzle targetScript;

        // target objects to receive from puzzle master
    public GameObject amethystTorch;
    public GameObject dragonsEyeTorch;
    public GameObject emeraldTorch;
    public GameObject sapphireTorch;
    public GameObject topazTorch;
    public GameObject jasperTorch;

    public float timeActivated = -2;
    public float maxTimeActive = 2f;



    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("Main Camera");         // initialise variables         
        interactionTarget = playerCamera.GetComponent<InteractionManager>();
        firstMessage = "E - Interact";              // set the messages
        alternateMessage = "I need a flame!";
        defaultMessage = "E - Interact";

        correctFlame.SetActive(false);

        targetScript = puzzleMaster.GetComponent<BottomFloorPuzzle>();
        /*
        amethystTorch = targetScript.amethystTorch;
        dragonsEyeTorch = targetScript.dragonsEyeTorch;
        emeraldTorch = targetScript.emeraldTorch;
        sapphireTorch = targetScript.sapphireTorch;
        topazTorch = targetScript.topazTorch;
        jasperTorch = targetScript.jasperTorch;
        */

    }

    // Update is called once per frame
    void Update()
    {
        if(interactionTarget.flameActive)              // once player candle is available...
        {                                                           // alter messages
            firstMessage = "E - Light the torch";
            alternateMessage = "Fire!";
        }

        //reset the torch flame
  //      if(Time.time <)

    }

    public override List<int> Activate(List<int> playerItems)
    {
        if(playerItems.Contains(1) || playerItems.Contains(2) || playerItems.Contains(3) || playerItems.Contains(4) || playerItems.Contains(5) || playerItems.Contains(6))
        {
            gameObject.tag = "Undefined";
            initiated = true;
            timeInitiated = Time.time;

            if(playerItems.Contains(requiredItemId))
            {
                Vector3 current = transform.position;
                current.y += distanceAbove;
                correctFlame.transform.position = current;
                correctFlame.SetActive(true);
            }
            else
            {
                Vector3 current = transform.position;
                current.y += distanceAbove;
                GameObject flame = targetScript.currentSelectedFlame;
                flame.transform.position = current;
                flame.SetActive(true);
                timeActivated = Time.time;
            }

        }
        else
        {
            defaultMessage = alternateMessage;
        }
        return playerItems;
    }


}
