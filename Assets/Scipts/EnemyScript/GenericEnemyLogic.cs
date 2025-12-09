using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GenericEnemyLogic : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    protected float detectionRange = 10.0f;

    [SerializeField]
    protected float attackRange = 2.0f;

    [SerializeField]
    protected float attackCooldown = 1.5f;

    [SerializeField]
    protected float attackCoolDownTimer = 0.0f;

    [SerializeField]
    private AudioClip AttackSound;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        OnStart();
        detectionRange = GetDetectionRange();
        attackRange = GetAttackRange();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCooldown > 0)
            attackCoolDownTimer -= Time.deltaTime;
        var target = GetTarget();
        OnUpdate();
        if (WithinAttackRange() && ObjectVisable())
        {
            agent.SetDestination(transform.position);
            this.transform.LookAt(target.transform.position);
            if (attackCoolDownTimer > 0)
                return;
            AttackTarget();
            if (AttackSound != null)
            {
                AudioSource.PlayClipAtPoint(AttackSound, transform.position);
            }
            attackCoolDownTimer = attackCooldown;
            return;
        }

        var spottedtarget = WithinDetectionRange() && ObjectVisable();

        if (spottedtarget)
        {
            agent.speed = GetSpeed();
            agent.SetDestination(GetTarget().transform.position);
            return;
        }


        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= agent.stoppingDistance)
        {
            SelectNextPatrolPoint();
            return;
        }        
    }

    protected virtual void OnStart()
    {
        return;
    }

    protected virtual void OnUpdate()
    {
        return;
    }

    protected abstract GameObject GetTarget();

    protected bool WithinDetectionRange()
    {
        var target = GetTarget();
        if (target == null)
            return false;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= detectionRange;
    }

    protected bool WithinAttackRange()
    {
        var target = GetTarget();
        if (target == null)
            return false;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= attackRange;
    }

    protected abstract void AttackTarget();

    protected abstract float GetSpeed();

    protected abstract float GetAttackRange();

    protected abstract float GetDetectionRange();

    protected bool ObjectVisable()
    {
        var otherObject = GetTarget();
        bool hitSomething = Physics.Raycast(transform.position, (otherObject.transform.position - transform.position).normalized, out RaycastHit hitInfo);

        if (hitSomething && hitInfo.collider.gameObject == otherObject)
            return true;
        return false;
    }

    void SelectNextPatrolPoint()
    {
        for (int i = 0; i < 10; i++)
        {
            // random point in circle on XZ plane
            Vector2 r = Random.insideUnitCircle * detectionRange;
            Vector3 candidate = transform.position + new Vector3(r.x, 0f, r.y);

            // sample navmesh near candidatewd
            NavMeshHit hit;
            if (NavMesh.SamplePosition(candidate, out hit, 2f, NavMesh.AllAreas))
            {
                // optionally check slope or area here (NavMesh hit.mask)
                agent.SetDestination(hit.position);
                return;
            }
        }
        agent.SetDestination(new Vector3(0, 0, 0));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
