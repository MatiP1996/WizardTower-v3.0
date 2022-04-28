using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionManager : MonoBehaviour
{
    public Text textObject;
    public Transform freeCam;

    ToggleCamera cameraScript;
    public GameObject selectedObject;
    public List<int> ItemIDs;
    Painting paintingScript;
    Camera playerCam;
    CamToTele camTele;
    GameObject player;

    CharacterController controllerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        cameraScript = player.GetComponent<ToggleCamera>();

        controllerScript = player.GetComponent<CharacterController>();

        camTele = gameObject.GetComponent<CamToTele>();
        playerCam = player.transform.GetChild(1).GetComponent<Camera>();

        ItemIDs = new List<int> { };

        textObject.text = "";

    }

    public void leaveTelescope()
    {
        
        freeCam.gameObject.GetComponent<Camera>().fieldOfView = 30f;

        playerCam.enabled = true;
        freeCam.gameObject.GetComponent<Camera>().enabled = false;
        
        freeCam.eulerAngles = new Vector3(freeCam.eulerAngles.x, 18f, freeCam.eulerAngles.z);

        Debug.Log("leavingTelescope");

        player.GetComponent<Rigidbody>().isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {

        Camera CameraObject = gameObject.GetComponent<Camera>();
        Ray ray = CameraObject.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 4f))
        {
            selectedObject = hit.transform.gameObject;
            if (selectedObject.tag == "Painting" && Input.GetKeyDown(KeyCode.E))
            {

                selectedObject = selectedObject.gameObject;
                paintingScript = selectedObject.GetComponent<Painting>();

                //Debug.Log(selectedObject.transform.parent);

                paintingScript.InteractPainting(selectedObject);


            }
            else if (selectedObject.tag == "TeleScope" && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("hit tele");
                freeCam.gameObject.GetComponent<Camera>().fieldOfView = 60f;
                freeCam.transform.position = playerCam.transform.position;
                freeCam.gameObject.GetComponent<Camera>().enabled = true;
                freeCam.gameObject.SetActive(true);

                playerCam.enabled = false;

                freeCam.eulerAngles = new Vector3(freeCam.eulerAngles.x, freeCam.eulerAngles.y + 180, freeCam.eulerAngles.z);
                camTele.GoToTele();
                
            }
            else
            {
                textObject.text = "";
            }

        }
        else
        {
            textObject.text = "";
        }
    }
}