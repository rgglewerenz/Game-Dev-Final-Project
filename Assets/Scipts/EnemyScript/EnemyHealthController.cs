using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealthController : GenericHealthScript
{
    private bool dead = false;

    public GameObject deathEffectPrefab;

    [SerializeField]
    private AudioClip damageSound;

    [SerializeField]
    private float FlashDuration = 0.2f;

    [SerializeField]
    private Color damageColor = new Color(0.25f, 0f, 0f, 0.5f);
    Color originalColor;

    [SerializeField]
    List<SpawnerItem> OnDeathItems = new List<SpawnerItem>();

    [SerializeField]
    float itemSpawnChance = 0.5f; // 50% chance to spawn an item on death


    [SerializeField]
    Transform itemSpawnPoint;

    bool changingColor = false;


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
        if(changingColor)
            return;
        MeshRenderer spriteRenderer = GetComponent<MeshRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.material.color;
            spriteRenderer.material.color = damageColor;
            Invoke("RevertColor", FlashDuration);
        }
        changingColor = true;
    }
    private void RevertColor()
    {
        MeshRenderer spriteRenderer = GetComponent<MeshRenderer>();
        spriteRenderer.material.color = originalColor;
        changingColor = false;
    }

    protected override void Die()
    {
        if (dead)
            return;
        dead = true;

        // Spawn items on death based on chance
        var randomNumber = Random.Range(0, 1f);
        if (randomNumber > (1 - itemSpawnChance))
        {
            if (OnDeathItems != null && OnDeathItems.Count > 0)
            {
                Instantiate(SpawnerItem.ChooseItemFromList(OnDeathItems), itemSpawnPoint.position, Quaternion.identity);
            }
        }


        if (deathEffectPrefab != null)
        {
            var gameobject = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameobject, 3f);
        }
        Destroy(this.gameObject);
    }
}
