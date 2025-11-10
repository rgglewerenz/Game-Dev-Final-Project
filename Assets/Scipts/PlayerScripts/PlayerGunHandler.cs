using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> guns = new List<GameObject>();
    private int selected_gun = 0;

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
}
