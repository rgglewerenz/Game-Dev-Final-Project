using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : GenericPickupScipt<PlayerGunHandler>
{
    public GameObject gunPrefab;

    public AudioClip pickupSound;


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
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        return true;
    }
}
