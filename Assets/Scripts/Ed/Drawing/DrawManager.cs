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
    private bool puzzleCompleted()
    {
        foreach (GameObject i in constellations)
        {
            ConstManager cm = i.GetComponent<ConstManager>();
            Debug.Log("constmanagaer: "+cm.completed);
            if (cm.completed)
            {   
                return true;
            }

            return false;
        }
        return false;


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log( "ConstLENGYTH: "+constellations.Length);
        




        puzzleFinished = puzzleCompleted();
        if (puzzleFinished)
        {
            Debug.Log("Stars Completed !!!!");
        }

        Debug.Log("Puzz finished: " + puzzleFinished);

    }
}
