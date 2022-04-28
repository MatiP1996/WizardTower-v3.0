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
        if (onLadder == true && Input.GetKey(KeyCode.W)) // if player is on ladder and W is being held, move the player up
        {
            player.GetComponent<UnityEngine.CharacterController>().Move(new Vector3(0, 3 * Time.deltaTime, 0));
        }
    }

    private void OnTriggerEnter(Collider collider) // when player enters the ladder trigger, set onLadder to true
    {
        if (collider.transform.root == player.transform)
        {
            onLadder = true;
        }
    }

    private void OnTriggerExit(Collider collider) // when player exits the ladder trigger, set onLadder to false
    {
        if (collider.transform.root == player.transform)
        {
            onLadder = false;
        }
    }
}

