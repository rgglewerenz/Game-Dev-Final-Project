using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bullet;
    public float fire_rate = 1f;
    public int shots_per_fire = 1;
    public float queue_input_length = 0.5f;
    public int ammo_per_shot = 1;
    public int ammo_capacity = 30;
    public int damage = 10;
    public Transform bullet_spawn;

    float shot_queue_time = 0f;
    float lastShot = 0f;
    float ammo_count = 30f;
    bool shoot = false;

    void Start()
    {
        if (bullet != null)
            bullet.GetComponent<ProjectileScript>().SetDamage(damage); // Set damage on bullet prefab if assigned (Gun damage pef over bullet damage)
        ammo_count = ammo_capacity;
    }

    void Update()
    {
        if(lastShot > 0)
            lastShot -= Time.deltaTime;


        if(shot_queue_time + queue_input_length > Time.time && lastShot <= 0 && shoot && shot_queue_time != 0)
        {
            Fire();
            shoot = false;
        }
    }

    public void QueueFire()
    {
        shot_queue_time = Time.time;
        shoot = true;
    }

    public void Fire()
    {
        if (ammo_count < ammo_per_shot)
            return;

        SpawnBullets();

        lastShot = fire_rate;
        ammo_count -= ammo_per_shot;
    }

    private void SpawnBullets() {
        if (bullet == null)
        {
            if (Physics.Raycast(bullet_spawn.position, bullet_spawn.forward, out RaycastHit hit))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider == null)
                {
                    return;
                }

                var health = hit.collider.GetComponent<GenericHealthScript>();
                if (health != null)
                {
                    Debug.Log("Damage");
                    health.TakeDamage(damage);
                }
            }
            return;
        }

        for (int i = 0; i < shots_per_fire; i++)
        {
            Instantiate(bullet, bullet_spawn.position, bullet_spawn.rotation);
        }
    }

}
