using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPlatform : PlatformBase
{
    public float explosionPower = 0.5f;
    public float waitTime = 1.5f;
    public float colTime = 0f;
    public bool isDamageOnce = false;
    public Vector2 area = new Vector2(5, 5);
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {

        yield return new WaitForSeconds(waitTime);

        GameObject player = Physics2D.OverlapBox(transform.position, area, 0).gameObject;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, area);
        Gizmos.DrawSphere(transform.position, 5);
        if (player != null)
        {
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * explosionPower, ForceMode2D.Impulse);
            Damage();

            isDamageOnce = true;
            yield return new WaitForSeconds(3f);
            isDamageOnce = false;
        }
    }
}
