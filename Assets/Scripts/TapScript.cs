using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScript : MonoBehaviour
{
    public PickUpObject pickUpObjectInstance;
    public GameObject wateringCanLocation;

    private GameObject theWateringCan;
    public AudioSource popSound;

   // public Collider colliderr;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // when E pressed
        {
            if (CameraRaycast.currentHitInteractable)
            {
                if (CameraRaycast.currentHitInteractable.gameObject == gameObject) // if looking at this gameObject
                {
                    gameObject.GetComponentInChildren<ParticleSystem>().Play(); // play the water particle system

                    Vector3 worldRayStart = gameObject.GetComponentInChildren<ParticleSystem>().gameObject.transform
                        .position; // set ray start pos

                    Ray ray = new Ray(worldRayStart, -gameObject.transform.forward);
                    RaycastHit raycastHit;

                    if (Physics.Raycast(ray, out raycastHit, 2)) // raycast
                    {
                        if (raycastHit.transform.gameObject.tag == "wateringCan") // if ray hits the watering can
                        {
                            raycastHit.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled =
                                true; // set water to visible
                        }
                    }
                }
            }
        }
    }


    public void WateringCanSocket(Collider other)
    {
        if (other.gameObject.tag == "wateringCan")
        {
            if (PickUpObject.isAnItemCurrentlyPickedUp)
            {
                pickUpObjectInstance.PickUp();
            }

            theWateringCan = other.gameObject;

            theWateringCan.GetComponent<Rigidbody>().isKinematic = true;
            theWateringCan.GetComponent<Rigidbody>().MovePosition(wateringCanLocation.transform.position);
            theWateringCan.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, -90, 0));
            popSound.Play();
        }
    }
}
