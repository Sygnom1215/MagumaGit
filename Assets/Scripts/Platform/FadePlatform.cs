using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePlatform : PlatformBase
{
    [SerializeField]
    private float fadeAlpha = 1;
    [SerializeField]
    private bool respawn = false;
    private SpriteRenderer spriteRenderer = null;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        //base.OnCollisionEnter2D(collision);
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("COl");
            StartCoroutine("Fade");
        }
    }

    private void ColliderEnabled()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col.enabled == true)
        {
            col.enabled = false;
            return;
        }
        col.enabled = true;
    }

    /// <summary>
    /// 발판을 투명하게 합니다 
    /// </summary>
    /// <returns></returns>
    IEnumerator Fade()
    {
        // for문 돌아가는 횟수랑 fadeAlpha 줄어드는 값/yeild return 값 수정해서 조금 더 부드럽게 움직이게 할 수 있음 
        for (int i = 0; i < 10; i++)
        {
            fadeAlpha -= 0.1f;
            spriteRenderer.color = new Color(1f, 1f, 1f, fadeAlpha);
            yield return new WaitForSeconds(0.1f);
        }
        if (fadeAlpha <= 0)
        {
            ColliderEnabled();
            if (respawn == true)
            {
                yield return new WaitForSeconds(4f);
                ColliderEnabled();
                SetRespawn();
            }
            StopCoroutine("Fade");
        }
    }
    void SetRespawn()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        fadeAlpha = 1f;
    }
}