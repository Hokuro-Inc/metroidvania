using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : Drop, IDrop
{
    public Inventory playerInventory;

    public override void GiveToPlayer(int amount = 0)
    {
        playerInventory.currency += amount;
        base.GiveToPlayer();
    }
}
