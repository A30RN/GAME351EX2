using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    // Referencing the vehicles
    public GameObject vehicle1;
    public GameObject vehicle2;
    public GameObject vehicle3;
    private GameObject currentFollowing;

    [Header("Camera Settings")]
    public float distance = 65f;   // Distance behind the vehicle
    public float height = 30f;     // Height above the vehicle
    public float smoothSpeed = 0.125f;  // Smoothing factor for movement

    void Start()
    {
        currentFollowing = vehicle1;
    }

    void LateUpdate()
    {
        // Calculate desired position:
        //   - Start at the vehicle's position.
        //   - Move back along the vehicle's forward vector.
        //   - Raise the camera by 'height'.
        Vector3 desiredPosition = currentFollowing.transform.position
                                  - currentFollowing.transform.forward * distance
                                  + Vector3.up * height;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Calculate a look-at point on the vehicle.
        // This can be adjusted (e.g., raising it slightly) so the camera looks at the target's back.
        Vector3 lookAtPoint = currentFollowing.transform.position + Vector3.up * (height * 0.5f);
        transform.LookAt(lookAtPoint);
    }

    void Update()
    {
        // Change between vehicles
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentFollowing == vehicle1)
            {
                currentFollowing = vehicle2;
            }
            else if (currentFollowing == vehicle2)
            {
                currentFollowing = vehicle3;
            }
            else if (currentFollowing == vehicle3)
            {
                currentFollowing = vehicle1;
            }
        }
    }
}
