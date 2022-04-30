using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotScript : MonoBehaviour
{
    public bool isSelected = false;
    public static int SortID;
    public ConstManager constMan;
    public bool doOnce = false;
    static CamToTele teleScript;
    static GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        constMan = transform.parent.GetComponent<ConstManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        teleScript = player.transform.GetChild(1).GetComponent<CamToTele>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (teleScript.inTele)
        {
            if (isSelected)
            {
                GetComponent<Image>().color = new Color32(0, 255, 0, 100);

                if (!doOnce)
                {
                    SortID = transform.GetSiblingIndex();
                    Debug.Log(SortID);
                    constMan.AddPoint(SortID);
                    doOnce = true;


                }


            }
            else
            {
                GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            }
        }
    }
}
