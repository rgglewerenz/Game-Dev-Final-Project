using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        if (HasGun(gun.GetComponent<GunScript>().PrefabName))
        {
            return;
        }
        var gun_object = Instantiate(gun, GunHolder);
        gun_object.SetActive(false);
        guns.Add(gun_object);
    }

    public bool HasGun(string gun_name)
    {
        return guns.Any(x => x.GetComponent<GunScript>().PrefabName.Contains(gun_name));
    }

    public void AddAmmoToGun(int amount)
    {
        guns[selected_gun].GetComponent<GunScript>().AddAmmo(amount);
    }

    public void LoadGunsFromStrings(IEnumerable<string> lines)
    {
        foreach(var item in guns)
        {
            Destroy(item.gameObject);
        }

        guns = new List<GameObject>();

        foreach (string line in lines)
        {
            var split = line.Split(':');
            var gun_name = split[0];
            var ammo_count = int.Parse(split[1]);
            var gun_prefab = GetGunPrefabByName(gun_name);
            if (gun_prefab != null)
            {
                AddGun(gun_prefab);
                guns[guns.Count - 1].GetComponent<GunScript>().SetAmmoCount(ammo_count);
            }
        }
        guns[0].SetActive(true);
    }

    public bool IsCurrentGunAmmoFull()
    {
        return guns[selected_gun].GetComponent<GunScript>().IsAmmoFull();
    }

    public string SirialzeGunsToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (GameObject gun in guns)
        {
            sb.AppendLine(gun.GetComponent<GunScript>().PrefabName + ":" + gun.GetComponent<GunScript>().GetAmmoCount().ToString());

        }
        return sb.ToString();
    }

    private GameObject GetGunPrefabByName(string gun_name)
    {
        var gun_prefabs = Resources.LoadAll<GameObject>("Prefabs/Guns/");
        foreach (var prefab in gun_prefabs)
        {
            if (prefab.name == gun_name)
            {
                return prefab;
            }
        }
        return null;
    }

}
