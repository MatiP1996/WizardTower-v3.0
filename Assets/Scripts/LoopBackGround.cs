using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackGround : MonoBehaviour
{
    float length;
    float startPos;
    public GameObject cam;
    
    [SerializeField]
    float parralexEffect;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parralexEffect));
        float distance = cam.transform.position.x * parralexEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }

}
