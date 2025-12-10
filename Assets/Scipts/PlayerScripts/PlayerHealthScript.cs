using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthScript : GenericHealthScript
{
    private float invincibilityDuration = 1.0f;

    private float lastDamageTime = 0f;

    [SerializeField]
    private AudioClip GameOverSound;

    [SerializeField]
    private AudioClip damageSound;
    protected override void Die()
    {
        if(GameOverSound != null)
        {
            AudioSource.PlayClipAtPoint(GameOverSound, transform.position);
        }
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
        if (damageSound != null)
        {
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }
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

    public void SetHealth(float ammount) {
        SetCurrentHealth(ammount);
        GameManager.Instance.SetPlayerHealthText($"{CurrentHealth} Hp");
    }


}
