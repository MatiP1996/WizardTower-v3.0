using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    private LineController lineController;
    public PenTools pt;
    Button button;
    bool doOnce = false;
    GameObject[] lines;
    public GameObject lineParent;
    Transform[] linesFromPar;

    public GameObject[] constManagers;

    public GameObject set1;
    private Transform[] set1Arr;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        lineController = pt.currentLine;



    }

    void TaskOnClick()
    {
        foreach (GameObject i in constManagers)
        {
            i.GetComponent<ConstManager>().ClearLines();
        }

    }
    private void Update()
    {
        if (!doOnce)
        {
            lineController = pt.currentLine;
            if (lineController != null)
            {
                Debug.Log(pt.currentLine);
                lineController = pt.currentLine.GetComponent<LineController>();
                doOnce = true;
            }

        }

    }

}