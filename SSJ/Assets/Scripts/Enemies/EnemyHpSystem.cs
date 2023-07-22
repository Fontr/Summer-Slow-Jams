using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpSystem : MonoBehaviour
{
    public int maxHP = 3;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Получение урона
    //====================================================================================
    public IEnumerator TakingDamage()
    {
        maxHP -= 1;
        if (maxHP == 0)
        {
            StartCoroutine(Death());
        }
        sprite.color = new Color(1, 0, 0, 0.8f);
        yield return new WaitForSeconds(0.2f);
        sprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }
    //====================================================================================


    // Смерть врага
    //====================================================================================
    public IEnumerator Death()
    {
        gameObject.GetComponent<Animator>().Play("Death");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
    //====================================================================================
}
