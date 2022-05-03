using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField]
    private Image hpImage;
    [SerializeField]
    private float playerHp = 100;
    [SerializeField]
    private float playerMaxHp = 100;


    public void HpDecrease(float decrease)
    {
        playerHp -= decrease;
        hpImage.fillAmount = playerHp / playerMaxHp;
        if(playerHp <= 0)
        {
            //게임오버씬 or 리스폰부활
            Destroy(gameObject);
        }
    }

    public void HpRecovery(float recovery)
    {
        playerHp += recovery;
        if (playerHp > playerMaxHp)
            playerHp = playerMaxHp;
        hpImage.fillAmount = playerHp / playerMaxHp;
    }

    public void MaxHpUpgrade(float upgrade)
    {
        playerMaxHp += upgrade;
    }
}
