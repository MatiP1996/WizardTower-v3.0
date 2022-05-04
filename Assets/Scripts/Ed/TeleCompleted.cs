using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleCompleted : MonoBehaviour
{
    DrawManager drawMan;
    public bool finished;
    
    // Start is called before the first frame update
    void Start()
    {
        drawMan = GameObject.FindGameObjectWithTag("DrawManager").GetComponent<DrawManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        finished = drawMan.puzzleFinished;
        if (drawMan.puzzleFinished)
        {
            gameObject.GetComponent<Light>().color = new Color(0, 1, 0, 1);

        }
        else 
        {
            gameObject.GetComponent<Light>().color = new Color(1,0,0,1);
        }
    }
}
