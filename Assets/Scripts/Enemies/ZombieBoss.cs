using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieBoss : Enemies
{
    public override void KillEnemy(int amount)
    {
        currentDamage += amount;
        enemySlider.fillRect.gameObject.SetActive(true);
        enemySlider.value = currentDamage;

        if (currentDamage >= damageToKill)
        {
            gameManagerScript.AddScoreB(gameManagerScript.strongZombieScore);
            Destroy(gameObject, 0.1f);
        }
    }
}
