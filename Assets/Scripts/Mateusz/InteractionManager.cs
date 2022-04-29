using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionManager : MonoBehaviour
{
    public Text textObject;         // on screen text

    public LayerMask interactMask;   // target mask for rayacast

    GameObject currentSelectedObject;        // connecting + resetting objects
    GameObject previousSelectedObject;

    InteractionParent currentlySelectedInteraction;      //   connecting + resetting interactions
    InteractionParent previouslySelectedInteraction;

    public float raycastLength = 2f;        // raycast distance

    public List<int> itemIDs;              // player collected items
    Camera cameraObject;                      // target camera

    public GameObject playerCandle;         // on screen candle 

    public bool candleActive;               // player holding candle
    public bool flameActive;
    // Start is called before the first frame update
    void Start()
    {
                        // initialise variables
        itemIDs = new List<int> { };            
        textObject.text = "";
        cameraObject = gameObject.GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {               // core functions
        RaycastingAndText();    
        ActivateCandle();
    }

    void ActivateCandle()       // check if player collected candle + flame (inventory)
    {
        if (!candleActive)
        {
            if (itemIDs.Contains(-1))            // if so enable candle object
            {
                candleActive = true;
                playerCandle.SetActive(true);
            }
        }
        else if(!flameActive)       // if candle activated  >>  check flame
        {
            if (itemIDs.Count > 1)
            {
                flameActive = true;
            }
        }
    }

    void RaycastingAndText()
    {
        Ray ray = cameraObject.ScreenPointToRay(Input.mousePosition);   // execute raycast + initialise hit
        RaycastHit hit;             
        previousSelectedObject = currentSelectedObject;                 // store previous raycast hit object

        if(previousSelectedObject != null)                    // if recent object present  >>  pass previous interaction script      
        {
            previouslySelectedInteraction = currentlySelectedInteraction;
        }

        if (Physics.Raycast(ray, out hit, raycastLength, interactMask))             // if raycast successfully connects with interaction layer mask...
        {
            currentSelectedObject = hit.transform.gameObject;                                       // take the object reference
            currentlySelectedInteraction = currentSelectedObject.GetComponent<InteractionParent>();     // access the interaction script
            string message = currentlySelectedInteraction.Communicate();                // access the text message
            textObject.text = message;                                              // update the interface

            //activate interaction

            if (Input.GetKeyDown("e"))                                      // when player presses E  >>  interaction script activate + modify player items
            {
                itemIDs = currentlySelectedInteraction.Activate(itemIDs);
            }

        }
        else                                                                       // otherwise reset interface + current object
        {
            currentSelectedObject = null;
            textObject.text = "";
        }

        if (previousSelectedObject != currentSelectedObject)                        // if recent object and current object are not the same...
        {
            if(previouslySelectedInteraction != null)                                   // AND if previous interaction script is not cleared...
            {
                previouslySelectedInteraction.ResetState();                         // reset recent interaction
                previouslySelectedInteraction = null;                               // clear the reference
            }

        }
    }
}
