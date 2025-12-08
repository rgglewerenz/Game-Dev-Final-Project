using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : GenericHealthScript
{
    public GameObject deathEffectPrefab;


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
