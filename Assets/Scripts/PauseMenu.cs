using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool pauseMenuVisible = false;
    //public GameObject playerChar;
    public CharacterController characterController;


    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        //characterController = playerChar.GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)  || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Resume();
        }

    }


    public void Resume()
    {
        pauseMenuVisible = !pauseMenuVisible;
        pauseMenu.SetActive(pauseMenuVisible);
        Cursor.visible = pauseMenuVisible;
        Cursor.lockState = CursorLockMode.Confined;

        if (pauseMenuVisible == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        pauseMenuVisible = !pauseMenuVisible;
        pauseMenu.SetActive(pauseMenuVisible);
        Cursor.visible = pauseMenuVisible;
        Time.timeScale = 1;

        SceneManager.LoadScene(0);

        


    }

    public void Quit()
    {
        Application.Quit();
    }
}
