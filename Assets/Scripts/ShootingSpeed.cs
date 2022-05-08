using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeed : MonoBehaviour
{
    GameManager gameManagerScript;

    public float speed;
    float boundZ = 9.0f;
    float boundX = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        if (transform.position.z >= boundZ)
        {
            Destroy(gameObject);
        } else if (transform.position.z <= -boundZ)
        {
            Destroy(gameObject);
        }

        if (transform.position.x >= boundX)
        {
            Destroy(gameObject);
        } else if (transform.position.x <= -boundX)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            
            if (other.gameObject.name == "Zombie(Clone)")
            {
                other.GetComponent<ZombieVanilla>().KillEnemy(1);
            } else if (other.gameObject.name == "Zombie_Strong(Clone)")
            {
                other.GetComponent<ZombieStronger>().KillEnemy(1);
            } else if (other.gameObject.name == "Zombie_Boss(Clone)")
            {
                other.GetComponent<ZombieBoss>().KillEnemy(1);
            }
        }
    }
}
