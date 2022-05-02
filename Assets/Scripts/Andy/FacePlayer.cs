using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public GameObject playerCamera;
  //  private Vector3 thisObjToPlayerVector;


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerCamera.transform.position);
    }
}
