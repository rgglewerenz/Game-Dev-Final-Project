using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    [Header("Bullet settings")]
    [SerializeField]
    private float projectileSpeed = 0.25f;
    [SerializeField]
    private float lifeTime = 5f;
    [SerializeField]
    private float damage = 10f;
    [SerializeField]
    private bool CanPierce = false;
    [SerializeField]
    private string targetTag = "Enemy";



    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * projectileSpeed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == targetTag)
        {
            GenericHealthScript enemyHealth = other.gameObject.GetComponent<GenericHealthScript>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            if (!CanPierce)
            {
                Destroy(this.gameObject);
            }
            else
            {
                damage /= 2f; // Reduce damage by half when piercing
            }
        }

    }

}
