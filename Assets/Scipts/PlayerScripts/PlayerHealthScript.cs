using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthScript : GenericHealthScript
{
    private float invincibilityDuration = 1.0f;

    private float lastDamageTime = 0f;

    protected override void Die()
    {
        GameManager.Instance.GameOver();
    }

    protected override void OnStart()
    {
    }


    protected override void OnUpdate()
    {
        if (lastDamageTime > 0f)
        {
            lastDamageTime -= Time.deltaTime;
            if (lastDamageTime <= 0f)
            {
                isInvincible = false;
            }
        }
    }

    protected override void OnDamageTaken(float damageAmount)
    {
        isInvincible = true;
        Debug.Log("Player took " + damageAmount + " damage!");
        Debug.Log("Current Health: " + CurrentHealth);
        lastDamageTime = invincibilityDuration;
        GameManager.Instance.SetPlayerHealthText($"{CurrentHealth} Hp");
    }

    protected override void OnHeal(float healAmount)
    {
        Debug.Log("Current Health: " + CurrentHealth);
        GameManager.Instance.SetPlayerHealthText($"{CurrentHealth} Hp");
    }

}
