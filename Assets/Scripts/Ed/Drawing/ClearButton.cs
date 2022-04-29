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


    public GameObject set1;
    private Transform[] set1Arr;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        lineController = pt.currentLine;

        //set1a = set1.transform.get

    }

    void TaskOnClick()
    {
        if (lineParent.transform.childCount > 0)
        {
            linesFromPar = lineParent.GetComponentsInChildren<Transform>();
            foreach (Transform child in linesFromPar)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        if (lineController != null)
        {
            /*lines = GameObject.FindGameObjectsWithTag("Line");

            foreach(GameObject line in lines)
            {
                Debug.Log("deeleted liens");
                //Object.Destroy(line);

            }

            set1Arr = set1.GetComponentsInChildren<Transform>();
            */
            ///sets colour to default
            foreach (Transform i in set1Arr)
            {
                if (i.GetComponent<DotScript>() != null)
                {
                    DotScript dotScript = i.gameObject.GetComponent<DotScript>();

                    dotScript.isSelected = false;
                }
            }
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