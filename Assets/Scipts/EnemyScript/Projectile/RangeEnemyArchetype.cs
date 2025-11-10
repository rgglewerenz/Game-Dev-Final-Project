using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyArchetype : GenericEnemyLogic
{
    [SerializeField]
    private float attackRange = 15f;
    [SerializeField]
    private float speed = 3.5f;
    [SerializeField]
    private float attackDamage = 10.0f;
    [SerializeField]
    private float attackCooldown = 1.5f;

    [SerializeField]
    private GameObject projectilePrefab;


    private float lastAttackTime = 0.0f;
    GameObject player;




    protected override void AttackTarget()
    {
        if(lastAttackTime > 0)
            return;


        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position + direction, Quaternion.LookRotation(direction));
        lastAttackTime = attackCooldown;
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
        if (lastAttackTime > 0)
            lastAttackTime -= Time.deltaTime;
    }



    protected override Vector3 GetTargetPos()
    {
        if(ObjectVisable(player))
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



}
