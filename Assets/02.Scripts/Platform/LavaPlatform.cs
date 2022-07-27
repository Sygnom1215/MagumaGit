using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPlatform : PlatformBase
{
    public float lastHealTime = 0f;
    public float healWaitTime = 1f;
    private bool isCol = false;

    private void Update()
    {
        if (isCol && lastHealTime + healWaitTime <= Time.time)
        {
            Heal();
            lastHealTime = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCol = true;
        lastHealTime = Time.time;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isCol = false;
    }
}
