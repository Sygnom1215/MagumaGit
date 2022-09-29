using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPlatform : PlatformBase
{
    public float explosionPower = 20f;
    public float waitTime = 1.5f;
    public float colTime = 0f;
    public bool isDamageOnce = false;
    public Vector2 area = new Vector2(2, 2);
    public Vector2 positionModify = new Vector2(1, 0);
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {

        yield return new WaitForSeconds(waitTime);

        Collider2D[] player = Physics2D.OverlapBoxAll(transform.position + (Vector3)positionModify, area, 0);

        foreach (Collider2D col in player)
        {
            if (col.CompareTag("Player"))
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * explosionPower, ForceMode2D.Impulse);
                Damage();

                isDamageOnce = true;
                yield return new WaitForSeconds(3f);
                isDamageOnce = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + (Vector3)positionModify, area);
    }
}