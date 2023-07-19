using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpSystem : MonoBehaviour
{
    public int maxHP = 3;

    // Получение урона
    //====================================================================================
    public void TakingDamage()
    {
        maxHP -= 1;
        if (maxHP == 0)
        {
            Death();
        }
    }
    //====================================================================================


    // Смерть врага
    //====================================================================================
    public IEnumerator Death()
    {
        yield return null;
    }
    //====================================================================================
}
