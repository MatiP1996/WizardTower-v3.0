using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeRoomManager : MonoBehaviour
{
    public Transform door;
    bool doOnce;
    // Start is called before the first frame update
    void Start()
    {
        //door.RotateAround();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.transform.GetChild(0).GetComponent<PaintingManager>().allComplete && gameObject.transform.GetChild(1).GetComponent<TeleCompleted>().finished)
        {
            if (!doOnce)
            {
                door.Rotate(0, -90, 0);
                doOnce = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            door.Rotate(0, -90, 0);
        }
        
    }
}
