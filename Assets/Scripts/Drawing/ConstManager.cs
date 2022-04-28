using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstManager : MonoBehaviour
{
    Transform[] array;
    List<int> pointsPressed = new List<int>();
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

        if (array.Length -1 == pointsPressed.Count)
        {
            for (int i = 1; i < pointsPressed.Count; i++)
            {
                Debug.Log(pointsPressed[i]+" " +" "+i);
                /*if (pointsPressed[i] != i)
                {
                    pointsInOrder = false;
                    break;
                }*/
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
        Debug.Log("Pressed num"+pointsPressed.Count);

        if (array.Length-1 == pointsPressed.Count)
        {
            CheckOrder();
        }
    }
}
