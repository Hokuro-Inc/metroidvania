using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : Drop, IDrop
{
    public FloatValue currency;

    public override void GiveToPlayer(int amount = 0)
    {
        currency.initialValue += amount;
        base.GiveToPlayer();
    }
}
