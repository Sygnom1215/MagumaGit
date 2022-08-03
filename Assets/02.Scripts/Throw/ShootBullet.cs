using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField]
    private float lunchForce;
    [SerializeField]
    private GameObject bullet;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        GameObject bulletIns = Instantiate(bullet, transform.position,transform.rotation);
        bulletIns.GetComponent<Rigidbody2D>().velocity = transform.right * lunchForce;
    }
}
