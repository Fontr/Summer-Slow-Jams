using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float cdShoot = 1.0f;
    [SerializeField] bool isShooting = false;
    void Update()
    {
        if (isShooting)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            StartCoroutine("ShootingCD");
        }
    }

    IEnumerator ShootingCD()
    {
        isShooting = true;
        yield return new WaitForSeconds(cdShoot);
        isShooting = false;
    }

}
