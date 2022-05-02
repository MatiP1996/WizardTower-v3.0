using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public GameObject playerCharacter;
    private Vector3 thisObjToPlayerVector;


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerCharacter.transform.position);
    }
}
