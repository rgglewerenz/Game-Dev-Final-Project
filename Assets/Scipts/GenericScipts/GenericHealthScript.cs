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

    protected bool isInvincible = false;

    private float currentHealth = -1;



    void Start()
    {
        if(currentHealth < 0f)
            currentHealth = maxHealth;
        OnStart();
    }

    void Update()
    {
        OnUpdate();
    }

    public void TakeDamage(float damageAmount)
    {
        if(isInvincible)
            return;
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
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHeal(healAmount);
    }

   
    protected virtual void OnDamageTaken(float damageAmount)
    {
        return;
    }

    protected virtual void OnHeal(float healAmount)
    {
        return;
    }

    protected virtual void OnStart()
    {
        return;
    }

    protected virtual void OnUpdate()
    {
        return;
    }

    protected void SetCurrentHealth(float healthAmount)
    {
        currentHealth = healthAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
