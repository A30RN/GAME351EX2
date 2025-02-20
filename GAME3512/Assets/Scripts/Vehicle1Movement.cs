using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Vehicle1Movement : MonoBehaviour
{
    public float speed = 50.0f;
    public float turnSpeed = 35.0f;
    public float floatHeight = 15.0f;
    private float horizontalInput;
    private float fowardInput;


    // Audio
    private Vector3 lastPosition;
    private AudioSource engineSound;

    public float minVolume = 0.2f;
    public float maxVolume = 1.0f;
    public float volumeChangeSpeed = 2.0f;

    public float minPitch = 0.3f;
    public float maxPitch = 0.8f;
    public float pitchChangeSpeed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        engineSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        fowardInput = Input.GetAxis("Vertical");

        // Moves the vehicle forward based on the verticle input
        transform.Translate(0, 0, 1 * Time.deltaTime * speed * fowardInput);
        // Rotates the car based on the horizontal input
        transform.Rotate(0, 1 * turnSpeed * horizontalInput * Time.deltaTime, 0);

        FloatAboveTerrain();

        AdjustEngineSound();
    }

    void FloatAboveTerrain()
    {
        float terrainHeight = Terrain.activeTerrain.SampleHeight(transform.position);

        float targetY = terrainHeight + floatHeight;

        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    void AdjustEngineSound()
    {
        if (engineSound == null) return; // Exit if no AudioSource is attached

        bool isMoving = transform.position != lastPosition; // Check if position has changed
        float targetVolume = isMoving ? maxVolume : minVolume;
        float targetPitch = isMoving ? maxPitch : minPitch;

        engineSound.volume = Mathf.Lerp(engineSound.volume, targetVolume, Time.deltaTime * volumeChangeSpeed); // Smoothly adjust volume
        engineSound.pitch = Mathf.Lerp(engineSound.pitch, targetPitch, Time.deltaTime * pitchChangeSpeed);

        lastPosition = transform.position; // Update last known position
    }
}
