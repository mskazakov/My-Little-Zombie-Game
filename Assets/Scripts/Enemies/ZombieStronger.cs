using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieStronger : Enemies
{
    public override void KillEnemy(int amount)
    {
        currentDamage += amount;
        enemySlider.fillRect.gameObject.SetActive(true);
        enemySlider.value = currentDamage;

        if (currentDamage >= damageToKill)
        {
            gameManagerScript.AddScoreZS(gameManagerScript.strongZombieScore);
            Destroy(gameObject);
            base.KillEnemy(amount);
        }
    }
}
