using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle2Movement : MonoBehaviour
{
    public float speed = 20.0f;
    public float turnSpeed = 55.0f;
    public float floatHeight = 15.0f;
    private float horizontalInput;
    private float fowardInput;

    // Start is called before the first frame update
    void Start()
    {

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
    }

    void FloatAboveTerrain()
    {
        float terrainHeight = Terrain.activeTerrain.SampleHeight(transform.position);

        float targetY = terrainHeight + floatHeight;

        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }
}
