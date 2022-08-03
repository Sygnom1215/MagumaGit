using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodObject : ObjectBase
{
    private void OnEnable()
    {
        Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Heal();
        Despawn();
        StartCoroutine(RespawnTime());
    }
}
