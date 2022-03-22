using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject SecondaryMenu;
    public GameObject Player;
    public GameObject SecondaryCamera;

    // Start is called before the first frame update
    void Start()
    {
        SecondaryMenu.SetActive(false);
        SecondaryCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!SecondaryMenu.activeSelf)
            {
                SecondaryMenu.SetActive(true);
                Player.SetActive(false);
                SecondaryCamera.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                SecondaryMenu.SetActive(false);
                Player.SetActive(true);
                SecondaryCamera.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;


            }
        }
        
    }
}
