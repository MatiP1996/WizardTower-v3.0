using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowLocation : MonoBehaviour
{
    public PickUpObject pickUpObjectInstance;
    private bool treePlanted = false;
    private GameObject thePlantedPlant;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "plant")
        {
            if (PickUpObject.isAnItemCurrentlyPickedUp)
            {
               pickUpObjectInstance.PickUp();
            }

            thePlantedPlant = other.gameObject.transform.parent.gameObject;

            thePlantedPlant.GetComponent<Rigidbody>().isKinematic = true;
            thePlantedPlant.GetComponent<Rigidbody>().MovePosition(this.transform.position);
            thePlantedPlant.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0,-90,0));
        }
    }
}
