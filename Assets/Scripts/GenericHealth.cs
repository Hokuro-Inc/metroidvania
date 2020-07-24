using UnityEngine;

public class GenericHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatValue health;
    private float currentHealth;

    void Start()
    {
        currentHealth = health.initialValue;
    }

    public virtual void Heal(float heal)
    {
        currentHealth += heal;
        if (currentHealth > health.initialValue)
        {
            currentHealth = health.initialValue;
        }
    }

    public virtual void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        this.gameObject.SetActive(false);
    }
}
