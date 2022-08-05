using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField]
    private float lunchForce;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform PointsParentTransform;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PointsParentTransform.gameObject.SetActive(true);
            Time.timeScale=0.1f;
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Shoot();
            PointsParentTransform.gameObject.SetActive(false);
            Time.timeScale=1;
        }
    }
    private void Shoot()
    {
        var bulletIns = DefaultAttack.GetObject();
        //GameObject bulletIns = Instantiate(bullet, transform.position, transform.rotation);
        bulletIns.transform.position = gameObject.transform.position;
        bulletIns.transform.localScale=Vector3.one;
        bulletIns.GetComponent<Rigidbody2D>().velocity = transform.right * lunchForce;
    }
}
