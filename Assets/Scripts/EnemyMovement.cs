using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameManager gameManagerScript;
    
    Transform lookAtTarget;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        lookAtTarget = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.isGameActive)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

            transform.LookAt(lookAtTarget);
        }
    }

    // introduces the small kickback when collided withe the bullet
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            gameObject.transform.Translate(0,0,-0.1f);
        }
    }
}
