using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStart : MonoBehaviour
{
    public GameObject player;
    public GameObject endTeleporter;

    Transform beginPos;
    Transform endPos;

    bool atEndPos = false;
    bool onTeleporter = false;
    bool roomComplete = false;
    TeleporterEnd tpEnd;
    //public RoomManager roomManager;

    // Start is called before the first frame update
    void Start()
    {
        beginPos = transform.GetChild(1);

        endPos = endTeleporter.transform.GetChild(1);

        tpEnd = endTeleporter.GetComponent<TeleporterEnd>();

        ///ROOM MANAGER. FINSHED BOOL
    }

    // Update is called once per frame
    void Update()
    {
        ///ADD ROOM FINISHED BOOL TO GET KEY DOWNS
        if (Input.GetKeyDown(KeyCode.Q) && onTeleporter == true)
        {
            player.transform.position = endPos.position;
            onTeleporter = false;

        }
        if (Input.GetKeyDown(KeyCode.Q) && tpEnd.endColliding == true)
        {
            player.transform.position = beginPos.position;
            tpEnd.endColliding = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onTeleporter = true;

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onTeleporter = false;

        }
    }
}
