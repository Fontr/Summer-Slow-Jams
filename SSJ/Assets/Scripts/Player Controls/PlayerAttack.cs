using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Transform[] attackPoints = new Transform[4];
    [SerializeField] private float attackRange = 0.5f, attackCD = 1f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private bool drawAttack = true;
    private Vector2 attackPos;
    private bool attackOnCd = false;

    private new Transform transform;
    void Start()
    {
        transform = GetComponent<Transform>();
        _camera = Camera.main;
    }

    void Attack(int side)
    {
        StartCoroutine(cdAttack());
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoints[side].position, attackRange,enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.name);
            enemy.GetComponent<EnemyHpSystem>().StartCoroutine("TakingDamage");
        }
    }

    //рисовка кругов регистрации атаки
    private void OnDrawGizmosSelected()
    {
        if (!drawAttack) { return; }
        foreach (Transform attackPoint in attackPoints)
        {
            if (attackPoint == null)
            {
                return;
            }
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }



    void Update()
    {
        if (attackOnCd)
        {
            return;
        }
        attackPos = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //проверка стороны атаки
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Mathf.Abs(attackPos.y) < Mathf.Abs(attackPos.x))
            {
                if (attackPos.x > 0)
                {
                    Debug.Log("Удар вправо");
                    Attack(0);
                }
                else
                {
                    Debug.Log("Удар влево");
                    Attack(2);
                }
            }
            else
            {
                if (attackPos.y > 0)
                {
                    Debug.Log("Удар вверх");
                    Attack(1);
                }
                else
                {
                    Debug.Log("Удар вниз");
                    Attack(3);
                }
            }
        }

    }

    IEnumerator cdAttack()
    {
        attackOnCd = true;
        yield return new WaitForSeconds(attackCD);
        attackOnCd = false;
    }

}
