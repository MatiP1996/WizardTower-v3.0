using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickup : MonoBehaviour
{
    public GameObject noteCanvas;
    bool inNote = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inNote)
        { 
            if(Input.GetKeyDown(KeyCode.F))
            {
                noteCanvas.SetActive(false);
                inNote = false;
            }
        }
    }

    public void InspectNote()
    {
        if (!inNote)
        {
            noteCanvas.SetActive(true);
            inNote = true;
        }
    }
}
