using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public GameObject player;
    public static bool onLadder = false;

    // Update is called once per frame
    void Update()
    {
        if (onLadder == true && Input.GetKey(KeyCode.W))
        {
            player.GetComponent<UnityEngine.CharacterController>().Move(new Vector3(0, 2 * Time.deltaTime, 0));
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root == player.transform)
        {
            onLadder = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.root == player.transform)
        {
            onLadder = false;
        }
    }
}

