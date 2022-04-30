using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstManager : MonoBehaviour
{
    public GameObject lineParent;
    Transform[] array;
    List<int> pointsPressed = new List<int>();
    bool completed = false;
    // Start is called before the first frame update
    void Start()
    {
        array = gameObject.GetComponentsInChildren<Transform>();
        Debug.Log(array.Length);
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
        Debug.Log("Pressed num" + pointsPressed.Count);

        if (array.Length - 1 == pointsPressed.Count)
        {
            completed = CheckOrder();

            if (completed)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.GetComponent<DotScript>().isSelected = true;


                }
                Debug.Log(lineParent.transform.GetChild(0).gameObject);
                //GameObject.Destroy(lineParent.transform.GetChild(0).gameObject);
                lineParent.transform.GetChild(0).gameObject.SetActive(false);
                ///lineParent.transform.GetChild(0).gameObject = null;
            }
        }
    }
}
