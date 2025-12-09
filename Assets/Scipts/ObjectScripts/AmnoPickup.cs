using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmnoPickup : GenericPickupScipt<PlayerGunHandler>
{
    protected override bool OnPickup(PlayerGunHandler picker)
    {
        if (picker.IsCurrentGunAmmoFull())
        {
            return false;
        }
        picker.AddAmmoToGun();
        return true;
    }
}
