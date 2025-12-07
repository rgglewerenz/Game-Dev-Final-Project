using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : GenericPickupScipt<PlayerGunHandler>
{
    public GameObject gunPrefab;

    protected override void OnPickup(PlayerGunHandler picker)
    {
        picker.AddGun(Instantiate(gunPrefab, picker.transform));
    }
}
