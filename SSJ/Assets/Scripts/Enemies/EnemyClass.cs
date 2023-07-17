using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public float maxHP = 100.0f;
    public float speed = 3f;
    public bool EnemyActive = false;

    public GameObject player;
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 curPos;
    private Vector2 LastPlayerPos; // Последнее видимое положение игрока
    public Vector2 targetPoint; // Точка к которой двигается враг
    public bool PlayerFound = false; // Был ли обнаружен игрок
    private RaycastHit2D hit;


    // Смена анимаций
    //====================================================================================
    private string curAnim;
    public void ChangeAnimation(string anim)
    {
        if (curAnim == anim) return;
        animator.Play(anim);
        curAnim = anim;
    }
    //====================================================================================

    // Движение прямо к точке
    //====================================================================================
    public void MoveToPoint(Vector2 point)
    {
        ChangeAnimation("Walk");
        if (correctPoint != new Vector2(0,0)) { point = correctPoint;}
        Debug.DrawRay(curPos, point-curPos, Color.red);
        if (Vector2.Distance(curPos, point) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, point, speed * Time.fixedDeltaTime);
            //rb.velocity = point * speed * Time.fixedDeltaTime;
        }
    }
    //====================================================================================


    // Направляется к последней точке, где игрока было видно
    //====================================================================================
    public void FollowPlayer(float dist)
    {
        float PLdistance = Vector2.Distance(transform.position, player.transform.position);
        curPos = transform.position;

        Vector2 direction = (player.transform.position - transform.position).normalized;
        Debug.DrawRay(transform.position, direction*dist, Color.yellow);

        hit = Physics2D.Raycast(transform.position, direction, distance:dist);
        if (hit != false)
        {
            if (hit.collider.gameObject.name == "Player" && PLdistance < dist)
            {
                if (!EnemyActive)
                {
                    EnemyActive = true;
                    ChangeAnimation("Activation");
                }

                PlayerFound = true;
                LastPlayerPos = player.transform.position;
                targetPoint = LastPlayerPos;
            }
        }
    }
    //====================================================================================


    // Патрулирование местности
    //====================================================================================
    private float waitTime;
    public void Patrol(float PRange, float stWaitTime)
    {
        if (waitTime <= 0)
        {
            waitTime = stWaitTime;
            targetPoint = new Vector2(transform.position.x + 2f*Random.Range(-PRange, PRange), transform.position.y + 2f*Random.Range(-PRange, PRange));
        }
        else
        {
            waitTime -= Time.deltaTime;
            MoveToPoint(targetPoint);
        }
    }
    //====================================================================================


    // Избегание стен
    //====================================================================================
    private float sideVecLenght;
    private Vector2 correctPoint;
    public void CheckWalls()
    {
        correctPoint = new Vector2(0,0);
        Vector2 moveDirection = (targetPoint - curPos).normalized*2f;

        Vector2 sideVec = moveDirection * 1.5f;
        Vector2 sideVec1 = Quaternion.Euler(0, 0, 45) * sideVec;
        Vector2 sideVec2 = Quaternion.Euler(0, 0, -45) * sideVec;
        sideVecLenght = Vector2.Distance(curPos, sideVec);

        Debug.DrawRay(curPos, moveDirection, Color.cyan);
        Debug.DrawRay(curPos, sideVec1, Color.cyan);
        Debug.DrawRay(curPos, sideVec2, Color.cyan);

        RaycastHit2D hitRight = Physics2D.Raycast(curPos, sideVec2, distance:sideVecLenght);
        RaycastHit2D hitLeft = Physics2D.Raycast(curPos, sideVec1, distance:sideVecLenght);

        if (hitRight != false)
        {
            if (hitRight.distance < 2.6f)
            {
                correctPoint = curPos + moveDirection + sideVec1;
            }
        }
        if (hitLeft != false)
        {
            if (hitLeft.distance < 2.6f)
            {
                correctPoint = curPos + moveDirection + sideVec2;
            }
        }
    }
    //====================================================================================
}
