using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionManager : MonoBehaviour
{
    public Text textObject;         // on screen text

    public LayerMask interactMask;   // target mask for rayacast

    public GameObject currentSelectedObject;        // connecting + resetting objects
    public GameObject previousSelectedObject;

    InteractionParent currentlySelectedInteraction;      //   connecting + resetting interactions
    InteractionParent previouslySelectedInteraction;

    public float raycastLength = 2f;        // raycast distance

    public List<int> itemIDs;              // player collected items
    Camera cameraObject;                      // target camera

    public GameObject playerCandle;         // on screen candle 

    public bool candleActive;               // player holding candle
    public bool flameActive;

    public float timeMoveEnable;
    MouseLook mouseControl;
    PlayerMove moveControl;
    bool controlDisabled;

    // Start is called before the first frame update
    void Start()
    {
                        // initialise variables
        itemIDs = new List<int> { };            
        textObject.text = "";
        cameraObject = gameObject.GetComponent<Camera>();

        mouseControl = GameObject.Find("Main Camera").GetComponent<MouseLook>();
        moveControl = GameObject.Find("FirstPersonPlayer").GetComponent<PlayerMove>();

    }

    // Update is called once per frame
    void Update()
    {               // core functions
        RaycastingAndText();    
        ActivateCandle();
        EnableMove();
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
            Debug.Log("anything");
            currentSelectedObject = hit.transform.gameObject;                                       // take the object reference
            currentlySelectedInteraction = currentSelectedObject.GetComponent<InteractionParent>();     // access the interaction script
            string message = currentlySelectedInteraction.Communicate();                // access the text message
            textObject.text = message;                                              // update the interface

            //activate interaction

            if (Input.GetKeyDown("e"))                                      // when player presses E  >>  interaction script activate + modify player items
            {
                timeMoveEnable = currentlySelectedInteraction.Activate();
                timeMoveEnable += Time.time;

                mouseControl.enabled = false;
                moveControl.enabled = false;
                controlDisabled = true;
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

    void EnableMove()
    {
        if(controlDisabled)
        {
            if (Time.time >= timeMoveEnable)
            {

                mouseControl.enabled = true;
                moveControl.enabled = true;

                controlDisabled = false;
            }
        }
    }
}
