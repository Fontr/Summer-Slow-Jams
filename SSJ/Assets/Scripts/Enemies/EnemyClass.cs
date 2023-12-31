using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public float speed = 3f;
    public bool EnemyActive = false;
    public bool isDamaging = false;

    public GameObject player;
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 curPos;
    private Vector2 LastPlayerPos; // ��������� ������� ��������� ������
    public Vector2 targetPoint; // ����� � ������� ��������� ����
    public bool PlayerFound = false; // ��� �� ��������� �����
    private RaycastHit2D hit;


    // ����� ��������
    //====================================================================================
    private string curAnim;
    public void ChangeAnimation(string anim)
    {
        if (curAnim == anim) return;
        animator.Play(anim);
        curAnim = anim;
    }
    //====================================================================================

    // �������� ����� � �����
    //====================================================================================
    public void MoveToPoint(Vector2 point)
    {
        if (isDamaging == false) {ChangeAnimation("Walk"); }
        if (correctPoint != new Vector2(0,0)) { point = correctPoint;}
        Debug.DrawRay(curPos, point-curPos, Color.red);
        if (point.x > curPos.x) { GetComponent<SpriteRenderer>().flipX = true; }
        else { GetComponent<SpriteRenderer>().flipX = false; }
        if (Vector2.Distance(curPos, point) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, point, speed * Time.fixedDeltaTime);
        }
    }
    //====================================================================================


    // ������������ � ��������� �����, ��� ������ ���� �����
    //====================================================================================
    public void FollowPlayer(float dist)
    {
        if (EnemyActive) { dist = 15f; }
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
                    if (gameObject.TryGetComponent(out AudioSource audio)) //!!!!!!!!!!!!!!!!!!
                    {
                        transform.localScale = new Vector3(15f, 15f, 0);
                        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1f);
                        audio.Play(0);
                    }
                }

                PlayerFound = true;
                LastPlayerPos = player.transform.position;
                targetPoint = LastPlayerPos;
                checkPoint = 0;
            }
        }
    }
    //====================================================================================


    // �������������� ���������
    //====================================================================================
    private float waitTime;
    private int checkPoint;
    private float deactivationTime;
    public void Patrol(float PRange, float stWaitTime)
    {
        if (checkPoint == 5)
        {
            ChangeAnimation("Deactivation");
            if (deactivationTime < 0) 
            {
                EnemyActive = false;
            }
            else { deactivationTime -= Time.fixedDeltaTime; }
        }
        else
        {
            deactivationTime = 0.73f;
            if (waitTime <= 0)
            {
                waitTime = stWaitTime;
                targetPoint = new Vector2(curPos.x + Random.Range(-PRange, PRange), curPos.y + Random.Range(-PRange, PRange));
                RaycastHit2D pHit = Physics2D.Raycast(curPos, targetPoint - curPos, PRange);
                while (pHit==true)
                {
                    Debug.Log("!!!!");
                    targetPoint = new Vector2(curPos.x + Random.Range(-PRange, PRange), curPos.y + Random.Range(-PRange, PRange));
                    pHit = Physics2D.Raycast(curPos, targetPoint - curPos, PRange+1f);
                }
                checkPoint++;
            }
            else
            {
                waitTime -= Time.fixedDeltaTime;
                MoveToPoint(targetPoint);
            }
        }

    }
    //====================================================================================


    // ���� � ������� ���
    //====================================================================================
    public IEnumerator Damage(float dmCd, float range)
    {
        isDamaging = true;
        ChangeAnimation("Damage");
        yield return new WaitForSeconds(0.60f);
        if (Vector2.Distance(transform.position, player.transform.position) < range)
        {
            player.GetComponentInChildren<HPEvents>().HPLoss();
        }
        ChangeAnimation("Walk");
        yield return new WaitForSeconds(dmCd);
        isDamaging = false;
    }
    //====================================================================================

    // ��������� ����
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
