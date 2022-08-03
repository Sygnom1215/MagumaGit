using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    [SerializeField]
    protected ObejctSO objectSO = null;

    protected PlayerHp playerHp = null;

    private void Awake()
    {
        playerHp = FindObjectOfType<PlayerHp>();
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
            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
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
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }
    }
}