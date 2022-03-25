using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomFloorPuzzle : MonoBehaviour
{
    // amethyst  dragonseye emerald sapphire topaz
    public int counter = 0;
    public List<string> gems = new List<string> { "amethyst", "dragonseye", "emerald", "sapphire", "topaz", "jasper" };

    public bool puzzleAccomplished;


    public GameObject amethystFlame;            // reference all the temporary flames
    public GameObject dragonsEyeFlame;
    public GameObject emeraldFlame;
    public GameObject sapphireFlame;
    public GameObject topazFlame;
    public GameObject jasperFlame;


    public GameObject playerCamera;                 // reference target camera to get interaction manager script
    InteractionManager playerTargetScript;

    public GameObject currentSelectedFlame;                 // to reference current temporary flame


    // Update is called once per frame
    void Update()
    {

    }

    void Start()
    {
        playerTargetScript = playerCamera.GetComponent<InteractionManager>();

        amethystFlame.SetActive(false);             // set all the temporary flames inactive
        dragonsEyeFlame.SetActive(false);
        emeraldFlame.SetActive(false);
        sapphireFlame.SetActive(false);
        topazFlame.SetActive(false);
        jasperFlame.SetActive(false);
    }

    public void SubmitFlame(GameObject temporaryFlame)
    {

      //  currentSelectedFlame.SetActive(false);
        currentSelectedFlame = temporaryFlame;
        //temporaryFlame.SetActive(true);
    }
}
