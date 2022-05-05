using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : MonoBehaviour
{
    private PlayerHp playerHp;

    private void Start()
    {
        playerHp = GetComponentInParent<PlayerHp>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Deadzone")
        {
            playerHp.PlayerDead();
        }
        else if (col.tag == "Item")
        {

            playerHp.HpRecovery(10f);
            Destroy(col.gameObject);
        }
        else if (col.tag == "Trickle")
        {

            playerHp.HpDecrease(10f);
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "HealZone")
        {
            playerHp.HpRecovery(1f);
        }
        else if(col.tag == "Water")
        {
            playerHp.HpDecrease(0.01f);
        }
    }

}
