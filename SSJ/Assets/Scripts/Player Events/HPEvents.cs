using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPEvents : MonoBehaviour
{
    [SerializeField] private new GameObject gameObject;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private int maxHealthPoint = 6;
    private int healthPoint;
    [Tooltip(">0.2")]
    [SerializeField] private float damageTakingCD = 1f;
    [Tooltip(">0.2")]
    [SerializeField] private float hpRecoveryCD = 1f;
    [SerializeField] private Image[] hpImages = new Image[6];
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
            if (healthPoint == 1)
            {
                hpImages[healthPoint - 1].gameObject.SetActive(false);
                PlayerDeath();
                return;
            }

            hpImages[healthPoint - 1].gameObject.SetActive(false);
            healthPoint -= 1;

            StartCoroutine("HitAnimation");
        }
    }
    //восстановление хп
    public void HPRecovery()
    {
        if (hpIsRecovery && healthPoint < maxHealthPoint)
        {
            hpImages[healthPoint].gameObject.SetActive(true);
            healthPoint += 1;
            StartCoroutine("RegenAnimation");
        }
    }

    //анимация получения урона
    private IEnumerator HitAnimation()
    {
        damageIsTaking = false;
        sprite.color = new Color(1, 0, 0, 0.8f);
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
        Time.timeScale = 1f;
        deathPanel.SetActive(true);
    }
}
