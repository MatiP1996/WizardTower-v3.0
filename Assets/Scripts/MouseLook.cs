using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock the cursor
    }
    
    void Update()
    {
        if (PauseMenu.pauseMenuVisible == false) // if pause menu isn't open, use mouse input to rotate player body and camera
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // gets X axis mouse movement
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; ; // gets Y axis mouse movement

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // rotate camera
            playerBody.Rotate(Vector3.up * mouseX); // rotate body
        }
    }
}
