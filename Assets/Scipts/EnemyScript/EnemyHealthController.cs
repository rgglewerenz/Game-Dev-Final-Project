using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : GenericHealthScript
{
    public GameObject deathEffectPrefab;

    [SerializeField]
    private AudioClip damageSound;

    [SerializeField]
    private float FlashDuration = 0.2f;

    private Color damageColor = new Color(0.25f, 0f, 0f, 0.5f);
    Color originalColor;


    protected override void OnDamageTaken(float damageAmount)
    {
        base.OnDamageTaken(damageAmount);
        if (damageSound != null)
        {
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }
        FlashDamageColor();

    }

    private void FlashDamageColor()
    {
        MeshRenderer spriteRenderer = GetComponent<MeshRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.material.color;
            spriteRenderer.material.color = damageColor;
            Invoke("RevertColor", FlashDuration);
        }
    }

    private void RevertColor()
    {
        MeshRenderer spriteRenderer = GetComponent<MeshRenderer>();
        spriteRenderer.material.color = originalColor;
    }

    protected override void Die()
    {
        if (deathEffectPrefab != null)
        {
            var gameobject = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameobject, 3f);
        }
        Destroy(this.gameObject);
    }
}
