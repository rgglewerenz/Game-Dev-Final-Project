using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossHealth : EnemyHealthController
{
    protected override void Die()
    {
        if (deathEffectPrefab != null)
        {
            var gameobject = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameobject, 3f);
        }
        Destroy(this.gameObject);
        GameManager.Instance.Win();
    }
}
