using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyArchetype : GenericEnemyLogic
{
    [SerializeField]
    private float attackRange = 2.0f;
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

    protected override Vector3 GetTargetPos()
    {
        if (ObjectVisable(player))
            return player.transform.position;
        else
            return transform.position;
    }

    protected override bool WithinRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange //Player within shooting Range
            && ObjectVisable(player)) //Line of sight check
        {
            return true;
        }
        return false;
    }

    protected override float GetSpeed()
    {
        return speed;
    }
}
