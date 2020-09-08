using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamage
{
    [SerializeField] private ExtendedFloatValue health;
    [SerializeField] private Signal healthSignal;

    public void Heal(float heal)
    {
        health.runtimeValue += heal;
        if (health.runtimeValue > health.maxValue)
        {
            health.runtimeValue = health.maxValue;
        }
        healthSignal.Raise();
    }

    public void Damage(float damage)
    {
        health.runtimeValue -= damage;
        if (health.runtimeValue <= 0)
        {
            Die();
        }
        healthSignal.Raise();
    }

    public void Die()
    {
        gameObject.GetComponentInParent<PlayerMovementTest>().gameObject.SetActive(false);
    }
}