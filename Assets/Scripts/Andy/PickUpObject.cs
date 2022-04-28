using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public static bool isAnItemCurrentlyPickedUp = false;
    public static GameObject itemCurrentlyPickedUp;

    Vector3 locationToMovePickedUpItemTo;
    float originalYAxisRotationOfPickedUpObject;
    float yAxisRotationOfPlayerUponPickup;
    float newYAxisRotationForPickedUpObject;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) /// WHEN E PRESSED AND THE PLAYER IS LOOKING AT SOMETHING THAT CAN BE PICKED UP, SET THE OBJECT AS PICKED UP
        {
            PickUp();
        }        
    }

    private void FixedUpdate()
    {
        if (isAnItemCurrentlyPickedUp == true) /// IF AN ITEM IS CURRENTLY PICKED UP, UPDATE ITS LOCATION AND ROTATION IN FRONT OF THE PLAYER
        {
            if (itemCurrentlyPickedUp.tag == "wateringCan")
            {
                locationToMovePickedUpItemTo = gameObject.transform.position + gameObject.transform.forward * 1f + gameObject.transform.right * 0.5f + gameObject.transform.up * -0.2f; // calculates the location the picked up item should be at
                itemCurrentlyPickedUp.GetComponent<Rigidbody>().MovePosition(locationToMovePickedUpItemTo); // moves the item to the above calculated location
                itemCurrentlyPickedUp.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, 0)); // rotates the picked up item to match the rotation of the player
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

    public void PickUp()
    {
        if (isAnItemCurrentlyPickedUp == false) // if nothing currently picked up
        {
            if (CameraRaycast.currentHitInteractable != null) // if player is currently looking at an interactable
            {
                if (CameraRaycast.currentHitInteractable.GetComponent<Rigidbody>()) // if the interactable has a rigidbody, set it as picked up etc
                {
                    itemCurrentlyPickedUp = CameraRaycast.currentHitInteractable;
                    itemCurrentlyPickedUp.GetComponent<Rigidbody>().isKinematic = true;
                    isAnItemCurrentlyPickedUp = true;
                    originalYAxisRotationOfPickedUpObject = itemCurrentlyPickedUp.transform.rotation.y;
                    yAxisRotationOfPlayerUponPickup = gameObject.transform.rotation.eulerAngles.y;
                }
            }
        }
        else // if something is currently picked up, drop the item etc
        {
            itemCurrentlyPickedUp.GetComponent<Rigidbody>().isKinematic = false;
            isAnItemCurrentlyPickedUp = false;
            itemCurrentlyPickedUp = null;
        }
    }
}
