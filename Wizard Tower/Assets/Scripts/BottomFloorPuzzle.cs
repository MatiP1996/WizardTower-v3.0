using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomFloorPuzzle : MonoBehaviour
{
    // amethyst  dragonseye emerald sapphire topaz
    // Amethyst - Dragon's Eye - Emerald - Jasper - Sapphire - Topaz
    public int counter = 0;
    public List<int> gemsOrder = new List<int> { 1, 2, 3, 4, 5, 6 };
    public List<string> riddleSentences = new List<string> { "Lavender Amethyst", "Chocolate Dragons Eye", "Grass Emerald", "Strawberry Jasper", "Ocean Sapphire", "Lemon Topaz" };
    
    public GameObject targetNote;


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
  //  InteractionManager playerTargetScript;

    public GameObject currentSelectedFlame;                 // to reference current temporary flame#

    public List<GameObject> torchLocations;
    public List<GameObject> candleLocations;


    // Update is called once per frame
    void Update()
    {

    }

    void Start()
    {
        ShuffleCandleLocations(candleLocations);
        ShuffleTorchLocations(torchLocations);
        RandomizeRiddle(riddleSentences, gemsOrder);

        tempFlames.Add(amethystFlame);
        tempFlames.Add(dragonsEyeFlame);
        tempFlames.Add(emeraldFlame);
        tempFlames.Add(sapphireFlame);
        tempFlames.Add(topazFlame);
        tempFlames.Add(jasperFlame);

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

        if (gemsOrder[counter] == itemId)
        {
            Debug.Log(counter);
            counter += 1;
            if (counter == 5)
            {
                puzzleAccomplished = true;
            }
        }

        else
        {
            for (int i = 0; i < 6; i++)
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

    void ShuffleCandleLocations(List<GameObject> objects)
    {
        int randomNum;                //    number to generate
        Vector3 tempLocation;         //     target locations
        Vector3 targetlocation;
        for (int i = 0; i < objects.Count; i++)         // loop based on the length of candle number
        {
            targetlocation = objects[i].transform.position;                     // extract location

            randomNum = Random.Range(0, objects.Count);                 // generate number
            tempLocation = objects[randomNum].transform.position;           // extract location

            objects[i].transform.position = tempLocation;               //  set the locations
            objects[randomNum].transform.position = targetlocation;
        }
    }

    void ShuffleTorchLocations(List<GameObject> objects)
    {
        int randomNum;                //    number to generate
        Vector3 tempLocation;         //     target locations
        Vector3 targetlocation;

        Vector3 tempRotation;           // target locationst
        Vector3 targetRotation;
        for (int i = 0; i < objects.Count; i++)         // loop based on the length of candle number
        {
            randomNum = Random.Range(0, objects.Count);                 // generate number

            targetlocation = objects[i].transform.position;                     // extract location
            tempLocation = objects[randomNum].transform.position;          

            targetRotation = objects[i].transform.rotation.eulerAngles;         // extract rotations
            tempRotation = objects[randomNum].transform.rotation.eulerAngles;

            objects[i].transform.position = tempLocation;               //  set the locations
            objects[randomNum].transform.position = targetlocation;

            objects[i].transform.transform.eulerAngles = tempRotation;          // set rotations
            objects[randomNum].transform.transform.eulerAngles = targetRotation;

        }
    }

    void RandomizeRiddle(List<string> riddleText, List<int> gemsOrder)
    {
        int randomNum;
        int tempId;
        string tempText;

        for(int i = 0; i < gemsOrder.Count; i++)
        {
            randomNum = Random.Range(0, gemsOrder.Count);                 // generate number

            tempId = gemsOrder[randomNum];              // shuffle id order
            gemsOrder[randomNum] = gemsOrder[i];
            gemsOrder[i] = tempId;

            tempText = riddleText[randomNum];
            riddleText[randomNum] = riddleText[i];
            riddleText[i] = tempText;

        }

        string finalString = "";

        for(int i = 0; i < riddleText.Count; i++)
        {
            finalString += riddleText[i];

            if(i == riddleText.Count-1)
            {
                InteractionParent targetScript = targetNote.GetComponent<InteractionParent>();
                targetScript.alternateMessage = finalString;
            }
            else
            {
                finalString += "\n";
            }
        }
    }
}