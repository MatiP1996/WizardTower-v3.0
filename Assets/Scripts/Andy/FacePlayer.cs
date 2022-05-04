using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public GameObject playerCamera;
    //  private Vector3 thisObjToPlayerVector;


    // Update is called once per frame

    private void Start()
    {
        playerCamera = GameObject.Find("Main Camera");
    }
    void Update()
    {
        transform.LookAt(playerCamera.transform.position);
    }
}
