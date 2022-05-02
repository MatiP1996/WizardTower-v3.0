using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoBehaviour
{

    List<GameObject> paintings = new List<GameObject>();
    public GameObject[] allPaintings;
    int integer;
    bool allComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.FindObjectsOfType<Painting>();

    }

    // Update is called once per frame
    void Update()
    {
        if (allComplete)
        {
            Debug.Log("All paintings true");
        }
        foreach (GameObject i in allPaintings)
        {
            if (i.GetComponent<Painting>().correctPos)
            {
                allComplete = true;
                
            }
            else
            {
                allComplete = false;
             //   Debug.Log("All paintings not true");
            }

        }


    }
}
