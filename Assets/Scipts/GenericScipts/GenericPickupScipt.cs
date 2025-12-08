using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPickupScipt<T> : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        T component = other.GetComponent<T>();
        if (component != null)
        {
            if (OnPickup(component)) {
                Destroy(gameObject);
            }
        }
    }

    protected abstract bool OnPickup(T picker);
}
