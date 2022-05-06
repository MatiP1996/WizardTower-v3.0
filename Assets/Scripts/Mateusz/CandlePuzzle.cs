using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePuzzle : InteractionParent
{
    GameObject playerCamera;                // required references

    public GameObject setFlameActive;       // target flames references
    public GameObject setFlameInactive;
    public GameObject setFlameInactive2;
    public GameObject setFlameInactive3;
    public GameObject setFlameInactive4;
    public GameObject setFlameInactive5;

    public int itemId;                     // item id of the candle flame (to pass to player inventory)

    public BottomFloorPuzzle puzzleScript;     // puzzle master script to reference  
    public GameObject temporaryFlame;           // flame for player to reference after activation

    public AudioClip alternateClip;


   // AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        targetPlayerScript = GameObject.Find("Main Camera").GetComponent<InteractionManager>();

        firstMessage = "E - Interact";          // set messages
        alternateMessage = "This could be useful!";
        defaultMessage = "E - Interact";

        puzzleScript = GameObject.Find("GameMaster").GetComponent<BottomFloorPuzzle>();     // set the reference
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPlayerScript.itemIDs.Contains(itemId))
        {
            gameObject.layer = 0;                           // switch the layer mask (disable interaction)
        }
        else
        {
            gameObject.layer = 7;                           // otherwise reset layer mask
            if (targetPlayerScript.itemIDs.Contains(-1))
            {
                defaultMessage = "E - Light the candle";
            }
        }
    }

    public override float Activate()       // activate function (overrides the parent class)
    {
        if (targetPlayerScript.candleActive)                         // if player has candle...
        {
            // if player contains any flames....
            if (targetPlayerScript.itemIDs.Count == 2)
            {
                targetPlayerScript.itemIDs.RemoveAt(1);                            // remove the recent flame

            }
 
            puzzleScript.SubmitFlame(temporaryFlame);               // pass the temp flame reference

            setFlameActive.SetActive(true);                         // reset temp flames in the scene
            setFlameInactive.SetActive(false);
            setFlameInactive2.SetActive(false);
            setFlameInactive3.SetActive(false);
            setFlameInactive4.SetActive(false);
            setFlameInactive5.SetActive(false);

            targetPlayerScript.itemIDs.Add(itemId);                            // add a new flame

            source.PlayOneShot(alternateClip);

        }
        else
        {
            defaultMessage = alternateMessage;                          // change the UI message
            if(canActivate)
            {
                if (!source.isPlaying)
                {
                    source.PlayOneShot(defaultClip);
                }
            }
        }
        return pauseTime;
    }
}

