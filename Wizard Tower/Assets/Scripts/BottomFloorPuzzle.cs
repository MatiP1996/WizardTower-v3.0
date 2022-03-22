using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomFloorPuzzle : MonoBehaviour
{
    // amethyst  dragonseye emerald sapphire topaz
    public int counter = 0;
    public List<string> gems = new List<string> { "amethyst", "dragonseye", "emerald", "sapphire", "topaz", "jasper" };

    public bool puzzleAccomplished;
    /*
    public GameObject amethystTorch;
    public GameObject dragonsEyeTorch;
    public GameObject emeraldTorch;
    public GameObject sapphireTorch;
    public GameObject topazTorch;
    public GameObject jasperTorch;
        */

    public GameObject amethystFlame;
    public GameObject dragonsEyeFlame;
    public GameObject emeraldFlame;
    public GameObject sapphireFlame;
    public GameObject topazFlame;
    public GameObject jasperFlame;


    public GameObject playerCamera;
    InteractionManager playerTargetScript;

    public GameObject currentSelectedFlame;


    // Update is called once per frame
    void Update()
    {

    }

    void Start()
    {
        playerTargetScript = playerCamera.GetComponent<InteractionManager>();

        amethystFlame.SetActive(false);
        dragonsEyeFlame.SetActive(false);
        emeraldFlame.SetActive(false);
        sapphireFlame.SetActive(false);
        topazFlame.SetActive(false);
        jasperFlame.SetActive(false);
    }

    public void SubmitFlame(GameObject temporaryFlame)
    {
        Debug.Log("YO");
      //  currentSelectedFlame.SetActive(false);
        currentSelectedFlame = temporaryFlame;
        //temporaryFlame.SetActive(true);
    }
}
