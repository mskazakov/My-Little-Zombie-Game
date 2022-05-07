using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLives : MonoBehaviour
{
    public Slider enemySlider;
    public int damageToKill;

    private int currentDamage;

    GameManager gameManagerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        enemySlider.maxValue = damageToKill;
        enemySlider.value = 0;
        enemySlider.fillRect.gameObject.SetActive(false);

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillEnemy (int amount)
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
