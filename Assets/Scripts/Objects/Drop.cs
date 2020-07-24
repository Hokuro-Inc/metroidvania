using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [Tooltip("Señal que avisa en caso de drop")]
    public Signal dropSignal;

    public virtual void GiveToPlayer(int amount = 0)
    {
        dropSignal.Raise();
    }
}
