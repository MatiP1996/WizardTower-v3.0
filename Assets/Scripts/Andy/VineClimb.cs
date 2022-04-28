using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineClimb : MonoBehaviour
{
    public GameObject player;
    public UnityEngine.CharacterController charCont;
    public static bool onVine = false;


    void Start()
    {
        charCont = player.GetComponent<UnityEngine.CharacterController>();
    }



    // Update is called once per frame
    void Update()
    {
        if (onVine == true && Input.GetKey(KeyCode.W)) // if player is on ladder and W is being held, move the player up
        {
            charCont.Move(new Vector3(0, 3 * Time.deltaTime, 0));
        }
    }

    private void OnTriggerEnter(Collider collider) // when player enters the ladder trigger, set onLadder to true
    {
        if (collider.transform.root == player.transform)
        {
            onVine = true;
        }
    }

    private void OnTriggerExit(Collider collider) // when player exits the ladder trigger, set onLadder to false
    {
        if (collider.transform.root == player.transform)
        {
            onVine = false;
        }
    }
}
