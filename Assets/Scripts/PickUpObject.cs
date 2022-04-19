using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    bool isAnItemCurrentlyPickedUp = false;
    GameObject itemCurrentlyPickedUp;

    Vector3 locationToMovePickedUpItemTo;
    float originalYAxisRotationOfPickedUpObject;
    float yAxisRotationOfPlayerUponPickup;
    float newYAxisRotationForPickedUpObject;
    public GameObject wand;
    public Component wandScript;


    private void Start()
    {
        wandScript = wand.GetComponent<WandInnit>(); // gets the script attached to the wand
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) /// WHEN LMB IS PRESSED AND THE PLAYER IS LOOKING AT SOMETHING THAT CAN BE PICKED UP, SET THE OBJECT AS PICKED UP
        {
            if (isAnItemCurrentlyPickedUp == false)
            {
                if (CameraRaycast.currentHitInteractable != null) // if player is currently looking at an interactable
                {
                    itemCurrentlyPickedUp = CameraRaycast.currentHitInteractable;
                    itemCurrentlyPickedUp.GetComponent<Rigidbody>().isKinematic = true;
                    isAnItemCurrentlyPickedUp = true;
                    originalYAxisRotationOfPickedUpObject = itemCurrentlyPickedUp.transform.rotation.y;
                    yAxisRotationOfPlayerUponPickup = gameObject.transform.rotation.eulerAngles.y;

                    if (itemCurrentlyPickedUp.tag == "Wand")
                    {
                        WandInnit.isPickedUp = true;
                    }
                }
            }
            else
            {
                if (itemCurrentlyPickedUp.tag == "Wand")
                {
                    WandInnit.isPickedUp = false;
                }

                itemCurrentlyPickedUp.GetComponent<Rigidbody>().isKinematic = false;
                isAnItemCurrentlyPickedUp = false;
                itemCurrentlyPickedUp = null;                
            }            
        }        
    }
    private void FixedUpdate()
    {
        if (isAnItemCurrentlyPickedUp == true) /// IF AN ITEM IS CURRENTLY PICKED UP, UPDATE ITS LOCATION AND ROTATION IN FRONT OF THE PLAYER
        {
            if (itemCurrentlyPickedUp.tag == "Wand")
            {                
                locationToMovePickedUpItemTo = gameObject.transform.position + gameObject.transform.forward * 1f + gameObject.transform.right * 0.5f + gameObject.transform.up * - 0.2f; // calculates the location the picked up item should be at
                itemCurrentlyPickedUp.GetComponent<Rigidbody>().MovePosition(locationToMovePickedUpItemTo); // moves the item to the above calculated location

                newYAxisRotationForPickedUpObject = originalYAxisRotationOfPickedUpObject + gameObject.transform.rotation.eulerAngles.y - yAxisRotationOfPlayerUponPickup; //calculates the y rotation the picked up item should be at
                itemCurrentlyPickedUp.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x + 70 , gameObject.transform.rotation.eulerAngles.y, 0)); // rotates the picked up item to match the rotation of the player

            }
            else
            {
                locationToMovePickedUpItemTo = gameObject.transform.position + gameObject.transform.forward * 1.5f; // calculates the location the picked up item should be at
                itemCurrentlyPickedUp.GetComponent<Rigidbody>().MovePosition(locationToMovePickedUpItemTo); // moves the item to the above calculated location

                newYAxisRotationForPickedUpObject = originalYAxisRotationOfPickedUpObject + gameObject.transform.rotation.eulerAngles.y - yAxisRotationOfPlayerUponPickup; //calculates the y rotation the picked up item should be at
                itemCurrentlyPickedUp.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(itemCurrentlyPickedUp.transform.rotation.eulerAngles.x, newYAxisRotationForPickedUpObject, itemCurrentlyPickedUp.transform.rotation.eulerAngles.z)); // rotates the picked up item to match the rotation of the player
            }
        }
    }
}
