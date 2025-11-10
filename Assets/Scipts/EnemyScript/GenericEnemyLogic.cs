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
        if (WithinRange())
        {
            AttackTarget();
        }
        else
        {
            agent.speed = GetSpeed();
            agent.SetDestination(GetTargetPos());
        }
        OnUpdate();
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

}
