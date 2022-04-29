using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePuzzle : InteractionParent
{
    GameObject playerCamera;                // required references
    InteractionManager interactionTarget;

    public GameObject setFlameActive;       // target flames references
    public GameObject setFlameInactive;
    public GameObject setFlameInactive2;
    public GameObject setFlameInactive3;
    public GameObject setFlameInactive4;
    public GameObject setFlameInactive5;

    public int itemId;                     // item id of the candle flame (to pass to player inventory)

    public BottomFloorPuzzle puzzleScript;     // puzzle master script to reference  
    public GameObject temporaryFlame;           // flame for player to reference after activation

    public AudioClip initial;
    public AudioClip secondary;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("Main Camera");          // initialise references
        interactionTarget = playerCamera.GetComponent<InteractionManager>();

        firstMessage = "E - Interact";          // set messages
        alternateMessage = "This could be useful!";
        defaultMessage = "E - Interact";

        puzzleScript = GameObject.Find("GameMaster").GetComponent<BottomFloorPuzzle>();     // set the reference
    }

    // Update is called once per frame
    void Update()
    {
        if(interactionTarget.itemIDs.Contains(itemId))
        {
            gameObject.layer = 0;                           // switch the layer mask (disable interaction)
        }
        else
        {
            gameObject.layer = 3;                           // otherwise reset layer mask
            if (interactionTarget.itemIDs.Contains(-1))
            {
                defaultMessage = "E - Light the candle";
            }
        }
    }

    public override List<int> Activate(List<int> playerItems)       // activate function (overrides the parent class)
    {
        if (interactionTarget.candleActive)                         // if player has candle...
        {
            // if player contains any flames....
            if (playerItems.Count == 2)
            {
                playerItems.RemoveAt(1);                            // remove the recent flame

            }
 
            puzzleScript.SubmitFlame(temporaryFlame);               // pass the temp flame reference

            setFlameActive.SetActive(true);                         // reset temp flames in the scene
            setFlameInactive.SetActive(false);
            setFlameInactive2.SetActive(false);
            setFlameInactive3.SetActive(false);
            setFlameInactive4.SetActive(false);
            setFlameInactive5.SetActive(false);

            playerItems.Add(itemId);                            // add a new flame

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

        return playerItems;
    }
}

