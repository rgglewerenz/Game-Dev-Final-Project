using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmnoPickup : GenericPickupScipt<PlayerGunHandler>
{
    public int AmmoAmount = 10;

    protected override void OnPickup(PlayerGunHandler picker)
    {
        
    }
}
