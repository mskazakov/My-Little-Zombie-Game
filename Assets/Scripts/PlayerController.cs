using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera mainCamera;
    GameManager gameManagerScript;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    public float speed;
    float playerBoundZ = 4.75f;
    float playerBoundX = 10.75f;

    float invincibilityLenth = 3.0f;
    float invincibilityCounter;
    public Renderer playerRenderer;
    public Renderer noseRenderer;
    float flashLenth = 0.1f;
    float flashCounter;

    public bool hasPowerup = false;
    public bool shoot = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.isGameActive)
        {
            // makes the player move
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
                    
            transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime, Space.World);
            transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime, Space.World);
        
            // makes player look at the direction of the mouse
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            // Makes player shoot projectile
            if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && !hasPowerup && !shoot)
            {
                Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
                shoot = true;
                StartCoroutine(PowerupShotsRoutine());
            } else if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)) && hasPowerup && !shoot)
            {
                Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
                shoot = true;
                StartCoroutine(PowerupShotsRoutine());
            }
        }

        // constrains the movement on Z axis
        if (transform.position.z >= playerBoundZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, playerBoundZ);
        } else if (transform.position.z <= -playerBoundZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -playerBoundZ);
        }

        // constrains the movement on X axis
        if (transform.position.x >= playerBoundX)
        {
            transform.position = new Vector3(playerBoundX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -playerBoundX)
        {
            transform.position = new Vector3(-playerBoundX, transform.position.y, transform.position.z);
        }

        // starts invincibility timer and blinking
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                noseRenderer.enabled = !noseRenderer.enabled;
                flashCounter = flashLenth;
            }

            if (invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
                noseRenderer.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && invincibilityCounter <= 0)
        {
            Debug.Log("You got hit by " + other.gameObject.name);
            gameManagerScript.AddLives(-1);

            playerRenderer.enabled = false;
            noseRenderer.enabled = false;
            invincibilityCounter = invincibilityLenth;
            flashCounter = flashLenth;
            Debug.Log("You are invincible for " + invincibilityCounter + " secs");
        }

        if (other.gameObject.CompareTag("Powerup") && !hasPowerup)
        {
            Destroy(other.gameObject);
            Debug.Log("You picked up a powerup!");
            hasPowerup = true;
            ChangeColor();
            StartCoroutine(PowerupCDRoutine());
        }
    } 

    // powerup rundown timer
    IEnumerator PowerupCDRoutine()
    {
        yield return new WaitForSeconds(2);
        hasPowerup = false;
        ChangeColor();
    }

    // manages how fast the player shoots
    IEnumerator PowerupShotsRoutine()
    {
        if (!hasPowerup)
        {
            yield return new WaitForSeconds(0.25f);
        } else if (hasPowerup)
        {
            yield return new WaitForSeconds(0.05f);
        }
        shoot = false;
    }

    private void ChangeColor()
    {
        if (hasPowerup)
        {
            Material material = playerRenderer.material;
            Material nosematerial = noseRenderer.material;
            material.color = Color.magenta;
            nosematerial.color = Color.magenta;
        } else if (!hasPowerup)
        {
            Material material = playerRenderer.material;
            Material nosematerial = noseRenderer.material;
            material.color = new Color(0.1910377f, 0.5816671f, 0.764151f, 1);
            nosematerial.color = new Color(0.1910377f, 0.5816671f, 0.764151f, 1);
        }
    }
} 
