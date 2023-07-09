using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEvents : MonoBehaviour
{
    [SerializeField] private new GameObject gameObject;
    [SerializeField] private int maxHealthPoint = 6;
    private int healthPoint;
    [Tooltip(">0.2")]
    [SerializeField] private float damageTakingCD = 1f;
    [Tooltip(">0.2")]
    [SerializeField] private float hpRecoveryCD = 1f;
    private bool damageIsTaking = true, hpIsRecovery = true;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        healthPoint = maxHealthPoint;
    }
    void Update()
    {
        
    }

    //получение урона
    public void HPLoss()
    {
        if (damageIsTaking)
        {
            healthPoint -= 1;
            if (healthPoint < 1)
            {
                PlayerDeath();
                return;
            }
            StartCoroutine("HitAnimation");
        }
    }
    //восстановление хп
    public void HPRecovery()
    {
        if (hpIsRecovery && healthPoint < maxHealthPoint)
        {
            healthPoint += 1;
            StartCoroutine("RegenAnimation");
        }
    }

    //анимация получения урона
    private IEnumerator HitAnimation()
    {
        damageIsTaking = false;
        sprite.color = new Color(1, 0, 0, 1f);
        yield return new WaitForSeconds(0.2f);
        sprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(damageTakingCD - 0.2f);
        damageIsTaking = true;
    }

    //анимация восстановления хп
    private IEnumerator RegenAnimation()
    {
        hpIsRecovery = false;
        sprite.color = new Color(0, 1, 0, 1f);
        yield return new WaitForSeconds(0.2f);
        sprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(hpRecoveryCD - 0.2f);
        hpIsRecovery = true;
    }

    //смерть персонажа
    public void PlayerDeath()
    {
        sprite.color = new Color(0, 0, 0, 1f);
        Debug.Log("ты сдох");
    }
}
