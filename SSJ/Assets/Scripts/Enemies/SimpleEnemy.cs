using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemy : MonoBehaviour
{
    //private Rigidbody2D rb;
    private EnemyClass enemy;

    public float viewDist = 15.0f; // Дальность обзора

    public float PRange = 3f; // Радиус патрулирования
    public float startWaitTime = 3f; // Время ожидания в точке патрулирования
    private float timeBeforePatrol = 5f;

    //====================================================================================
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyClass>();
        enemy.LastPlayerPos = this.transform.position;
    }
    //====================================================================================


    //====================================================================================
    void FixedUpdate()
    {
        enemy.FollowPlayer(viewDist);

        if (Vector2.Distance(this.transform.position, enemy.LastPlayerPos) < 0.4f)
        {
            enemy.PlayerFound = false;
        }

        if (enemy.PlayerFound)
        {
            enemy.MoveToPoint(enemy.LastPlayerPos);
            timeBeforePatrol = 5f;
        }

        if (enemy.PlayerFound == false && enemy.EnemyActive)
        {
            if (timeBeforePatrol <= 0f)
            {
                enemy.Patrol(PRange, startWaitTime);
            }
            else { timeBeforePatrol -= Time.fixedDeltaTime; }
        }
    }
    //====================================================================================
}
