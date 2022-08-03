using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondPlatform : PlatformBase
{
    [SerializeField]
    private float delayTime = 0.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("Die", delayTime);
    }
}
