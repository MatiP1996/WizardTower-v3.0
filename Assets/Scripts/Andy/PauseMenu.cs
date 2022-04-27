using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool pauseMenuVisible = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)  || Input.GetKeyDown(KeyCode.KeypadMinus)) // if player presses escape, open pause menu
        {
            Resume();
        }
    }

    // opens and closes the pause menu
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

    // restarts the game
    public void Restart() 
    {
        pauseMenuVisible = !pauseMenuVisible;
        pauseMenu.SetActive(pauseMenuVisible);
        Cursor.visible = pauseMenuVisible;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    // quits the game
    public void Quit()
    {
        Application.Quit();
    }
}
