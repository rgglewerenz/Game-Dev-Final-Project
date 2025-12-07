using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGunHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> guns = new List<GameObject>();
    private int selected_gun = 0;

    void Update()
    {
        if (guns.Count == 0)
            return;
        GunScript gun_script = guns[selected_gun].GetComponent<GunScript>();
        GameManager.Instance.SetPlayerAmmoText(gun_script.GetAmmoCount().ToString() + " / " + gun_script.GetAmmoCapacity().ToString());
    }

    public void ChangeSelectedGun(int direction)
    {
        selected_gun = wrapper(selected_gun, direction, guns.Count);
    }

    public void FireGun()
    {
        guns[selected_gun].GetComponent<GunScript>().QueueFire();
    }


    private int wrapper(int starting, int direction, int count)
    {

        direction = direction % count;

        if (direction == 0)
        {
            return starting;
        }

        if(starting + direction > count)
        {
            return starting + direction - count;
        }

        if(starting + direction < 0)
        {
            return starting - direction + count;
        }


        return starting + direction;
    }


    public void AddGun(GameObject gun)
    {
        guns.Add(gun);
    }

    public void AddAmmoToGun(int amount)
    {
        guns[selected_gun].GetComponent<GunScript>().AddAmmo(amount);
    }

    public void LoadGunsFromString()
    {

    }
}
