using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField]
    private Image hpImage;
    [SerializeField]
    private int playerHp = 100;
    [SerializeField]
    private int playerMaxHp = 100;


    public void HpDecrease(int decrease)
    {
        playerHp -= decrease;
        hpImage.fillAmount = playerHp / playerMaxHp;
    }
}
