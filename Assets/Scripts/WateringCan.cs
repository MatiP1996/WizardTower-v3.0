using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    private ParticleSystem waterParticles;
    private MeshRenderer waterRenderer;

    void Start()
    {
        waterParticles = transform.GetChild(1).GetComponent<ParticleSystem>();
        waterRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (PickUpObject.itemCurrentlyPickedUp == gameObject)
            {
                if (waterRenderer.enabled == true)
                {
                    waterParticles.Play();
                    waterRenderer.enabled = false;
                }
            }
        }
    }
}
