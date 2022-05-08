using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemies : MonoBehaviour
{
    public GameManager gameManagerScript;

    Transform lookAtTarget;
    private float speed = 1.0f;

    public Slider enemySlider;
    public int damageToKill = 1;

    public int currentDamage;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        enemySlider = gameObject.GetComponentInChildren<Slider>();

        enemySlider.maxValue = damageToKill;
        enemySlider.value = 0;
        enemySlider.fillRect.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    // introduces the small kickback when collided withe the bullet
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            gameObject.transform.Translate(0, 0, -0.1f);
        }
    }

    public virtual void MoveToPlayer()
    {
        lookAtTarget = GameObject.Find("Player").GetComponent<Transform>();

        if (gameManagerScript.isGameActive)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

            transform.LookAt(lookAtTarget);
        }
    }

    public virtual void KillEnemy(int amount)
    {
        currentDamage += amount;
        enemySlider.fillRect.gameObject.SetActive(true);
        enemySlider.value = currentDamage;

        if (currentDamage >= damageToKill)
        {
            gameManagerScript.AddScoreZ(gameManagerScript.zombieScore);
            Destroy(gameObject, 0.1f);
        }
    }
}
