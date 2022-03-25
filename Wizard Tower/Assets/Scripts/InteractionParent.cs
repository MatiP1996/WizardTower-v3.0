using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionParent : MonoBehaviour
{
    // the parent of the interaction classes

    public string defaultMessage = "E - Interact";
    public string firstMessage = "E - Interact";
    public string alternateMessage = "Hello!";


  //  public GameObject player;
    //public AudioSource AudioSource;

  //  public AudioClip CurrentClip;
  //  public AudioClip AudioClip;
  //  public AudioClip AlternativeClip;


    void Start()
    {
        defaultMessage = firstMessage;
   //     Debug.Log("Parent");

        //   player = GameObject.Find("Player");
        //    AudioSource = Player.GetComponent<AudioSource>();

        //  CurrentClip = AudioClip;
    }

    public virtual string Communicate()
    {
        return defaultMessage;
    }

    public virtual List<int> Activate(List<int> playerItems)
    {
        defaultMessage = alternateMessage;
      //  AudioSource.PlayOneShot(currentClip);
        //currentClip = alternativeClip;

        return playerItems;
    }


    public virtual void ResetState()
    {
        defaultMessage = firstMessage;
    }

}
