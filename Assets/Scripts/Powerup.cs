using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float movementSpeed = 0.5f;
    private float amplitude = 0.3f;
    float elapsedTimePos = 3.0f;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(50.0f * Time.deltaTime, 50.0f * Time.deltaTime, 50.0f * Time.deltaTime);

        elapsedTimePos += Time.deltaTime * Time.timeScale * movementSpeed;
        transform.position = startPos + Vector3.up * Mathf.Sin(elapsedTimePos) * amplitude;
    }
}
