using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericHealthScript : MonoBehaviour
{
    [SerializeField]
    public float maxHealth = 100f;

    [SerializeField]
    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        OnDamageTaken(damageAmount);
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    protected abstract void Die();

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        OnHeal(healAmount);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

   
    protected virtual void OnDamageTaken(float damageAmount)
    {
        return;
    }

    protected virtual void OnHeal(float healAmount)
    {
        return;
    }

}
