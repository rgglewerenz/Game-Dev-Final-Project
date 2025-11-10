using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : GenericHealthScript
{
    private float invincibilityDuration = 1.0f;

    private float lastDamageTime = 0f;


    protected override void Die()
    {
        Debug.Log("Player Died!");
        // Implement player death logic here (e.g., respawn, game over screen)
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
    }

}
