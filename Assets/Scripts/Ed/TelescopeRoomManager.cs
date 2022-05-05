using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeRoomManager : MonoBehaviour
{
    public Transform door;
    bool doOnce;
    public bool roomComplete = false;
    public GameObject spareCam;
    Camera playerCam;
    GameObject player;
    // Start is called before the first frame update
    private void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.transform.GetChild(0).GetComponent<PaintingManager>().allComplete && gameObject.transform.GetChild(1).GetComponent<TeleCompleted>().finished)
        {
            if (!doOnce)
            {

                roomComplete = true;

                spareCam.SetActive(true);
                playerCam.enabled = false;

                doOnce = true;

                StartCoroutine(WaitForTime(2));
            }

        }

        IEnumerator WaitForTime(float time)
        {
            yield return new WaitForSeconds(time);

            door.position = new Vector3(door.position.x, -7f, door.position.z);
            yield return new WaitForSeconds(time);

            playerCam.enabled = true;
            spareCam.SetActive(false);
        }
    }
}
