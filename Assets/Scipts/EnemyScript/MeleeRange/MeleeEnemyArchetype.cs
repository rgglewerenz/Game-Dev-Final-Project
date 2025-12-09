using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyArchetype : GenericEnemyLogic
{
    [SerializeField]
    private float speed = 3.5f;
    [SerializeField]
    private float attackDamage = 10.0f;

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
    }


    protected override void AttackTarget()
    {
        var playerHealth = player.GetComponent<PlayerHealthScript>();

        if(playerHealth == null)
            return;

        playerHealth.TakeDamage(attackDamage);
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
