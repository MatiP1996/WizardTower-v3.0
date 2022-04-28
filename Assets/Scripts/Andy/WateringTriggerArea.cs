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
    private GrowVine growVineScript;


    void Start()
    {
        growVineScript = growVine.GetComponent<GrowVine>();
    }

    void Update()
    {
        if (wateringCanDetected == true) // if watering can is in collider
        {
            if (wateringCan.GetComponent<WateringCan>().isWatering == true) // if the player is currently watering
            {
                if (PlantScript.isPlantEnchanted == true && PlantGrowLocation.isTheVinePlanted == true) // if the vine is enchanted and is also planted
                {
                    if (slurpSoundPlaying == false) // play the sound if not already playing
                    {
                        slurpSound.Play();
                        slurpSoundPlaying = true;
                    }

                    if (growVineScript.growValue < 0.999f) // if grow vine isn't fully grown
                    {
                        growVineScript.growValue += Time.deltaTime / 15; // increase the vine grow amount

                        if (growVineScript.growValue > 0.999f) // if grow value is 1 or greater, restrict to 0.999f (to stop errors in vine grow script)
                        {
                            growVineScript.growValue = 0.999f;
                        }
                    }
                }
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
