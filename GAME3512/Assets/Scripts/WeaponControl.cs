using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileLifeSpan = 5f;
    public float speed = 20f;

    public void FireProjectile()
    {
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, spawnRotation);

        // Get the ProjectileMovement script and set speed
        ProjectileMovement movement = projectile.GetComponent<ProjectileMovement>();
        if (movement != null)
        {
            movement.SetVelocity(speed);
        }

        // Destroy after projectileLifeSpan seconds
        Destroy(projectile, projectileLifeSpan);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }
}