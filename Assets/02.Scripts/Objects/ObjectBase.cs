using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    [SerializeField]
    protected ObejctSO objectSO = null;

    protected PlayerHp playerHp = null;

    private Collider2D col = null;
    private SpriteRenderer spriteRenderer = null;


    private void Awake()
    {
        playerHp = FindObjectOfType<PlayerHp>();
    }

    private void Start()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Damage()
    {
        if (objectSO.isDamage)
        {
            playerHp.HpDecrease(objectSO.damage);
        }
    }

    protected void Heal()
    {
        if (objectSO.isHeal)
        {
            playerHp.HpRecovery(objectSO.heal);
        }
    }

    protected void Respawn()
    {
        if (objectSO.isRespawn)
        {
            col.enabled = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    protected IEnumerator RespawnTime()
    {
        if (objectSO.isRespawn == true)
        {
            yield return new WaitForSeconds(objectSO.respawnTime);
            Respawn();
        }
    }

    protected void Despawn()
    {
        if (!objectSO.isRespawn)
        {
            Destroy(gameObject);
        }
        else
        {
            col.enabled = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}