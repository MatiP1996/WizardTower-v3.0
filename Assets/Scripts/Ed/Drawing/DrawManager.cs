using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public GameObject[] constellations;
    public bool puzzleFinished = false;
    // Start is called before the first frame update
    
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(puzzleFinished)
        {
            Debug.Log("Stars Completed !!!!");
        }
        foreach (GameObject i in constellations)
        {
            if (i.GetComponent<ConstManager>().completed)
            {
                puzzleFinished = true;
            }
            else 
            {
                puzzleFinished = false;
            }
            
        }
       
    }
}
