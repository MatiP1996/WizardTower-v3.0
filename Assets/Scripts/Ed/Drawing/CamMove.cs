using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    Transform startCamPos;
    float mouseStart;
    GameObject player;
    CamToTele camTeleScript;
    // Start is called before the first frame update
    void Start()
    {
        startCamPos = transform;
        mouseStart = Input.mousePosition.x;
        Debug.Log(Input.mousePosition.x);
        player = GameObject.FindGameObjectWithTag("Player");

         camTeleScript= player.transform.GetChild(1).GetComponent<CamToTele>();

    }

    // Update is called once per frame
    void Update()
    {
        if (camTeleScript.inTele)
        {
            if (Input.mousePosition.x >= Screen.width * 0.95f)
            {
                transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);

            }
            else if (Input.mousePosition.x <= 60)
            {
                transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
            }
        }

    }

}
