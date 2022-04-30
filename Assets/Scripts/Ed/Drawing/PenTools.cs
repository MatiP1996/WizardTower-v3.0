using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenTools : MonoBehaviour
{
    [Header ("Dots")]
    [SerializeField] Transform dotParent;
    [SerializeField] private GameObject dotPrefab;

    [Header("Lines")]
    [SerializeField] Transform lineParent;
    [SerializeField] private GameObject linePrefab;

    [Header("Lines")]
    public CircleCollider2D circleCol;
    public GameObject circleColObj;

    public LineController currentLine;

    Ray ray;
    RaycastHit hit;
    Transform hitLoc;

    [SerializeField]Camera cam;
    Vector3 worldMouseLocation;
    

    Vector3 lastPos = Vector3.zero;
    Vector3 delta = Vector3.zero;

    List<Transform> dots = new List<Transform>();

    TransformsFound transformScript;
    GameObject player;
    InteractionManager intMan;
    GameObject starCam;
    CamToTele camTele;

    // Start is called before the first frame update
    void Start()
    {
        transformScript = circleColObj.GetComponent<TransformsFound>();
        player = GameObject.FindGameObjectWithTag("Player");
        starCam = GameObject.FindGameObjectWithTag("StarCam");
        camTele = player.transform.GetChild(1).GetComponent<CamToTele>();


    }

    void LeaveScene()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("LeaveScene");
        //SceneManager.LoadScene("Main Scene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main Scene"));
        //intMan.leaveTelescope();

        camTele.LeaveTele();


    }
    // Update is called once per frame
    void Update()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = Camera.main.nearClipPlane;
        //worldMouseLocation = Camera.main.ScreenToWorldPoint(mousePos);
        if (Input.GetKeyDown(KeyCode.F))
        {
            LeaveScene();
        }
        if (Input.GetMouseButton(1))
        {
            delta = Input.mousePosition - lastPos;
        }
        
        //RayCast test
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            hitLoc = hit.transform;

        }

        if (Input.GetMouseButtonDown(0))
        {

            //GetNearestPoint();
            
            
            //DELTED TO STOP POINT SPAWNING

            if (currentLine == null)
            {
                currentLine = Instantiate(linePrefab,Vector3.zero , Quaternion.identity, lineParent).GetComponent<LineController>();
            }

            //GameObject dot = Instantiate(dotPrefab,GetMousePosition(),Quaternion.identity,dotParent);

            

            if (transformScript.closestPoint != null)
            {
                currentLine.AddPoint(transformScript.closestPoint.transform);
            }


            //transformScript.SetPoint();
            
        }

    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        return mousePos;
    }

    private void GetNearestPoint()
    {
        //circleCol.isTrigger = true;
        circleCol.transform.localPosition = GetMousePosition();
        
        //circleCol.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dots.Contains(collision.transform))
        {
            dots.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dots.Contains(collision.transform))
        {
            dots.Remove(collision.transform);
        }

    }
}
