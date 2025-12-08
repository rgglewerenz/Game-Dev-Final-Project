using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupScript : GenericPickupScipt<GenericHealthScript>
{

    public int HealthAmount = 20;

    public bool AnyHealthPickup = false;


    protected override bool OnPickup(GenericHealthScript picker)
    {
        if(AnyHealthPickup)
        {
            if (picker.CurrentHealth >= picker.maxHealth)
                return false;
            picker.Heal(HealthAmount);
        }
        else
        {
            if(picker.gameObject.GetComponent<PlayerHealthScript>() != null)
            {
                PlayerHealthScript playerHealth = picker.gameObject.GetComponent<PlayerHealthScript>();
                if (playerHealth.CurrentHealth >= playerHealth.maxHealth)
                    return false;
                playerHealth.Heal(HealthAmount);
            }
        }
        return true;
    }

}
