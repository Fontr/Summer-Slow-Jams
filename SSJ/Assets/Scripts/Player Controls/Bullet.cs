using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeDestroy = 3f;
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Transform transformGun;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transformGun = GameObject.Find("GunPoint").GetComponent<Transform>();
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transformGun.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ-90f);

        rb.velocity = transform.up * speed;
        Invoke("DestroyBullet", timeDestroy);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            collision.GetComponentInParent<EnemyHpSystem>().StartCoroutine("TakingDamage");
        }
        if (collision.tag != "Player" && collision.tag != "zPos" && collision.tag != "DialogueTrigger")
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}