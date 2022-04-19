using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGravityDirection : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject downArrow;
    Rigidbody seedRigidbody;
    float gravityAmplitude = 60;


    Vector3 left = new Vector3(-1,0,0);
    Vector3 right = new Vector3(1, 0, 0);
    Vector3 up = new Vector3(0, 1, 0);
    Vector3 down = new Vector3(0, -1, 0);

    string direction = null;


    private void Start()
    {
        seedRigidbody = gameObject.GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CameraRaycast.currentHitInteractable == leftArrow)
            {
                direction = "left";
            }
            else if (CameraRaycast.currentHitInteractable == rightArrow)
            {
                direction = "right";
            }
            else if (CameraRaycast.currentHitInteractable == upArrow)
            {
                direction = "up";
            }
            else if (CameraRaycast.currentHitInteractable == downArrow)
            {
                direction = "down";
            }
        }







        if (direction == "left")
        {
            seedRigidbody.AddForce(left * Time.deltaTime * gravityAmplitude);
        }
        else if (direction == "right")
        {
            seedRigidbody.AddForce(right * Time.deltaTime * gravityAmplitude);
        }
        else if (direction == "up")
        {
            seedRigidbody.AddForce(up * Time.deltaTime * gravityAmplitude);
        }
        else if (direction == "down")
        {
            seedRigidbody.AddForce(down * Time.deltaTime * gravityAmplitude);
        }
    }
}
