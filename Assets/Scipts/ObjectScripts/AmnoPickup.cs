using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmnoPickup : GenericPickupScipt<PlayerGunHandler>
{
    [SerializeField]
    private AudioClip pickupSound;

    protected override bool OnPickup(PlayerGunHandler picker)
    {
        if (picker.IsCurrentGunAmmoFull())
        {
            return false;
        }
        picker.AddAmmoToGun();
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        return true;
    }
}
