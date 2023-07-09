using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public EnemyClass enemy;

    private float PLdistance;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        PLdistance = Vector2.Distance(transform.position, enemy.player.transform.position);
        Vector2 direction = (enemy.player.transform.position - transform.position).normalized;
        enemy.MoveToPlayer();
        //rb.MovePosition(direction);
    }
}
