using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamToTele : MonoBehaviour
{
    public Transform teleLens;
    float camMoveSpeed =5f;
    public bool inTele = false;
    InteractionManager intManager;
    string DrawLineTest = "DrawLineTest";
    Scene currentScene;
    GameObject starCamGo;
    GameObject player;
    Camera playerCam;
    Camera starCam;
    public GameObject freeCam;
    // Start is called before the first frame update
    void Start()
    {
        starCamGo = GameObject.FindGameObjectWithTag("StarCam");
        starCam = starCamGo.GetComponent<Camera>();    
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = player.transform.GetChild(1).GetComponent<Camera>();
    }

    public void LeaveTele()
    {
        freeCam.GetComponent<Camera>().enabled = true;
        playerCam.enabled = true; 

        starCam.enabled = false;
        inTele = false;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void GoToTele()
    {
        transform.position = Vector3.Lerp(transform.position,teleLens.position,camMoveSpeed * Time.deltaTime);
        inTele = true;

        freeCam.GetComponent<Camera>().enabled = false;
        starCam.enabled = true;


        ///intManager.freeCam.GetComponent<Camera>().enabled = false;

        Debug.Log("Loading scene");



        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (inTele)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                LeaveTele();
            }
        }
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
    IEnumerator LoadedCompletion(AsyncOperation aSync)
    {

        while (!currentScene.isLoaded)
        {
            yield return null;
        }
    }
}
