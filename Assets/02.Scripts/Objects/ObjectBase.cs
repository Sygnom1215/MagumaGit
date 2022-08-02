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
        if (objectSO.isRespawn && objectSO.respawnCheck)
        {
            gameObject.SetActive(true);
        }
    }

    protected IEnumerator RespawnTime()
    {
        if (objectSO.isRespawn == true && objectSO.respawnCheck == false)
        {
            yield return new WaitForSeconds(objectSO.respawnTime);
            objectSO.respawnCheck = true;
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
            gameObject.SetActive(false);
        }
    }
}