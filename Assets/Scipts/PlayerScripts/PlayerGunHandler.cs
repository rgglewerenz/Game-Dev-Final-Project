using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGunHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> guns = new List<GameObject>();
    private int selected_gun = 0;
    [SerializeField]
    public Transform GunHolder;

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
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
        guns[selected_gun].SetActive(true);
    }

    public void SelectGun(int index)
    {
        if (index < 0 || index >= guns.Count)
        {
            return;
        }
        selected_gun = index;
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
        guns[selected_gun].SetActive(true);

    }

    public void FireGun()
    {
        guns[selected_gun].GetComponent<GunScript>().QueueFire();
    }


    private int wrapper(int starting, int direction, int count)
    {
        var new_index = starting + direction % count;

        if(new_index == count)
            return 0;

        if (new_index < 0)
            return count + new_index;

        return new_index;
    }


    public void AddGun(GameObject gun)
    {
        var gun_object = Instantiate(gun, GunHolder);
        gun_object.SetActive(false);
        guns.Add(gun_object);
    }

    public void AddAmmoToGun(int amount)
    {
        guns[selected_gun].GetComponent<GunScript>().AddAmmo(amount);
    }

    public void LoadGunsFromString()
    {

    }

    public bool IsCurrentGunAmmoFull()
    {
        return guns[selected_gun].GetComponent<GunScript>().IsAmmoFull();
    }
}
