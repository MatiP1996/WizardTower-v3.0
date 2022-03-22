using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerSingleton : MonoBehaviour
{
    
    public static SceneManagerSingleton _instance;

    public static string SceneName;
    public GameObject InGameMenu;
    bool SecondMenuActivated;
    public string playerName;
    float timeCreated;
    // Start is called before the first frame update


    private void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
        /*
        SceneName = SceneManager.GetActiveScene().name;

        Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {

            Debug.Log("main scene");
            InGameMenu = GameObject.Find("Canvas2");
            InGameMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

        }
    }
        */
    /*
    private void Update()
    {
        if(SceneName == "SampleScene")
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(InGameMenu.activeSelf)
                {
                 //   Debug.Log("YO");
                    InGameMenu.SetActive(false);

              
                }
                else
                {
                    InGameMenu.SetActive(true);


                }
            }
        }
    }
    */





}
