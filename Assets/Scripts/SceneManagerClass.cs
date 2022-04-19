using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerClass : MonoBehaviour
{
    public void LoadMain()
    {
        //      Debug.Log("load main");
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
        //   Debug.Log("quit");
    }

    public void LoadMenu()
    {
        //   Debug.Log("load LoadMenu");
        SceneManager.LoadScene(0);
    }

}
