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
    public int ammo_capacity = 10000;
    public int damage = 10;
    public int ammo_from_pickup = 30;
    public float cone_angle = 5f;
    public string PrefabName;

    public AudioClip gunSound;

    float shot_queue_time = 0f;
    float lastShot = 0f;
    int ammo_count = 10000;
    bool shoot = false;

    void Start()
    {
        if(ammo_count > ammo_capacity)
            ammo_count = ammo_capacity;
    }

    void Update()
    {
        OnUpdate();

        

        if (shot_queue_time + queue_input_length > Time.time && lastShot <= 0 && shoot && shot_queue_time != 0)
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
        if (bullet != null)
            bullet.GetComponent<ProjectileScript>().SetDamage(damage);
        if (ammo_count < ammo_per_shot)
            return;

        if (gunSound != null)
            AudioSource.PlayClipAtPoint(gunSound, transform.position);
        SpawnBullets();

        lastShot = fire_rate;
        ammo_count -= ammo_per_shot;
    }

    private void SpawnBullets() {
        var mainCamera = Camera.main;
        var bullet_spawn = mainCamera.transform;
        if (bullet == null)
        {
            if (Physics.Raycast(bullet_spawn.position + (2f * bullet_spawn.forward), bullet_spawn.forward, out RaycastHit hit))
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

        if(shots_per_fire <= 1)
        {
            Instantiate(bullet, bullet_spawn.position + (2f * bullet_spawn.forward), bullet_spawn.rotation);
            return;
        }

        for (int i = 0; i < shots_per_fire; i++)
        {
            float xangle = Random.Range(-cone_angle / 2, cone_angle / 2);
            float yangle = Random.Range(-cone_angle / 2, cone_angle / 2);
            Quaternion rotation = Quaternion.AngleAxis(xangle, bullet_spawn.up) * Quaternion.AngleAxis(yangle, bullet_spawn.right) * bullet_spawn.rotation;
            Instantiate(bullet, bullet_spawn.position + (2f * bullet_spawn.forward), rotation);
        }
    }

    public int GetAmmoCount()
    {
        return ammo_count;
    }

    public int GetAmmoCapacity()
    {
        return ammo_capacity;
    }

    public void AddAmmo()
    {
        ammo_count += ammo_from_pickup;
        if (ammo_count > ammo_capacity)
            ammo_count = ammo_capacity;
    }

    public bool IsAmmoFull()
    {
        return ammo_count >= ammo_capacity;
    }

    public void SetAmmoCount(int ammo)
    {
        ammo_count = ammo;
    }

    public void OnUpdate()
    {
        if (lastShot > 0)
            lastShot -= Time.deltaTime;
    }

    Vector3 GetWorldPoint()
    {
        var mouse = Input.mousePosition;
        mouse.z = 10f; // distance from camera
        return Camera.main.ScreenToWorldPoint(mouse);
    }

}
