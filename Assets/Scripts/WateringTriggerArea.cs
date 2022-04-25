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




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wateringCanDetected == true) // if watering can is in collider
        {
            if (wateringCan.GetComponent<WateringCan>().isWatering == true) // if the player is currently watering
            {
                if (slurpSoundPlaying == false)
                {
                    slurpSound.Play();
                    slurpSoundPlaying = true;
                }

                growVine.GetComponent<GrowVine>().growValue += Time.deltaTime / 10;
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
