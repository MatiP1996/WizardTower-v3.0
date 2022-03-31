using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomFloorPuzzle : MonoBehaviour
{
    public int counter = 0;
    public List<int> gemsOrder = new List<int> { 1, 2, 3, 4, 5, 6 };        // gem ids list             //  riddle text list
    public List<string> riddleSentences = new List<string> { "Lavender Amethyst", "Chocolate Dragons Eye", "Grass Emerald", "Strawberry Jasper", "Ocean Sapphire", "Lemon Topaz" };  
    
    public GameObject targetNote;           // note to contain the riddle

    public bool puzzleAccomplished;         // boolean whether puzzle has been finished


    public GameObject amethystFlame;            // reference all the temporary flames
    public GameObject dragonsEyeFlame;
    public GameObject emeraldFlame;
    public GameObject sapphireFlame;
    public GameObject topazFlame;
    public GameObject jasperFlame;

    List<GameObject> tempFlames = new List<GameObject>();       // list of all temporary flames
    public List<GameObject> torches;


    public GameObject playerCamera;                 // reference target camera to get interaction manager script


    public GameObject currentSelectedFlame;                 // to reference current temporary flame#

    public List<GameObject> torchLocations;
    public List<GameObject> candleLocations;


    // Update is called once per frame

    void Start()
    {               // randomization of the puzzle...
        ShuffleTorchLocations(torchLocations);
        ShuffleCandleLocations(candleLocations);            
        RandomizeRiddle(riddleSentences, gemsOrder);

        tempFlames.Add(amethystFlame);              // prepare temporary flames the list  >>  append all items
        tempFlames.Add(dragonsEyeFlame);
        tempFlames.Add(emeraldFlame);
        tempFlames.Add(sapphireFlame);
        tempFlames.Add(topazFlame);
        tempFlames.Add(jasperFlame);

        ResetTempFlames();          // set all the temporary flames inactive

    }

    public void SubmitFlame(GameObject temporaryFlame)          
    {
        currentSelectedFlame = temporaryFlame;
    }

    public void SubmitTorch(int itemId)         // function to let activated torches pass the flame id
    {

        if (gemsOrder[counter] == itemId)           // if correct flame is activated  >>  proceed to next stage of puzzle...
        {
            counter += 1;                           // increment
            if (counter == 5)                       // final increment  >>  puzzle successful
            {
                puzzleAccomplished = true;
            }
        }

        else                                      // otherwise  >>  reset the puzzle
        {
            for (int i = 0; i < torches.Count; i++)             // iterate through torches
            {
                torches[i].GetComponent<TorchPuzzle>().ResetTorch();   // reset all torches         
            }
            counter = 0;            // reset the puzzle  >>  set the stage to start
        }
    }

    public void ResetTempFlames()                   // setting all temporary flames inactive
    {
        for (int i = 0; i < tempFlames.Count; i++)
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
        int randomNum;                      // number to generate
        int tempId;                         // temporary placeholders...
        string tempText;

        for(int i = 0; i < gemsOrder.Count; i++)                // loop to iterate through gemsOrder list
        {
            randomNum = Random.Range(0, gemsOrder.Count);                 // generate number

            tempId = gemsOrder[randomNum];              // shuffle id order
            gemsOrder[randomNum] = gemsOrder[i];
            gemsOrder[i] = tempId;

            tempText = riddleText[randomNum];               // shuffle text order
            riddleText[randomNum] = riddleText[i];
            riddleText[i] = tempText;

        }

        string finalString = "";                        // string to push to the note

        for(int i = 0; i < riddleText.Count; i++)               // loop to iterate through the riddle string list
        {
            finalString += riddleText[i];                       // append riddle text

            if(i == riddleText.Count-1)                     // final iteration...
            {
                InteractionParent targetScript = targetNote.GetComponent<InteractionParent>();      // access riddle script and push the string
                targetScript.alternateMessage = finalString;
            }
            else
            {
                finalString += "\n";                    // finalise the line of the string
            }
        }
    }
}