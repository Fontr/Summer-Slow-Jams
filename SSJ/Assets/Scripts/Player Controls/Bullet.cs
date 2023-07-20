using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeDestroy = 3f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ-90f);

        rb.velocity = transform.up * speed;
        Invoke("DestroyBullet", timeDestroy);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        /*if (collision.tag == "Enemy")
        {
            
        }*/
        if (collision.tag != "Player" && collision.tag != "zPos")
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}