using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField]
    private GameObject CMCam;
    [SerializeField]
    private Image hpImage;
    [SerializeField]
    private float playerHp = 100;
    [SerializeField]
    private float playerMaxHp = 100;

    private bool _isDead = false;

    public void HpDecrease(float decrease)
    {
        playerHp -= decrease;
        hpImage.fillAmount = playerHp / playerMaxHp;
        if(playerHp <= 0)
        {
            PlayerDead();
        }
    }

    public void PlayerDead()
    {
        playerHp = 0;
        hpImage.fillAmount = playerHp / playerMaxHp;
        _isDead = true;
        Destroy(gameObject);
        CMCam.SetActive(false);
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
