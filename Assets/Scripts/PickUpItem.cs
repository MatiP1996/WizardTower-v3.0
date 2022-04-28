using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : InteractionParent
{
    public int itemId;
    public float delayTime = 1;
    float timeActivate = -1;
    InteractionManager player;
    // Start is called before the first frame update

    private void Start()
    {
        source = GetComponent<AudioSource>();
        player = GameObject.Find("Main Camera").GetComponent<InteractionManager>();
        Debug.Log(player);
    }

    private void Update()
    {
        if(timeActivate != -1)
        {
            float currentTime = Time.time;
            if(currentTime >= timeActivate + delayTime)
            {
                ///player.itemIDs.Add(itemId); FIX
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    public override List<int> Activate(List<int> playerItems)
    {
        source.PlayOneShot(defaultClip);
        gameObject.layer = 0; 
        timeActivate = Time.time;
        return playerItems;
    }
}

