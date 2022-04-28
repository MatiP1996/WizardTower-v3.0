using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotScript : MonoBehaviour
{
    public bool isSelected = false;
    public static int SortID;
    public ConstManager constMan;
    bool doOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        constMan = transform.parent.GetComponent<ConstManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (isSelected)
        {
            GetComponent<Image>().color = new Color32(0, 255, 0, 100);

            if (!doOnce)
            {
                constMan.AddPoint(SortID);
                SortID++;
                doOnce = true;
            }
            

        }
        else
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }
}
