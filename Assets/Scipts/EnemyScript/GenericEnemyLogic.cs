using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GenericEnemyLogic : MonoBehaviour
{
    private NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {

        OnUpdate();
        if (WithinRange())
        {
            AttackTarget();
        }
        else
        {
            agent.speed = GetSpeed();
            agent.SetDestination(GetTargetPos());
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

    protected abstract Vector3 GetTargetPos();

    protected abstract bool WithinRange();

    protected abstract void AttackTarget();

    protected abstract float GetSpeed();


    protected bool ObjectVisable(GameObject otherObject)
    {
        bool hitSomething = Physics.Raycast(transform.position, (otherObject.transform.position - transform.position).normalized, out RaycastHit hitInfo);

        if (hitSomething && hitInfo.collider.gameObject == otherObject)
            return true;
        return false;
    }
}
