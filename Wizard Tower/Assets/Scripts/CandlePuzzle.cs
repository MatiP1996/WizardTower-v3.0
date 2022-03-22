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

    public int itemId;

    public BottomFloorPuzzle puzzleScript;
    public GameObject temporaryFlame;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("Main Camera");          // initialise references
        interactionTarget = playerCamera.GetComponent<InteractionManager>();

        firstMessage = "E - Interact";          // set messages
        alternateMessage = "This could be useful!";
        defaultMessage = "E - Interact";

        puzzleScript = GameObject.Find("GameMaster").GetComponent<BottomFloorPuzzle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactionTarget.candleActive)
        {
            firstMessage = "E - Light the candle";
            alternateMessage = "";
        }
    }

    public override List<int> Activate(List<int> playerItems)
    {
        defaultMessage = alternateMessage;

        if (interactionTarget.candleActive)
        {

            if (playerItems.Contains(1) || playerItems.Contains(2) || playerItems.Contains(3) || playerItems.Contains(4) || playerItems.Contains(5) || playerItems.Contains(6))
            {
                playerItems.RemoveAt(1);
            }

            playerItems.Add(itemId);

            puzzleScript.SubmitFlame(temporaryFlame);

            setFlameActive.SetActive(true);
            setFlameInactive.SetActive(false);
            setFlameInactive2.SetActive(false);
            setFlameInactive3.SetActive(false);
            setFlameInactive4.SetActive(false);
            setFlameInactive5.SetActive(false);
        }

        return playerItems;
    }
}
