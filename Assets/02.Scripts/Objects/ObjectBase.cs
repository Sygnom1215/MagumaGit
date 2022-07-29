using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    protected bool isRespawn = false;       //가능 여부 
    protected bool isDamage = false;        //가능 여부 
    protected bool isHeal = false;          //가능 여부 
    protected bool respawnCheck = true;    //현재 리스폰이 가능한지 판단  
    protected float respawnTime = 0f;
    protected float getTime = 0f;
    protected float heal = 0f;
    protected float damage = 0f;

    protected PlayerHp playerHp = null;

    private void Awake()
    {
        playerHp = FindObjectOfType<PlayerHp>();
    }

    protected void Damage()
    {
        if (isDamage)
        {
            playerHp.HpDecrease(damage);
        }
    }

    protected void Heal()
    {
        if (isHeal)
        {
            playerHp.HpRecovery(heal);
        }
    }

    protected void Respawn()
    {
        if (isRespawn && respawnCheck)
        {
            gameObject.SetActive(true);
        }
    }

    protected IEnumerator RespawnTime()
    {
        if (isRespawn == true && respawnCheck == false)
        {
            yield return new WaitForSeconds(respawnTime);
            respawnCheck = true;
        }
    }

    protected void Despawn()
    {
        if (!isRespawn)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}