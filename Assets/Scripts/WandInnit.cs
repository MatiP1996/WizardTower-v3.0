using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandInnit : MonoBehaviour
{
    public static bool isPickedUp = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPickedUp == true)
            {
                gameObject.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
