using UnityEngine;

public class GenericDamage : MonoBehaviour
{
    [Tooltip("Cuanto daño hace")]
    [SerializeField] private float damageAmount;
    [Tooltip("Capa en la que buscar al objetivo")]
    [SerializeField] private LayerMask attackableLayer;

    protected IDamage damage;
    protected Collider2D target;
    protected bool damaged = false;

    protected void Attack()
    {
        target = Physics2D.OverlapCircle(transform.position, 0.5f, attackableLayer);

        if (target != null)
        {
            damage = target.gameObject.GetComponent<IDamage>();

            if (damage != null)
            {
                damaged = true;
                damage.Damage(damageAmount);
            }
        }
        else
        {
            damage = null;
        }
    }
}