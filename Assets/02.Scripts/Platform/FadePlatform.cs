using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FadePlatform : PlatformBase
{
    [SerializeField]
    private float respawnTime = 5f;
    [SerializeField]
    private float fadeAlpha = 1;
    [SerializeField]
    private bool respawn = false;
    private SpriteRenderer spriteRenderer = null;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        SetRespawn();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            spriteRenderer.DOFade(0, 2);
            Invoke("ColliderEnabled", 2f);
        }
        if (respawn)
        {
            Invoke("ColliderEnabled", respawnTime);
            Invoke("SetRespawn", respawnTime);
        }
    }

    private void ColliderEnabled()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = !col.enabled;
    }

    void SetRespawn()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}