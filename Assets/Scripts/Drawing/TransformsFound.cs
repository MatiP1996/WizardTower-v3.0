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
        Vector3 mousePos = starCam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

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
    void Update()
    {
        if (camScript.inTele)///CHASNGE TO INTELE BOOL
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetNearestPoint();

                Delay(0.01f);


                if (GetClosestPoint(nearby) != null)
                {
                    closestPoint = GetClosestPoint(nearby).gameObject;

                    if (closestPoint.gameObject.GetComponent<DotScript>() != null)
                    {
                        closestPoint.GetComponent<DotScript>().isSelected = !closestPoint.GetComponent<DotScript>().isSelected;
                        //Debug.Log(closestPoint.GetComponent<DotScript>().isSelected);
                    }

                }



            }

        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        nearby.Add(collision.transform);
        Debug.Log(collision.transform);
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
