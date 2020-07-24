using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Tooltip("Cuanto daño hace el jugador")]
    public float damage;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Breakable"))
        {
            other.GetComponent<BreakableObject>().Destroy();
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<GenericHealth>().Damage(damage);
        }
    }*/
}
