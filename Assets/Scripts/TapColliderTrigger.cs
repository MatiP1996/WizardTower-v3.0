using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TapColliderTrigger : MonoBehaviour
{
    public TapScript theTapScript; 


    void OnTriggerEnter(Collider other)
    {
        theTapScript.WateringCanSocket(other);
    }
}
