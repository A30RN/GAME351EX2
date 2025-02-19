using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [Tooltip("Speed of the projectile")]
    private float velocity = 50.0f; // Default value, but will be set dynamically

    [Tooltip("Maximum projectile life in seconds")]
    private float timeToLive = 6.0f; // Can be removed if managed by WeaponControl

    void Update()
    {
        timeToLive -= Time.deltaTime;

        if (timeToLive > 0.0f)
        {
            transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        }

        if (timeToLive <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    // Setter method for velocity
    public void SetVelocity(float newVelocity)
    {
        velocity = newVelocity;
    }
}