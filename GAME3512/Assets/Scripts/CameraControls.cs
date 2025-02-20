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

    //refrence the weapon gameobject
    public GameObject weapon;

    [Header("Camera Settings")]
    public float distance = 65f;   // Distance behind the vehicle
    public float height = 30f;     // Height above the vehicle
    public float smoothSpeed = 0.125f;  // Smoothing factor for movement

    //refrence each car script
    private Vehicle1Movement vehicle1Movement;
    private Vehicle2Movement vehicle2Movement;
    private Vehicle3Movement vehicle3Movement;

    //refrence weapon script
    private WeaponControl weaponControl;

    //refrence Audio Sources
    private AudioSource engineSound;

    private AudioSource vehicle1Sound;
    private AudioSource vehicle2Sound;
    private AudioSource vehicle3Sound;

    void Start()
    {
        currentFollowing = vehicle1;

        //get refrences for each script
        vehicle1Movement = vehicle1.GetComponent<Vehicle1Movement>();
        vehicle2Movement = vehicle2.GetComponent<Vehicle2Movement>();
        vehicle3Movement = vehicle3.GetComponent<Vehicle3Movement>();
        weaponControl = weapon.GetComponentInChildren<WeaponControl>();

        //enanble the correct script
        vehicle1Movement.enabled = true;
        vehicle2Movement.enabled = false;
        vehicle3Movement.enabled = false;
        weaponControl.enabled = false;

        //refrence the sounds components
        vehicle1Sound = vehicle1.GetComponent<AudioSource>();
        vehicle2Sound = vehicle2.GetComponent<AudioSource>();
        vehicle3Sound = vehicle3.GetComponent<AudioSource>();

        //enable the correct audio
        vehicle1Sound.Play();
        vehicle2Sound.Stop();
        vehicle3Sound.Stop();

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
                vehicle1Movement.enabled = false;
                vehicle2Movement.enabled = true;
                vehicle3Movement.enabled = false;
                weaponControl.enabled = true;

                vehicle1Sound.Stop();
                vehicle2Sound.Play();
                vehicle3Sound.Stop();
            }
            else if (currentFollowing == vehicle2)
            {
                currentFollowing = vehicle3;
                vehicle1Movement.enabled = false;
                vehicle2Movement.enabled = false;
                vehicle3Movement.enabled = true;
                weaponControl.enabled = false;

                vehicle1Sound.Stop();
                vehicle2Sound.Stop();
                vehicle3Sound.Play();
            }
            else if (currentFollowing == vehicle3)
            {
                currentFollowing = vehicle1;
                vehicle1Movement.enabled = true;
                vehicle2Movement.enabled = false;
                vehicle3Movement.enabled = false;
                weaponControl.enabled = false;

                vehicle1Sound.Play();
                vehicle2Sound.Stop();
                vehicle3Sound.Stop();
            }
        }
    }
}