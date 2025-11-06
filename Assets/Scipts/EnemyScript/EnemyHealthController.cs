using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : GenericHealthScript
{
    protected override void Die()
    {
        Destroy(this.gameObject);
    }
}
