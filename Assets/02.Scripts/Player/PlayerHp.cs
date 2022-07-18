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
    [SerializeField]
    private Transform respawnPos;
    private Rigidbody2D rigid;
    [SerializeField]
    private MovementDataSO movementDataSO;
    private bool _isDead = false;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
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
        gameObject.SetActive(false);
        CMCam.SetActive(false);

        Invoke("Respawn", 3f);
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

    public void Respawn()
    {
        //playerHp = playerMaxHp;
        //rigid.gravityScale = movementDataSO._movementData.Gravity;
        //hpImage.fillAmount = playerHp / playerMaxHp;
        //transform.position = respawnPos.position;
        //gameObject.SetActive(true);
        //CMCam.SetActive(true);
        UIManager.Instance.Restart();
    }
}
