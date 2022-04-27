using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStaff : InteractionParent
{
    public GameObject target;

    //elevating

    float amplitudeY = 5.0f;
    float omegaX;
    float omegaY;
    float index;

    // Start is called before the first frame update
    void Start()
    {
        omegaX = transform.position.x;
        omegaY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, 50 * Time.deltaTime);
        /*
        //elevating
        index += Time.deltaTime;
        float x = transform.position.x;
        float y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));
        transform.localPosition = new Vector3(x, y / 100, 0);
        */
    }
}
