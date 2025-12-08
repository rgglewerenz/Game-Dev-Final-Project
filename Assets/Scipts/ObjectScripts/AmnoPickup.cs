using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmnoPickup : GenericPickupScipt<PlayerGunHandler>
{
    public int AmmoAmount = 10;

    protected override bool OnPickup(PlayerGunHandler picker)
    {
        if (picker.IsCurrentGunAmmoFull())
        {
            return false;
        }
        picker.AddAmmoToGun(AmmoAmount);
        return true;
    }
}
