using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GenericDamage : MonoBehaviour
{
    [Tooltip("Cuanto daño hace")]
    [SerializeField] protected float damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.Damage(damage);
        }
    }
}
