using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform[] attackPoints = new Transform[4];
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private bool drawAttack = true;
    private Vector2 attackPos;

    private new Transform transform;
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Attack(int side)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoints[side].position, attackRange,enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.name);
        }
    }

    //������� ������ ����������� �����
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
        //��������� ������� ������������ ������
        attackPos = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //�������� ������� �����
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Mathf.Abs(attackPos.y) < Mathf.Abs(attackPos.x))
            {
                if (attackPos.x > 0)
                {
                    Debug.Log("���� ������");
                    Attack(0);
                }
                else
                {
                    Debug.Log("���� �����");
                    Attack(2);
                }
            }
            else
            {
                if (attackPos.y > 0)
                {
                    Debug.Log("���� �����");
                    Attack(1);
                }
                else
                {
                    Debug.Log("���� ����");
                    Attack(3);
                }
            }
        }

    }

}
