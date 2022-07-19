using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPlatform : PlatformBase
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        //collision.gameObject.GetComponent<Rigidbody2D>().
    }
}
