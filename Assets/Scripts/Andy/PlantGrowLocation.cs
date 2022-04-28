using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowLocation : MonoBehaviour
{
    public PickUpObject pickUpObjectInstance;
    private GameObject thePlantedPlant;
    public GameObject preGrowVine;
    public AudioSource popSound;
    public GameObject growVine;


    public static bool isTheVinePlanted = false;


    // if plant enters the grow location, snap it to the centre of the grow location
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "plant")
        {
            if (PlantScript.isPlantEnchanted == true)
            {
                preGrowVine.layer = 0;
                growVine.GetComponent<GrowVine>().growValue = 0;
            }
            

           
            

            isTheVinePlanted = true;

            if (PickUpObject.isAnItemCurrentlyPickedUp) // drop object if its currently picked up
            {
               pickUpObjectInstance.PickUp();
            }

            thePlantedPlant = other.gameObject.transform.parent.gameObject;
            thePlantedPlant.GetComponent<Rigidbody>().isKinematic = true;
            thePlantedPlant.GetComponent<Rigidbody>().MovePosition(this.transform.position);
            thePlantedPlant.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0,0,0));
            popSound.Play();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "plant")
        {
            isTheVinePlanted = false;


        }
    }

}
