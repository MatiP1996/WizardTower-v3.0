using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoBehaviour
{

    List<GameObject> paintings = new List<GameObject>();
    GameObject[] allPaintings;
    int integer;
    bool allComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.FindObjectsOfType<Painting>();

        allPaintings = GameObject.FindGameObjectsWithTag("Painting");


        foreach (GameObject i in allPaintings)
        {
            paintings.Add(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("List Count:"+ paintings.Count);
        foreach (GameObject i in paintings)
        {
            /*if (i.GetComponent<Painting>().correctPos == true)
            {
                allComplete = true;

                Debug.Log("completed");
            }
            else 
            {
                allComplete = !allComplete;
            }*/
        }
    }
}
