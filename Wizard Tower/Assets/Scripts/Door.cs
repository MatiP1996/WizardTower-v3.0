using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractionParent
{
    // child class of interaction parent

    // Start is called before the first frame update


    void Start()
    {

     //   Debug.Log("Child");


      //  Player = GameObject.Find("Player");
     //   AudioSource = Player.GetComponent<AudioSource>();

       // CurrentClip = AudioClip;
    }

    public override List<int> Activate(List<int> playerItems)
    {
        //CurrentMessage = AlternateMessage;
        //AudioSource.PlayOneShot(CurrentClip);
       // CurrentClip = AlternativeClip;

        return playerItems;
    }

}
