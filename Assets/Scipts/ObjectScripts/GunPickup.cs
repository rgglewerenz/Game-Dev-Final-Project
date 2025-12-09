using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : GenericPickupScipt<PlayerGunHandler>
{
    public GameObject gunPrefab;

    void Start()
    {
        if (gunPrefab == null)
        {
            Debug.LogError("GunPickup: gunPrefab is not assigned.");
            return;
        }
        Instantiate(gunPrefab, transform);
    }

    protected override bool OnPickup(PlayerGunHandler picker)
    {
        if (picker.HasGun(gunPrefab.name))
        {
            return false;
        }
        picker.AddGun(gunPrefab);
        return true;
    }
}
