using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public static RaycastHit raycastHit;
    [SerializeField] LayerMask LayerMask = 0;
    public static Ray ray = new Ray();
    public static Vector3 cameraOrigin;
    public static Vector3 cameraDirection;
    public static GameObject currentHitInteractable;  // the interactable currently being looked at
    public static GameObject priorHitInteractable;    // the interactable hit before the "currentHitInteractable"


    GameObject selectedGameObject;
    Painting paintingScript;
    GameObject player;
    Camera playerCam;
    CamToTele camTele;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = player.transform.GetChild(1).GetComponent<Camera>();
        camTele = player.transform.GetChild(1).GetComponent<CamToTele>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraOrigin = gameObject.transform.position;          // sets the origin of the ray cast
        cameraDirection = gameObject.transform.forward * 3;    // sets the direction of the ray cast

        ray = new Ray(cameraOrigin, cameraDirection);

        /// UPDATES "currentHitInteractable" WITH WHATEVER INTERACTABLE THE PLAYER IS LOOKING AT, IF PLAYER ISN'T LOOKING AT AN INTERACTABLE, SET "currentHitInteractable" TO NULL ///
        /// 

        if (Physics.Raycast(ray, out raycastHit ,3) && Input.GetKeyDown(KeyCode.E))
        {
            selectedGameObject = raycastHit.transform.gameObject;
            if (raycastHit.transform.gameObject.tag == "Painting")
            {
                Debug.Log("HIt painting");
                selectedGameObject = raycastHit.transform.gameObject;

                paintingScript = selectedGameObject.GetComponent<Painting>();
                Debug.Log(paintingScript);
                
                paintingScript.InteractPainting();
            }
            else if (selectedGameObject.tag == "TeleScope")
            {
                Debug.Log("hit tele");

                playerCam.enabled = false;

                //freeCam.eulerAngles = new Vector3(freeCam.eulerAngles.x, freeCam.eulerAngles.y + 180, freeCam.eulerAngles.z);
                camTele.GoToTele();

            }
        }

        if (Physics.Raycast(ray, out raycastHit, 3, LayerMask))  // if something is hit AND the object hit is of the layer specified (layer specified is currently "interactable")
        {
            if (raycastHit.collider.gameObject != currentHitInteractable)
            {
                priorHitInteractable = currentHitInteractable;

                if (priorHitInteractable != null) // if the prior hit interactable isn't null, set its emission back to 0
                {
                    for (int i = 0; i < priorHitInteractable.GetComponent<Renderer>().materials.Length; i++) // for each material attached to "currentHitInteractable" set the emission value to 0
                    {
                        priorHitInteractable.GetComponent<Renderer>().materials[i].SetFloat("emissionIntensity", 0);
                    }
                }
            }
            
            currentHitInteractable = raycastHit.collider.gameObject;

            for (int i = 0; i < currentHitInteractable.GetComponent<Renderer>().materials.Length; i++) // for each material attached to "currentHitInteractable" set the emission value to 0.2
            {
                currentHitInteractable.GetComponent<Renderer>().materials[i].SetFloat("emissionIntensity", 0.2f);
            }
        }
        else // if item hit isn't an interactable object, set "currentHitInteractable" to null
        {
            priorHitInteractable = currentHitInteractable;
            currentHitInteractable = null;

            if (priorHitInteractable != null) // if the prior hit interactable isn't null, set its emission back to 0
            {
                for (int i = 0; i < priorHitInteractable.GetComponent<Renderer>().materials.Length; i++) // for each material attached to "currentHitInteractable" set the emission value to 0
                {
                    priorHitInteractable.GetComponent<Renderer>().materials[i].SetFloat("emissionIntensity", 0);
                }
            }
        }
    }
}
