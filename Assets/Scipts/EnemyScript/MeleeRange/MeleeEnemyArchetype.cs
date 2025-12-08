using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyArchetype : GenericEnemyLogic
{
    [SerializeField]
    private float speed = 3.5f;
    [SerializeField]
    private float attackDamage = 10.0f;
    [SerializeField]
    private float attackCooldown = 1.5f;

    private float lastAttackTime = 0.0f;

    GameObject player;

    protected override void OnStart()
    {
        player = GameObject.FindWithTag("Player");
    }


    protected override void OnUpdate()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
            return;
        }
        if (lastAttackTime > 0)
            lastAttackTime -= Time.deltaTime;
    }


    protected override void AttackTarget()
    {
        var playerHealth = player.GetComponent<PlayerHealthScript>();

        if(playerHealth == null)
            return;

        if (lastAttackTime <= 0)
        {
            playerHealth.TakeDamage(attackDamage);
            lastAttackTime = attackCooldown;
        }
    }

    protected override float GetSpeed()
    {
        return speed;
    }

    protected override GameObject GetTarget()
    {
        return player;
    }

    protected override float GetAttackRange()
    {
        return attackRange;
    }

    protected override float GetDetectionRange()
    {
        return detectionRange;
    }
}
