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
    }

    public void HpRecovery(float recovery)
    {
        playerHp += recovery;
        if (playerHp > playerMaxHp)
            playerHp = playerMaxHp;
        hpImage.fillAmount = playerHp / playerMaxHp;
    }
}
