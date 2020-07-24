using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : Drop, IDrop
{
    [Tooltip("Controla el display de la barra de vida")]
    public HealthManager HpManager;

    // Entrega el drop al jugador
    public override void GiveToPlayer(int amount = 0)
    {
        HpManager.IncreaseHealth();
        base.GiveToPlayer();
    }
}
