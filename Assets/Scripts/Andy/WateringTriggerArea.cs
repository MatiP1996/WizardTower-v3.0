using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringTriggerArea : MonoBehaviour
{
    public GameObject wateringCan;
    public GameObject growVine;
    public bool isWateringPlant = false;
    public bool wateringCanDetected = false;
    public bool slurpSoundPlaying = false;
    public AudioSource slurpSound;
    
    void Update()
    {
        if (wateringCanDetected == true) // if watering can is in collider
        {
            if (wateringCan.GetComponent<WateringCan>().isWatering == true) // if the player is currently watering
            {
                if (slurpSoundPlaying == false) // play the sound if not already playing
                {
                    slurpSound.Play();
                    slurpSoundPlaying = true;
                }

                growVine.GetComponent<GrowVine>().growValue += Time.deltaTime / 10; // increase the vine grow amount
            }
        }
    }

    private void OnTriggerEnter(Collider collider) // detects the watering can entering collider
    {
        if (collider.gameObject == wateringCan)
        {
            wateringCanDetected = true;
        }
    }


    private void OnTriggerExit(Collider collider) // detects the watering can exiting collider
    {
        if (collider.gameObject == wateringCan)
        {
            wateringCanDetected = false;
        }
    }

}
