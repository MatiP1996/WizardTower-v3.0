using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterEnd : MonoBehaviour
{
    public bool endColliding;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            endColliding = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            endColliding = false;
        }

    }
}
