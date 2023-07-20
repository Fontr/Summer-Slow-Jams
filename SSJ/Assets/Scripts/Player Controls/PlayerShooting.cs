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
        if (Input.GetKeyDown(KeyCode.F))
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
