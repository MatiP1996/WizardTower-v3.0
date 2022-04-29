using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    Material mat;
    GameObject obj;
    Transform point;
    Ray ray;
    RaycastHit hit;
    public bool paintingInUse = false;

    public Camera freeCam;
    float freeCamStartFOV;
    public Camera playerCam;


    bool resetMouse = false;

    float zoomNum = 1f;


    public GameObject player;
    PlayerMove controlScript;
    Transform camPos;
    Transform selectedBox;
    Vector3 mousePos;
    Transform chosenPoint;
    bool correctLoc = false;
    bool correctRot = false;
    public bool correctPos = false;
    bool mouseStartPosReset = false;
    Vector3 mouseStartPos;

    Quaternion startBoxRot;

    float paintingWidth;
    float paintingHeight;
    Transform freecamStartPos;

    float boxAngle;
    float chosenPointAngle;
    Vector3 freeCamPos;

    // Start is called before the first frame update


    void Start()
    {
        selectedBox = this.transform.GetChild(2);
        camPos = this.transform.GetChild(3);
        chosenPoint = this.transform.GetChild(1);
        freeCamStartFOV = freeCam.fieldOfView;

        player = GameObject.FindGameObjectWithTag("Player");

        freecamStartPos = camPos.transform;
        startBoxRot.eulerAngles = selectedBox.localEulerAngles;


        controlScript = player.GetComponent<PlayerMove>();
        playerCam = player.transform.GetChild(1).GetComponent<Camera>();


        obj = gameObject;
        point = obj.transform;

        //get child of painting rendereer should be optimised
        string matName = obj.GetComponentInChildren<Renderer>().material.name;

        Debug.Log(matName);


        paintingWidth= gameObject.GetComponent<BoxCollider>().size.x;
        paintingHeight = gameObject.GetComponent<BoxCollider>().size.y;


        switch (matName)
        {
            case "red":
                Debug.Log("red");
                break;

            case "green":
                Debug.Log("green");
                break;

            case "blue":
                Debug.Log("white");
                break;

        }
    }

    public void CameraTransform()
    { 
        freeCam.transform.position = camPos.transform.position;
    }

    public void InteractPainting(GameObject painting)
    {
        //NO MORE RB ON PLAYER
        //controlScript.rb.isKinematic = true;

        //controlScript.rb.detectCollisions = false;

        freeCam.enabled = true;
        playerCam.enabled = false;

        CameraTransform();
        freeCam.transform.SetParent(transform);

        controlScript.inputAllowed = false;
        paintingInUse = true;
        player.GetComponent<MeshRenderer>().enabled = false;


        mousePos = Input.mousePosition;
        mousePos = selectedBox.transform.position;
    }


    void ResetMouseCentre()
    {
        mousePos = Input.mousePosition;
        mousePos.z = selectedBox.localPosition.z - 1;
        Cursor.lockState = CursorLockMode.Locked;
        mousePos.x = this.transform.position.x;
        mousePos.y = this.transform.position.y;
    }
    // Update is called once per frame

    void LeavePainting()
    {
        player.GetComponent<MeshRenderer>().enabled = true;
        player.transform.GetChild(1).GetComponent<Camera>().enabled = true;
        //controlScript.rb.detectCollisions = true;

        freeCam.enabled = false;
        paintingInUse = false;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<PlayerMove>().inputAllowed = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        freeCam.transform.SetParent(null);
        mouseStartPosReset = false ;
    }


    void GetInRange(Transform correctPoint, Transform boxLocation)
    {
        float distance = Vector3.Distance(correctPoint.position, boxLocation.position);
        
        //Debug.Log(distance);

        if (distance <= 0.16f)
        {
            Debug.Log("INRAGEHGERG");
            correctLoc = true;
        }
        else
        {
            correctLoc = false;
        }
    }

    void RotatePiece(Transform usedPiece, float rot)
    {
        if (usedPiece.localRotation.z > 180)
        {
            usedPiece.localEulerAngles = new Vector3(usedPiece.localRotation.x, usedPiece.localRotation.y, usedPiece.localEulerAngles.z -360f);


        }
        usedPiece.Rotate(0, rot, 0);

        boxAngle = Mathf.Atan2(usedPiece.transform.forward.x, usedPiece.transform.forward.y) * Mathf.Rad2Deg;
    }


    void Update()
    {

        if (paintingInUse)
        {
            freeCamPos = freeCam.transform.position;
            freeCamPos.z = Mathf.Clamp(freeCam.transform.position.z, freecamStartPos.position.z, freecamStartPos.position.z +2f);
            freeCam.transform.position = freeCamPos;
            freeCam.fieldOfView = 30;
            freeCam.transform.eulerAngles = new Vector3(freeCam.transform.eulerAngles.x,18f,freeCam.transform.eulerAngles.z);

            Debug.Log("Point:"+chosenPoint.localRotation.eulerAngles.z+"  Plane:"+selectedBox.localRotation.eulerAngles.y);
            
            if (selectedBox.localEulerAngles.z == chosenPoint.localEulerAngles.z)
            {
                Debug.Log("ORINETATION CORREVT");
                correctRot = true;
            }
            mousePos = Input.mousePosition;
           

            if (!mouseStartPosReset)
            {
                mouseStartPos = Input.mousePosition;
                Debug.Log(mouseStartPos);
                mouseStartPosReset = true;
            }


            Mathf.Clamp(freeCam.fieldOfView, 10, 30);
            Mathf.Clamp(mousePos.y, mouseStartPos.y - (-paintingHeight/2) , mouseStartPos.y -(paintingHeight / 2));



            /// IF squaree item on border NOT FUINCTIONING ATM
            /// 
            if (selectedBox.position.y >= Screen.height * 0.95)
            {
                Debug.Log("TRIGGERED IN SCRENHEIUHGT");
                if (Input.mousePosition.y > 200)
                {
                    freeCam.transform.position = new Vector3(freeCam.transform.position.x, freeCam.transform.position.y - 0.1f, freeCam.transform.position.z);
                }
                else
                {
                    freeCam.transform.position = new Vector3(freeCam.transform.position.x, freeCam.transform.position.y + 0.1f, freeCam.transform.position.z);
                }

            }



            if (camPos.position.z > freecamStartPos.position.z)
            {
                freeCam.transform.localPosition = new Vector3(selectedBox.localPosition.x, selectedBox.localPosition.y, freeCam.transform.localPosition.z);
                //Vector3.Lerp(freeCam.transform.position, new Vector3(selectedBox.position.x, selectedBox.localPosition.y, freeCam.transform.localPosition.z +2f), 2.5f);
                Debug.Log("Cam Greater#");
            }
            else 
            {
                //freeCam.transform.position = camPos.transform.position;
                Vector3.Lerp(freeCam.transform.position, camPos.transform.position,2);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                zoomNum = 1;
                //freeCam.fieldOfView -= 1;
                freeCam.transform.position = new Vector3(selectedBox.transform.position.x,selectedBox.transform.position.y, freeCam.transform.position.z +1);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                zoomNum =- 1;
                //freeCam.fieldOfView += 1;
                freeCam.transform.position = new Vector3(selectedBox.transform.position.x, selectedBox.transform.position.y, freeCam.transform.position.z - 1);
            }

            if (correctLoc && correctRot)
            {
                correctPos = true;
                Debug.Log("BOFFA TRUE");


            }
            else
            {
                correctPos = false;
            }
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                LeavePainting();
            }


            if (Input.GetMouseButtonDown(1))
            {
                RotatePiece(selectedBox, 90);


                if (selectedBox.localRotation.eulerAngles.y == chosenPoint.localRotation.eulerAngles.z)
                {
                    correctRot = true;
                }
                else
                {
                    correctRot = false;
                }

            }
            if (Input.GetMouseButtonDown(0))
            {
                //selectedBox.localEulerAngles = new Vector3(selectedBox.localEulerAngles.x, selectedBox.localEulerAngles.y, selectedBox.localEulerAngles.z);

                RotatePiece(selectedBox, -90);

                if (selectedBox.localRotation.z == chosenPoint.localRotation.z)                    
                {
                    correctRot = true;
                }
                else
                {
                    correctRot = false;
                }
            }

            if (!resetMouse)
            {
                ResetMouseCentre();
                resetMouse = true;
            }
            //ResetMouseCentre();

            Cursor.lockState = CursorLockMode.Confined;

            Ray ray = freeCam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.gameObject.tag =="Painting")
                {
                    selectedBox.transform.position = hit.point + new Vector3(0, 0, -0.001f);


                    GetInRange(chosenPoint, selectedBox);
                }

            }
        }
        else
        {
            controlScript.inputAllowed = true;
        }
    }
}
