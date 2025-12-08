using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : GenericPickupScipt<PlayerGunHandler>
{
    public GameObject gunPrefab;

    protected override bool OnPickup(PlayerGunHandler picker)
    {
        picker.AddGun(gunPrefab);
        return true;
    }
}
