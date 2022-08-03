using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBase : MonoBehaviour
{
    [SerializeField]
    protected Platform platformSO = null;
    private PlayerHp playerHp;

    void Start()
    {
        playerHp = FindObjectOfType<PlayerHp>();
    }

    protected void Damage()
    {
        if (platformSO.isDamage == true)
        {
            playerHp.HpDecrease(platformSO.damage);
        }
    }

    protected void Heal()
    {
        if (platformSO.isHeal == true)
        {
            playerHp.HpRecovery(platformSO.healAmount);
        }
    }

    protected void Die()
    {
        if (platformSO.isDie == true)
        {
            playerHp.PlayerDead();
        }
    }
}