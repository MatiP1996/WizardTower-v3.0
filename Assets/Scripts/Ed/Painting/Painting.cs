using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

 //   float zoomNum = 1f;


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
    CharacterController cc;

    Color redLight;
    float chosenPointRot;
    float selectedBoxRot;

    public HintUI hintUI;

    public Vector2 mouseLimit;
    float borderThickness = 10f;
    float camMoveSpeed = 1f;
    float scrollSpeed = 5f;

    // Start is called before the first frame update


    void Start()
    {
        selectedBox = this.transform.GetChild(2);
        camPos = this.transform.GetChild(3);
        chosenPoint = this.transform.GetChild(1);
        freeCamStartFOV = freeCam.fieldOfView;

        player = GameObject.FindGameObjectWithTag("Player");
        cc = player.GetComponent<CharacterController>();

        freecamStartPos = camPos.transform;
        startBoxRot.eulerAngles = selectedBox.localEulerAngles;


        controlScript = player.GetComponent<PlayerMove>();
        playerCam = player.transform.GetChild(1).GetComponent<Camera>();

        redLight = transform.GetChild(4).GetComponent<Light>().color;

        obj = gameObject;
        point = obj.transform;

        //get child of painting rendereer should be optimised
        string matName = obj.GetComponentInChildren<Renderer>().material.name;

   //     Debug.Log(matName);


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

    public void InteractPainting()
    {
        //NO MORE RB ON PLAYER
        //controlScript.rb.isKinematic = true;

        //controlScript.rb.detectCollisions = false;

        freeCam.enabled = true;
        playerCam.enabled = false;

        CameraTransform();
        freeCam.transform.SetParent(transform);

        //cc.enabled = false;
        controlScript.inputAllowed = false;
        paintingInUse = true;
        //player.GetComponent<MeshRenderer>().enabled = false;

        Cursor.visible = false;
        mousePos = Input.mousePosition;
        mousePos = selectedBox.transform.position;

        hintUI.hintText.enabled = true;
        
        StartCoroutine(WaitThenNull(5));
        ///sort dis

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
        //player.GetComponent<MeshRenderer>().enabled = true;
        player.transform.GetChild(1).GetComponent<Camera>().enabled = true;
        //controlScript.rb.detectCollisions = true;

        freeCam.enabled = false;
        paintingInUse = false;

        player.GetComponent<PlayerMove>().inputAllowed = true;
        //cc.enabled = true;

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

        if (correctPos)
        {
            transform.GetChild(4).GetComponent<Light>().color = new Color(0, 1, 0,1);
        }
        else
        {
            transform.GetChild(4).GetComponent<Light>().color = new Color(1, 0, 0, 1);
        }

        if (paintingInUse)
        {
            freeCamPos = freeCam.transform.position;
            freeCamPos.z = Mathf.Clamp(freeCam.transform.position.z, freecamStartPos.position.z, freecamStartPos.position.z +2f);
            freeCam.transform.position = freeCamPos;
            freeCam.fieldOfView = 30;
            freeCam.transform.eulerAngles = new Vector3(freeCam.transform.eulerAngles.x,18f,freeCam.transform.eulerAngles.z);




            if (chosenPoint.localRotation.eulerAngles.x != 0)
            {
                chosenPointRot = chosenPoint.localRotation.eulerAngles.x + 90;
            }

            else if (chosenPoint.localRotation.eulerAngles.x == -180)
            {
                //chosenPointRot = 90;
                //chosenPointRot = 270;

            }

            if (chosenPointRot == 0)
            {
                chosenPointRot = 90;
            }


            if (chosenPointRot == 90 && chosenPoint.localRotation.eulerAngles.x == 0)
            {
                //chosenPointRot = 0;
            }
            else if (chosenPointRot == 90)
            {
                Debug.Log(chosenPointRot);

                if (chosenPoint.localRotation.eulerAngles.x == 0)
                {
                    chosenPointRot = 90;
                }
                else
                {
                    chosenPointRot = 270;
                }
                
            }



            if (chosenPointRot > 90)
            {
                if (chosenPointRot != 0)
                {
                    //chosenPointRot = chosenPoint.localRotation.eulerAngles.x + 90;
                }


            }


            if (chosenPointRot == 360)
            {
                chosenPointRot = 0;
            }


            


            if (chosenPointRot < 0)
            {
                //chosenPointRot = (chosenPoint.localRotation.eulerAngles.x + -90f) +360f;
            }

            Debug.Log(chosenPointRot);

            Debug.Log("Point:" + chosenPoint.localRotation.eulerAngles.y + "  Plane:" + selectedBox.localRotation.eulerAngles.y);

            if (selectedBox.localRotation.eulerAngles.y == chosenPointRot)
            {
                correctRot = true;
            }
            else
            {
                correctRot = false;
            }
            mousePos = Input.mousePosition;
           

            if (!mouseStartPosReset)
            {
                mouseStartPos = Input.mousePosition;
                mouseStartPosReset = true;
            }


            Mathf.Clamp(freeCam.fieldOfView, 10, 30);
            Mathf.Clamp(mousePos.y, mouseStartPos.y - (-paintingHeight/2) , mouseStartPos.y -(paintingHeight / 2));


            Vector3 pos = freeCam.transform.localPosition;

            if (Input.mousePosition.y >= Screen.height - borderThickness)
            {
                pos.y += camMoveSpeed *Time.deltaTime;
            
            }
            if (Input.mousePosition.y <= borderThickness)
            {
                pos.y -= camMoveSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x >= Screen.width - borderThickness)
            {
                pos.x += camMoveSpeed * Time.deltaTime;

            }
            if (Input.mousePosition.x <= borderThickness)
            {
                pos.x -= camMoveSpeed * Time.deltaTime;
            }

            pos.x = Mathf.Clamp(pos.x, -mouseLimit.x, mouseLimit.x);
            pos.y = Mathf.Clamp(pos.y, -mouseLimit.y, mouseLimit.y);

            if (pos.z < -2.25f)
            {
                camMoveSpeed = 1.5f;
            }
            else
            {
                camMoveSpeed = 3f;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            pos.z += scroll * scrollSpeed *50f * Time.deltaTime;

            freeCam.transform.localPosition = pos;
            


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
            }

            if (Input.GetMouseButtonDown(0))
            {
                RotatePiece(selectedBox, -90);
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

    IEnumerator WaitThenNull(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Time dun true");

        hintUI.hintText.enabled = false;
    }
}
