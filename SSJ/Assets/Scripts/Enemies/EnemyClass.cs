using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public float maxHP = 100.0f;
    public float speed = 3.0f;
    private bool isMoving;

    public GameObject player;
    private Vector2 LastPlayerPos; // ѕоследнее видимое положение игрока

    // ƒвижение пр€мо к игроку
    //====================================================================================
    public void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }
    //====================================================================================


    // Ќаправл€етс€ к последней точке, где игрока было видно
    //====================================================================================
    public void FollowPlayer(float dist)
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Debug.DrawRay(transform.position, direction*dist, Color.yellow);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance:dist);
        if (hit.collider.gameObject.name == "Player")
        {
            LastPlayerPos = player.transform.position;
            MoveToPlayer();
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, LastPlayerPos, speed * Time.fixedDeltaTime);
        }
    }
    //====================================================================================


    /*
    public void FindPath()
    {
        PLdistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;
    }   */
}
