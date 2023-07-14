using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public float maxHP = 100.0f;
    public float speed = 3.0f;
    public bool EnemyActive = false;

    public GameObject player;
    public Vector2 LastPlayerPos; // Последнее видимое положение игрока
    public bool PlayerFound = false; // Был ли обнаружен игрок
    private RaycastHit2D hit;


    // Движение прямо к точке
    //====================================================================================
    public void MoveToPoint(Vector2 point)
    {
        if (Vector2.Distance(transform.position, point) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, point, speed * Time.fixedDeltaTime);
        }
    }
    //====================================================================================


    // Направляется к последней точке, где игрока было видно
    //====================================================================================
    public void FollowPlayer(float dist)
    {
        float PLdistance = Vector2.Distance(transform.position, player.transform.position);

        Vector2 direction = (player.transform.position - transform.position).normalized;
        Debug.DrawRay(transform.position, direction*dist, Color.yellow);

        hit = Physics2D.Raycast(transform.position, direction, distance:dist);
        if (hit != false)
        {
            if (hit.collider.gameObject.name == "Player" && PLdistance < dist)
            {
                EnemyActive = true;
                PlayerFound = true;
                LastPlayerPos = player.transform.position;
            }
        }
    }
    //====================================================================================


    // Патрулирование местности
    //====================================================================================
    private float waitTime;
    private Vector2 randomSpot;
    public void Patrol(float PRange, float stWaitTime)
    {
        if (waitTime <= 0)
        {
            waitTime = stWaitTime;
            randomSpot = new Vector2(transform.position.x + Random.Range(-PRange, PRange), transform.position.y + Random.Range(-PRange, PRange));
        }
        else
        {
            waitTime -= Time.deltaTime;
            MoveToPoint(randomSpot);
        }
    }
    //====================================================================================
}
