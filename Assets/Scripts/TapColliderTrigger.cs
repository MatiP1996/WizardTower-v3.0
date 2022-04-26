using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TapColliderTrigger : MonoBehaviour
{
    public TapScript theTapScript; 

    // if watering can enters tap trigger area, snap the watering can to underneath the tap
    void OnTriggerEnter(Collider other)
    {
        theTapScript.WateringCanSocket(other);
    }
}
