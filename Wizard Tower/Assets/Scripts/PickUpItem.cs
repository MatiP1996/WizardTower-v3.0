using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : InteractionParent
{
    public int itemId;
    // Start is called before the first frame update


    // Update is called once per frame
    public override List<int> Activate(List<int> playerItems)
    {
        gameObject.SetActive(false);
        playerItems.Add(itemId);
        return playerItems;
    }
}

