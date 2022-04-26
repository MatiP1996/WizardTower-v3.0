using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = Unity.Mathematics.Random;

public class EnchantingTable : MonoBehaviour
{
    public GameObject orb1;
    public GameObject orb2;
    public GameObject orb3;

    public Transform orb1StartPos;
    public Transform orb2StartPos;
    public Transform orb3StartPos;

    public Transform orb1FloatingPosition;
    public Transform orb2FloatingPosition;
    public Transform orb3FloatingPosition;

    Vector3 orb1MovePos;
    Vector3 orb2MovePos;
    Vector3 orb3MovePos;

    public GameObject orb1Light;
    public GameObject orb2Light;
    public GameObject orb3Light;

    bool isOrb1Pulsing = false;
    bool isOrb2Pulsing = false;
    bool isOrb3Pulsing = false;

    float orb1PulsingTimer = 0;
    float orb2PulsingTimer = 0;
    float orb3PulsingTimer = 0;

    bool orb1LightActive = false;
    bool orb2LightActive = false;
    bool orb3LightActive = false;

    float orbLightMaxintensity = 2;
    float pulseProportionComplete = 0;
    float pulseLengthSeconds = 0.7f;
    bool lightSequencePLaying = false;
    float lightSequenceTimer = 0;
    bool playerSequenceInputInProgress = false;
    float playerSequenceInputTimer = 0;

    bool areOrbsFloating = false;
    bool areOrbsMovingUp = false;
    bool areOrbsMovingDown = false;
    float orbMoveTimer = 0;
    float orbMoveTimeSeconds = 1;
    float orbMoveProportionComplete = 0;
    
    public AudioSource correctSound;
    public AudioSource incorrectSound;
    public AudioSource correctPressSound;
    public AudioSource playSequenceSound;
    private bool errorJustMade = false;
    private float errorMadeTimer = 0;

    List<string> playerSequenceInputList = new List<string>();
    List<string> correctSequenceList = new List<string>();

    GameObject thePlantOnTheTable;
    public bool plantOnTable = false;

    float floatingBobUpAndDownTimer = 0;
    float orb1FloatingBobUpAndDownYVal = 0;
    float orb2FloatingBobUpAndDownYVal = 0;
    float orb3FloatingBobUpAndDownYVal = 0;

    public Light enchantmentSuccessfulLight;
    public bool enchantmentLightEvent = false;
    float enchantmentLightEventTimer = 0f;
    float enchantmentLightIntensity = 2f;

    private void Start()
    {
        SetRandomCorrectSequence();

        orb1MovePos = orb1.transform.position;
        orb2MovePos = orb2.transform.position;
        orb3MovePos = orb3.transform.position;
    }

    void Update()
    {
        // move orbs up and down over time to make them look like they are floating
        if (areOrbsFloating == true)
        {
            floatingBobUpAndDownTimer += Time.deltaTime;

            orb1FloatingBobUpAndDownYVal = Mathf.Sin(floatingBobUpAndDownTimer)/5000;
            orb2FloatingBobUpAndDownYVal = Mathf.Sin(floatingBobUpAndDownTimer + 120) / 5000;
            orb3FloatingBobUpAndDownYVal = Mathf.Sin(floatingBobUpAndDownTimer + 240) / 5000;

            orb1.transform.Translate(new Vector3(0, orb1FloatingBobUpAndDownYVal, 0));
            orb2.transform.Translate(new Vector3(0, orb2FloatingBobUpAndDownYVal, 0));
            orb3.transform.Translate(new Vector3(0, orb3FloatingBobUpAndDownYVal, 0));
        }

        // if there's a plant on table, set the orbs to float above the enchanting table. Else, set the orbs to move down
        if (plantOnTable == true)
        {
            if (areOrbsFloating == false)
            {
                areOrbsMovingUp = true;                
            }
        }
        else
        {
            if (areOrbsFloating == true)
            {
                areOrbsMovingDown = true;                
            }
        }

        // if an error just made by player, set a short timer to stop the new sequence being played immediately after
        if (errorJustMade == true) 
        {
            errorMadeTimer += Time.deltaTime;

            if (errorMadeTimer > 1.5f)
            {
                errorMadeTimer = 0;
                errorJustMade = false;
            }
        }
        
        // moves the orbs up and down
        if (areOrbsMovingUp == true)
        {
            orbMoveTimer += Time.deltaTime;
            
            if (orbMoveTimer > orbMoveTimeSeconds)
            {
                orbMoveTimer = 0;
                orbMoveProportionComplete = 1;
                areOrbsMovingUp = false;
                areOrbsFloating = true;
            }
            else
            {
                orbMoveProportionComplete = orbMoveTimer / orbMoveTimeSeconds;
            }

            orb1MovePos.y = Mathf.SmoothStep(orb1StartPos.position.y, orb1FloatingPosition.position.y, orbMoveProportionComplete);
            orb2MovePos.y = Mathf.SmoothStep(orb2StartPos.position.y, orb2FloatingPosition.position.y, orbMoveProportionComplete);
            orb3MovePos.y = Mathf.SmoothStep(orb3StartPos.position.y, orb3FloatingPosition.position.y, orbMoveProportionComplete);

            orb1.transform.position = orb1MovePos;
            orb2.transform.position = orb2MovePos;
            orb3.transform.position = orb3MovePos;
        }
        else if (areOrbsMovingDown == true)
        {
            orbMoveTimer += Time.deltaTime;

            if (orbMoveTimer > orbMoveTimeSeconds)
            {
                orbMoveTimer = 0;
                orbMoveProportionComplete = 1;
                areOrbsMovingDown = false;
                areOrbsFloating = false;
            }
            else
            {
                orbMoveProportionComplete = orbMoveTimer / orbMoveTimeSeconds;
            }

            orb1MovePos.y = Mathf.SmoothStep(orb1.transform.position.y, orb1StartPos.position.y, orbMoveProportionComplete);
            orb2MovePos.y = Mathf.SmoothStep(orb2.transform.position.y, orb2StartPos.position.y, orbMoveProportionComplete);
            orb3MovePos.y = Mathf.SmoothStep(orb3.transform.position.y, orb3StartPos.position.y, orbMoveProportionComplete);

            orb1.transform.position = orb1MovePos;
            orb2.transform.position = orb2MovePos;
            orb3.transform.position = orb3MovePos;
        }

        // play the light sequence 
        if (lightSequencePLaying == true) 
        {
            if (plantOnTable == false)
            {
                lightSequencePLaying = false;
            }

            if (areOrbsFloating == true && errorJustMade == false)
            {
                lightSequenceTimer += Time.deltaTime;

                if (lightSequenceTimer < pulseLengthSeconds * 1)
                {
                   PulseOrbAtCorrectSequenceIndex(0);
                }
                else if (lightSequenceTimer < pulseLengthSeconds * 2)
                {
                    PulseOrbAtCorrectSequenceIndex(1);
                }
                else if (lightSequenceTimer < pulseLengthSeconds * 3)
                {
                    PulseOrbAtCorrectSequenceIndex(2);
                }
                else if (lightSequenceTimer < pulseLengthSeconds * 4)
                {
                    PulseOrbAtCorrectSequenceIndex(3);
                }
                else if (lightSequenceTimer < pulseLengthSeconds * 5)
                {
                    PulseOrbAtCorrectSequenceIndex(4);
                }
                else if (lightSequenceTimer < pulseLengthSeconds * 6)
                {
                    PulseOrbAtCorrectSequenceIndex(5);
                }
                else if (lightSequenceTimer < pulseLengthSeconds * 7)
                {
                    PulseOrbAtCorrectSequenceIndex(6);
                }
                else if (lightSequenceTimer < pulseLengthSeconds * 8)
                {
                    PulseOrbAtCorrectSequenceIndex(7);
                }
                else if (lightSequenceTimer < pulseLengthSeconds * 9)
                {
                    lightSequencePLaying = false;
                    lightSequenceTimer = 0;
                }
            }
            else
            {
                lightSequenceTimer = 0;
            }
        }

        // the light pulse for orb 1
        if (isOrb1Pulsing == true)  
        {
            orb1PulsingTimer += Time.deltaTime;

            if (orb1LightActive == false)
            {
                orb1Light.GetComponent<Light>().enabled = true;
                orb1LightActive = true;
            }

            if (orb1PulsingTimer < pulseLengthSeconds + 0.01f)
            {
                pulseProportionComplete = 1 - orb1PulsingTimer / pulseLengthSeconds;
                orb1Light.GetComponent<Light>().intensity = orbLightMaxintensity * pulseProportionComplete;
                orb1.GetComponent<Renderer>().materials[0].SetFloat("emissionIntensity", pulseProportionComplete*3);
            }
            else
            {
                orb1Light.GetComponent<Light>().enabled = false;
                orb1PulsingTimer = 0;
                orb1LightActive = false;
                isOrb1Pulsing = false; 
            }
        }

        // the light pulse for orb 2
        if (isOrb2Pulsing == true)
        {
            orb2PulsingTimer += Time.deltaTime;

            if (orb2LightActive == false)
            {
                orb2Light.GetComponent<Light>().enabled = true;
                orb2LightActive = true;
            }

            if (orb2PulsingTimer < pulseLengthSeconds + 0.01f)
            {
                pulseProportionComplete = 1 - orb2PulsingTimer / pulseLengthSeconds;
                orb2Light.GetComponent<Light>().intensity = orbLightMaxintensity * pulseProportionComplete;
                orb2.GetComponent<Renderer>().materials[0].SetFloat("emissionIntensity", pulseProportionComplete*3);
            }
            else
            {
                orb2Light.GetComponent<Light>().enabled = false;
                orb2PulsingTimer = 0;
                orb2LightActive = false;
                isOrb2Pulsing = false;
            }
        }

        // the light pulse for orb 3
        if (isOrb3Pulsing == true)  
        {
            orb3PulsingTimer += Time.deltaTime;

            if (orb3LightActive == false)
            {
                orb3Light.GetComponent<Light>().enabled = true;
                orb3LightActive = true;
            }

            if (orb3PulsingTimer < pulseLengthSeconds + 0.01f)
            {
                pulseProportionComplete = 1 - orb3PulsingTimer / pulseLengthSeconds;
                orb3Light.GetComponent<Light>().intensity = orbLightMaxintensity * pulseProportionComplete;
                orb3.GetComponent<Renderer>().materials[0].SetFloat("emissionIntensity", pulseProportionComplete*3);
            }
            else
            {
                orb3Light.GetComponent<Light>().enabled = false;
                orb3PulsingTimer = 0;
                orb3LightActive = false;
                isOrb3Pulsing = false;
            }
        }

        // on press "E" add player orb selection to list, if incorrect reset list. If correct, check to see if entire sequence is completed, if so commence sequence correct functionality
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (lightSequencePLaying == false)
            {
                if (CameraRaycast.currentHitInteractable == orb1) // if looking at orb1
                {
                    if (playerSequenceInputInProgress == false) // set the player sequence input to true
                    {
                        playerSequenceInputInProgress = true;
                    }

                    playerSequenceInputList.Add("orb1");

                    int playerSequenceInputListCount = playerSequenceInputList.Count;

                    if (playerSequenceInputList[playerSequenceInputListCount-1] != correctSequenceList[playerSequenceInputListCount-1]) // if last player input is incorrect, rest player sequence and play "incorrect" sound
                    {
                        ResetPlayerSequenceInput();
                        SetRandomCorrectSequence();
                        errorJustMade = true;
                        lightSequencePLaying = true;
                    }
                    else if (plantOnTable == false) // if plant not on table reset player sequence, and play "incorrect" sound 
                    {
                        ResetPlayerSequenceInput();
                        SetRandomCorrectSequence();
                        errorJustMade = true;
                        lightSequencePLaying = true;
                    }
                    else // player input is correct and plant is on table
                    {
                        correctPressSound.pitch = 1 + 0.05f * playerSequenceInputListCount;
                        correctPressSound.Play();
                    }

                    isOrb1Pulsing = true;
                    orb1PulsingTimer = 0;
                    playerSequenceInputTimer = 0;
                    IsSequenceCorrect();
                }
                else if (CameraRaycast.currentHitInteractable == orb2) // if looking at orb2
                {
                    if (playerSequenceInputInProgress == false)
                    {
                        playerSequenceInputInProgress = true;
                    }

                    playerSequenceInputList.Add("orb2");

                    int playerSequenceInputListCount = playerSequenceInputList.Count;

                    if (playerSequenceInputList[playerSequenceInputListCount - 1] != correctSequenceList[playerSequenceInputListCount - 1]) // if last player input is incorrect, rest player sequence and play "incorrect" sound
                    {
                        SetRandomCorrectSequence();
                        ResetPlayerSequenceInput();
                        errorJustMade = true;
                        lightSequencePLaying = true;
                    }
                    else if (plantOnTable == false) // if plant not on table reset player sequence, and play "incorrect" sound 
                    {
                        SetRandomCorrectSequence();
                        ResetPlayerSequenceInput();
                        errorJustMade = true;
                        lightSequencePLaying = true;
                    }
                    else // player input is correct and plant is on table
                    {
                        correctPressSound.pitch = 1 + 0.05f * playerSequenceInputListCount;
                        correctPressSound.Play();
                    }

                    isOrb2Pulsing = true;
                    orb2PulsingTimer = 0;
                    playerSequenceInputTimer = 0;
                    IsSequenceCorrect();
                }
                else if (CameraRaycast.currentHitInteractable == orb3) // if looking at orb3
                {
                    if (playerSequenceInputInProgress == false)
                    {
                        playerSequenceInputInProgress = true;
                    }

                    playerSequenceInputList.Add("orb3");

                    int playerSequenceInputListCount = playerSequenceInputList.Count;

                    if (playerSequenceInputList[playerSequenceInputListCount - 1] != correctSequenceList[playerSequenceInputListCount - 1]) // if last player input is incorrect, rest player sequence and play "incorrect" sound
                    {
                        SetRandomCorrectSequence();
                        ResetPlayerSequenceInput();
                        errorJustMade = true;
                        lightSequencePLaying = true;
                    }
                    else if (plantOnTable == false) // if plant not on table reset player sequence, and play "incorrect" sound 
                    {
                        SetRandomCorrectSequence();
                        ResetPlayerSequenceInput();
                        errorJustMade = true;
                        lightSequencePLaying = true;
                    }
                    else // player input is correct and plant is on table
                    {
                        correctPressSound.pitch = 1 + 0.05f * playerSequenceInputListCount;
                        correctPressSound.Play();
                    }

                    isOrb3Pulsing = true;
                    orb3PulsingTimer = 0;
                    playerSequenceInputTimer = 0;
                    IsSequenceCorrect();
                }
            }            
        }

        // resets the player sequence if they take too long to decide 
        if (playerSequenceInputInProgress == true)
        {
            playerSequenceInputTimer += Time.deltaTime;

            if (playerSequenceInputTimer > 2) // if player hasn't made an input in 2 seconds, reset the input
            {
                lightSequencePLaying = true;
                errorJustMade = true;
                ResetPlayerSequenceInput();
                SetRandomCorrectSequence();
            }
        }

        // on sequence correct, play enchantment light event
        if (enchantmentLightEvent == true)
        {
            enchantmentLightEventTimer += Time.deltaTime/2.2f;

            enchantmentSuccessfulLight.enabled = true;
            enchantmentSuccessfulLight.intensity = enchantmentLightIntensity * Mathf.Sin(enchantmentLightEventTimer);

            if (enchantmentLightEventTimer > 3.15f)
            {
                enchantmentLightEvent = false;
                enchantmentSuccessfulLight.enabled = false;
                enchantmentLightEventTimer = 0;
            }
        }
    }
    
    // on plant enter the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "plant")
        {
            if (plantOnTable == false)
            {
                ResetValuesToDefault();   
                SetRandomCorrectSequence();
                lightSequencePLaying = true;
                lightSequenceTimer = 0;
                thePlantOnTheTable = other.gameObject.transform.parent.gameObject;
                plantOnTable = true;
            }            
        }
    }
    
    // on plant exit the trigger collider
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "plant")
        {
            if (plantOnTable == true)
            {
                lightSequenceTimer = 0;
                lightSequencePLaying = false;
                ResetValuesToDefault();
                plantOnTable = false;
                thePlantOnTheTable = null;
                errorJustMade = true;
            }            
        }
    }
    
    // resets the orb values to default
    void ResetValuesToDefault()
    {
        isOrb1Pulsing = false;
        isOrb2Pulsing = false;
        isOrb3Pulsing = false;

        orb1PulsingTimer = 0;
        orb2PulsingTimer = 0;
        orb3PulsingTimer = 0;

        orb1LightActive = false;
        orb2LightActive = false;
        orb3LightActive = false;

        pulseProportionComplete = 0;
        lightSequencePLaying = false;
        lightSequenceTimer = 0;

        orb1Light.GetComponent<Light>().enabled = false;
        orb2Light.GetComponent<Light>().enabled = false;
        orb3Light.GetComponent<Light>().enabled = false;

        orb1.GetComponent<Renderer>().materials[0].SetFloat("emissionIntensity", 0);
        orb2.GetComponent<Renderer>().materials[0].SetFloat("emissionIntensity", 0);
        orb3.GetComponent<Renderer>().materials[0].SetFloat("emissionIntensity", 0);
    }

    // resets the player input sequence
    private void ResetPlayerSequenceInput()
    {
        playerSequenceInputList.Clear();
        playerSequenceInputInProgress = false;
        playerSequenceInputTimer = 0;
        incorrectSound.Play();
    }
    
    // checks to see if sequence is correct, if so commence sequence correct functionality
    private void IsSequenceCorrect()
    {
        if (playerSequenceInputList.Count == correctSequenceList.Count) // if sequence is correct
        {
            correctSound.Play();
            playerSequenceInputInProgress = false;
            playerSequenceInputTimer = 0;
            playerSequenceInputList.Clear();
            gameObject.GetComponent<ParticleSystem>().Play();

            if (plantOnTable == true && thePlantOnTheTable) // if the plant is on the table
            {
                SetRandomCorrectSequence();
                thePlantOnTheTable.GetComponent<ParticleSystem>().Play();
                enchantmentLightEvent = true;
                thePlantOnTheTable.GetComponent<PlantScript>().isPlantEnchanted = true;
            }
        }        
    }

    // randomly sets the correct sequence
    private void SetRandomCorrectSequence()
    {
        bool sequenceSetCorrectly = false;

        System.Random random = new System.Random();

        while (sequenceSetCorrectly == false) // while a suitable sequence is not yet set
        {
            correctSequenceList.Clear();

            for (int i = 0; i < 8; i++)
            {
                int randomInt = random.Next(1, 4);


                if (randomInt == 1)
                {
                    correctSequenceList.Add("orb1");
                }
                else if (randomInt == 2)
                {
                    correctSequenceList.Add("orb2");
                }
                else
                {
                    correctSequenceList.Add("orb3");
                }
            }

            bool containsOrb1 = false;
            bool containsOrb2 = false;
            bool containsOrb3 = false;

            for (int i = 0; i < 8; i++) // check to see if sequence contains each orb at least once
            {
                if (correctSequenceList[i] == "orb1")
                {
                    containsOrb1 = true;
                }

                if (correctSequenceList[i] == "orb2")
                {
                    containsOrb2 = true;
                }

                if (correctSequenceList[i] == "orb3")
                {
                    containsOrb3 = true;
                }
            }

            if (containsOrb1 == true && containsOrb2 == true && containsOrb3 == true) // if sequence contains all three orbs, set sequenceSetCorrectly to true
            {
                sequenceSetCorrectly = true;
            }
        }
    }

    // sets orb to pulse at given 'correct sequence' index
    private void PulseOrbAtCorrectSequenceIndex(int sequenceIndex)
    {
        if (correctSequenceList[sequenceIndex] == "orb1")
        {
            if (isOrb1Pulsing == false)
            {
                isOrb1Pulsing = true;
                playSequenceSound.pitch = 1 + 0.05f * sequenceIndex;
                playSequenceSound.Play();
            }
        }
        else if (correctSequenceList[sequenceIndex] == "orb2")
        {
            if (isOrb2Pulsing == false)
            {
                isOrb2Pulsing = true;
                playSequenceSound.pitch = 1 + 0.05f * sequenceIndex;
                playSequenceSound.Play();
            }
        }
        else if (correctSequenceList[sequenceIndex] == "orb3")
        {
            if (isOrb3Pulsing == false)
            {
                isOrb3Pulsing = true;
                playSequenceSound.pitch = 1 + 0.05f * sequenceIndex;
                playSequenceSound.Play();
            }
        }
    }
}

