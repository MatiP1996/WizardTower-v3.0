using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformsFound : MonoBehaviour
{
    Transform[] items;
    Transform empty;
    public List<Transform> nearby = new List<Transform>();
    public GameObject closestPoint = null;
    private CircleCollider2D circleCol;
    GameObject player;
    CamToTele camScript;
    GameObject starCam;

    [Header("Lines")]
    [SerializeField] Transform lineParent;
    [SerializeField] private GameObject linePrefab;

    public LineController currentLine;


    // Start is called before the first frame update
    void Start()
    {
        starCam = GameObject.FindGameObjectWithTag("StarCam");
        //Debug.Log(transform);
        circleCol = gameObject.GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        camScript = player.transform.GetChild(1).GetComponent<CamToTele>();
    }

    Transform GetClosestPoint(List<Transform> points)
    {
        Debug.Log("deez");
        Transform bestTarget = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = gameObject.transform.position;
        foreach (Transform point in points)
        {
            Vector3 directionTarget = point.position - currentPos;
            float dis = directionTarget.sqrMagnitude;
            if (dis < minDistance)
            {
                bestTarget = point;
                minDistance = dis;
            }
        }
        Debug.Log(bestTarget);
        return bestTarget;
    }

    private Vector3 GetMousePosition()
    {
        //Vector3 mousePos = new Vector3(0,0,0);
        //Ray ray = starCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        Vector3 mousePos = starCam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

        //change to area on z axis
        mousePos.z = 165;

        return mousePos;
    }
    private void GetNearestPoint()
    {

        circleCol.transform.localPosition = GetMousePosition();

    }


    public void SetPoint()
    {
        Debug.Log(nearby.Count);


        /*if (GetClosestPoint(nearby).gameObject != null)
        {
            closestPoint = GetClosestPoint(nearby).gameObject;
        }*/


        closestPoint = GetClosestPoint(nearby).gameObject;

        if (closestPoint.gameObject.GetComponent<DotScript>() != null)
        {
            closestPoint.GetComponent<DotScript>().isSelected = !closestPoint.GetComponent<DotScript>().isSelected;
            Debug.Log(closestPoint.GetComponent<DotScript>().isSelected);
        }



        foreach (Transform i in nearby)
        {
            Debug.Log(i.transform);
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (camScript.inTele)///CHASNGE TO INTELE BOOL
        {
            if (currentLine != null)
            {
                Debug.Log(currentLine.points.Count);
            }

            if (Input.GetMouseButtonDown(0))
            {
                GetNearestPoint();

                Delay(0.01f);


            }

        }


    }
    private void CheckPoints()
    {
        if (GetClosestPoint(nearby) != null)
        {
            Debug.Log("PointNOTNULL");
            closestPoint = GetClosestPoint(nearby).gameObject;
            Debug.Log(closestPoint);

            if (closestPoint.gameObject.GetComponent<DotScript>() != null)
            {
                closestPoint.GetComponent<DotScript>().isSelected = !closestPoint.GetComponent<DotScript>().isSelected;
            }


            if (currentLine == null)
            {
                currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, lineParent).GetComponent<LineController>();
                Debug.Log("Instanciated line");
            }

            if (closestPoint != null)
            {
                Debug.Log("ADDED POINT");
                currentLine.AddPoint(closestPoint.transform);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        nearby.Add(collision.transform);
        Debug.Log(collision.transform);
        CheckPoints();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        nearby.Remove(collision.transform);
        Debug.Log(collision.transform);
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);

    }
}
