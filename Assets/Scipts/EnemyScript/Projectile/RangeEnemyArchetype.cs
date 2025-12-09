using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyArchetype : GenericEnemyLogic
{
    [SerializeField]
    private float speed = 3.5f;
    [SerializeField]
    private float attackDamage = 10.0f;

    [SerializeField]
    private GameObject projectilePrefab;

    GameObject player;




    protected override void AttackTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position + direction, Quaternion.LookRotation(direction));
    }

    protected override float GetSpeed()
    {
        return speed;
    }

    protected override void OnStart()
    {
        player = GameObject.FindWithTag("Player");
        projectilePrefab.GetComponent<ProjectileScript>().SetDamage(attackDamage);
    }

    protected override void OnUpdate()
    {
        if(player == null)
            player = GameObject.FindWithTag("Player");
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
