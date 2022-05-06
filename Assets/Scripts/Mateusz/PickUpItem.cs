using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : InteractionParent
{
    public int itemId;
    
    float timeActivate = -1;
    InteractionManager player;
    // Start is called before the first frame update


    private void Start()
    {
        source = GetComponent<AudioSource>();
        player = GameObject.Find("Main Camera").GetComponent<InteractionManager>();
    }

    private void Update()
    {
        if (timeActivate != -1)
        {
            float currentTime = Time.time;
            if(currentTime >= timeActivate + pauseTime)
            {
                player.itemIDs.Add(itemId);
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    public override float Activate()
    {
        source.PlayOneShot(defaultClip);
        gameObject.layer = 0; 
        timeActivate = Time.time;

        return pauseTime;
    }
}

