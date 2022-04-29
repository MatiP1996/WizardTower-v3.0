using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : InteractionParent
{
    // Start is called before the first frame update
    private void Start()
    {
        firstMessage = "E - Launch Baloon";
    }

    public override List<int> Activate(List<int> playerItems)
    {
        return playerItems;
    }
    // Update is called once per frame
    void Update()
    {

    }

}