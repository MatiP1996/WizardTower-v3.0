using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    public GameObject painting;
    Transform camPos;
    float rot;
    GameObject square;

    // Start is called before the first frame update
    void Start()
    {
        camPos = painting.transform.GetChild(3).transform;
        square = painting.transform.GetChild(2).gameObject;

        rot = painting.transform.eulerAngles.y;

    }

    void CamRotate()
    {
        gameObject.transform.SetParent(camPos);
        //camPos.transform.Rotate(camPos.transform.eulerAngles.x, rot, camPos.transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (gameObject.activeSelf == true)
        {
            
            Vector3 mousePos = Input.mousePosition;

            //camPos.transform.position = mousePos; CAM TO CENTRE OF SQUARE


            square.transform.position = mousePos;

            
            

        }
        else 
        {
            gameObject.transform.SetParent(null);
        }

        */

    }
}
