using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstManager : MonoBehaviour
{
    public GameObject lineParent;
    Transform[] array;
    List<int> pointsPressed = new List<int>();
    public bool completed;
    CamToTele camTele;
    static GameObject player;
    GameObject lpChild;
    bool doOnce = false;
    bool oneTime = false;

    [SerializeField] TransformsFound transformsFound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        array = gameObject.GetComponentsInChildren<Transform>();

        camTele = player.transform.GetChild(1).GetComponent<CamToTele>();

    }
    public void AddPoint(int DotID)
    {
        pointsPressed.Add(DotID);
    }
    public bool CheckOrder()
    {
        bool pointsInOrder = true;

        if (array.Length - 1 == pointsPressed.Count)
        {
            for (int i = 0; i < pointsPressed.Count; i++)
            {
                Debug.Log(pointsPressed[i] + " " + " " + i);
                if (pointsPressed[i] != i)
                {
                    pointsInOrder = false;
                    break;
                }
            }
        }
        else
        {
            pointsInOrder = false;
        }

        Debug.Log("Valid order: " + pointsInOrder);
        return pointsInOrder;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (camTele.inTele)
        {

            if (array.Length - 1 == pointsPressed.Count)
            {
                completed = CheckOrder();
                Debug.Log(completed);

                if (completed)
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.GetComponent<DotScript>().isSelected = true;


                    }

                    if (lineParent.transform.childCount > 0)
                    {

                        if (!doOnce)
                        {
                            lpChild = lineParent.transform.GetChild(0).gameObject;
                            lineParent.transform.GetChild(0).gameObject.SetActive(false);
                            Destroy(lpChild);


                            transformsFound.currentLine = null;

                            doOnce = true;
                        }

                    }

                }
                else 
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.GetComponent<DotScript>().isSelected = false;
                        child.gameObject.GetComponent<DotScript>().doOnce = false;


                    }
                    if (lineParent.transform.childCount > 0)
                    {

                        if (!oneTime)
                        {
                            lpChild = lineParent.transform.GetChild(0).gameObject;
                            lineParent.transform.GetChild(0).gameObject.SetActive(false);
                            Destroy(lpChild);


                            transformsFound.currentLine = null;

                            pointsPressed.Clear();
                            Debug.Log("Cleared");
                        }

                    }
                }
            }
        }

    }
}
