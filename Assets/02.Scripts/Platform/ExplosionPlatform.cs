using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPlatform : PlatformBase
{
    public float explosionPower = 0.5f;
    public float waitTime = 1.5f;
    public float colTime = 0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        colTime = Time.time;
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (colTime + waitTime <= Time.time)
        {
            Debug.Log("call");
            GameObject player = collision.gameObject;
            Vector3 dir = player.transform.position - gameObject.transform.position;
            dir = dir.normalized;
            Explosion(player, dir);
            Damage();
        }
    }

    private void Explosion(GameObject player, Vector3 dir)
    {
        player.GetComponent<Rigidbody2D>().AddForce(dir * explosionPower, ForceMode2D.Impulse);
    }
}
