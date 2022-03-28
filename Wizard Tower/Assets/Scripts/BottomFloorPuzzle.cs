using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomFloorPuzzle : MonoBehaviour
{
    // amethyst  dragonseye emerald sapphire topaz
    public int counter = 0;
    public List<int> gemsOrder = new List<int> {1,2,3,4,5,6};

    public bool puzzleAccomplished;


    public GameObject amethystFlame;            // reference all the temporary flames
    public GameObject dragonsEyeFlame;
    public GameObject emeraldFlame;
    public GameObject sapphireFlame;
    public GameObject topazFlame;
    public GameObject jasperFlame;

    List<GameObject> tempFlames = new List<GameObject>();

  //  public List<GameObject> correctFlames;
    public List<GameObject> torches;


    public GameObject playerCamera;                 // reference target camera to get interaction manager script
    InteractionManager playerTargetScript;

    public GameObject currentSelectedFlame;                 // to reference current temporary flame


    // Update is called once per frame
    void Update()
    {

    }

    void Start()
    {
        tempFlames.Add(amethystFlame);
        tempFlames.Add(dragonsEyeFlame);
        tempFlames.Add(emeraldFlame);
        tempFlames.Add(sapphireFlame);
        tempFlames.Add(topazFlame);
        tempFlames.Add(jasperFlame);

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

    public void SubmitTorch(int itemId)
    {
        Debug.Log("submit");
        
        if(gemsOrder[counter] == itemId)
        {
            Debug.Log(counter);
            counter += 1;
            if(counter == 5)
            {
                puzzleAccomplished = true;
            }
        }

        else
        {
            for(int i = 0; i < 6; i++)
            {
                Debug.Log(i);
                //correctFlames[i].SetActive(false);
                torches[i].GetComponent<TorchPuzzle>().ResetTorch();
                counter = 0;

            }
        }
    }

    public void ResetTempFlames()
    {
        for (int i = 0; i < 6; i++)
        {
            tempFlames[i].SetActive(false);
        }
    }
}
