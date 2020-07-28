using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GenericDamage : MonoBehaviour
{
    [Tooltip("Cuanto daño hace")]
    [SerializeField] protected float damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (TryGetComponent(out IParry parry))
        {
            parry.Parry();
            return;
        }

        IDamage damage = other.gameObject.GetComponent<IDamage>();

        if (damage != null)
        {
            damage.Damage(this.damage);
        }
    }
}
