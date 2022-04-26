using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantmentTableSocket : MonoBehaviour
{
    public PickUpObject pickUpObjectInstance;
    public AudioSource popSound;
    private GameObject thePlant;


    private void OnTriggerEnter(Collider other) // when the plant enters the trigger area, set its location to the centre of the enchantment table
    {
        if (other.tag == "plant")
        {
            if (PickUpObject.isAnItemCurrentlyPickedUp)
            {
                pickUpObjectInstance.PickUp();
            }

            thePlant = other.gameObject.transform.parent.gameObject;

            thePlant.GetComponent<Rigidbody>().isKinematic = true;
            thePlant.GetComponent<Rigidbody>().MovePosition(this.transform.position);
            thePlant.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, 180, 0));
            popSound.Play();
        }
    }
}
