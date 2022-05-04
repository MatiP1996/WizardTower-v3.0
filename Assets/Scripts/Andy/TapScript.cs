using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScript : MonoBehaviour
{
    public PickUpObject pickUpObjectInstance;
    public GameObject wateringCanLocation;
    private GameObject theWateringCan;
    public AudioSource popSound;
    public AudioSource tapSound;
    public GameObject rayCastStart;

    void Update()
    {

        //Debug.DrawRay(rayCastStart.transform.position, gameObject.transform.forward);


        /// IF PLAYER INTERACTS WITH TAP, PLAY THE WATER PARTICLE EFFECT. IF THE WATERING CAN IS UNDERNEAT THE TAP, FILL THE CAN WITH WATER
        if (Input.GetKeyDown(KeyCode.E)) // when E pressed
        {
            if (CameraRaycast.currentHitInteractable) // if looking at an interactable
            {
                if (CameraRaycast.currentHitInteractable.gameObject == gameObject) // if looking at this gameObject (the tap)
                {
                    gameObject.GetComponentInChildren<ParticleSystem>().Play(); // play the water particle system
                    tapSound.Play(); // play the tap sound

                    Vector3 worldRayStart = gameObject.GetComponentInChildren<ParticleSystem>().gameObject.transform.position; // set ray start pos

                    Ray ray = new Ray(worldRayStart, -gameObject.transform.up*2);


                    //Debug.Log(("rayyyyyy"));

                    //Debug.DrawRay(rayCastStart.transform.position,-gameObject.transform.up*3,Color.cyan,5);

                    RaycastHit raycastHit;

                    if (Physics.Raycast(ray, out raycastHit, 2)) // raycast
                    {
                        if (raycastHit.transform.gameObject.tag == "wateringCan") // if ray hits the watering can
                        {  
                            raycastHit.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true; // set water to visible
                        }
                    }
                }
            }
        }
    }


    // if watering can enters tap trigger area, snap the watering can to underneath the tap
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
