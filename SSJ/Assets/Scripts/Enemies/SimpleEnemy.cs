using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemy : MonoBehaviour
{
    //private Rigidbody2D rb;
    private EnemyClass enemy;

    public float viewDist = 15.0f; // Дальность обзора

    public float dmRange = 3f;
    public float dmCooldown = 0.6f;

    public float PRange = 3f; // Радиус патрулирования
    public float startWaitTime = 3f; // Время ожидания в точке патрулирования
    private float timeBeforePatrol = 5f;
    private float activationTime = 0.75f;

    //====================================================================================
    void Start()
    {
        //enemy.rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyClass>();
        enemy.targetPoint = this.transform.position;
        enemy.animator = GetComponent<Animator>();
    }
    //====================================================================================


    //====================================================================================
    void FixedUpdate()
    {
        if (!enemy.EnemyActive) { enemy.ChangeAnimation("Idle(NotActive)"); }

        enemy.FollowPlayer(viewDist);

        if (Vector2.Distance(this.transform.position, enemy.targetPoint) < 0.4f)
        {
            enemy.PlayerFound = false;
        }

        if (enemy.PlayerFound)
        {
            if (activationTime <= 0f)
            {
                enemy.CheckWalls();
                enemy.MoveToPoint(enemy.targetPoint);
                timeBeforePatrol = 5f;
                if (Vector2.Distance(this.transform.position, enemy.player.transform.position) < dmRange && enemy.isDamaging==false) 
                { StartCoroutine(enemy.Damage(dmCooldown)); }
            }
            else { activationTime -= Time.fixedDeltaTime; }
        }

        if (enemy.PlayerFound == false && enemy.EnemyActive)
        {
            if (timeBeforePatrol <= 0f)
            {
                enemy.Patrol(PRange, startWaitTime);
                activationTime = 0.75f;
            }
            else { timeBeforePatrol -= Time.fixedDeltaTime; }
        }
    }
    //====================================================================================
}
