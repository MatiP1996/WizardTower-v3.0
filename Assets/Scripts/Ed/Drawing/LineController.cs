using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    LineRenderer lineRef;

    public List<Transform> points = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        lineRef.sortingOrder = 1;
    }
    private void Awake()
    {
        lineRef = GetComponent<LineRenderer>();
        lineRef.positionCount = 0;
    }
    public void AddPoint(Transform point)
    {
        lineRef.positionCount++;
        points.Add(point);
    }

    private void LateUpdate()
    {
           
        if (points.Count > 1)
        {
            for (int i = 0; i < points.Count; i++)
            {
                lineRef.SetPosition(i, points[i].position);
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
