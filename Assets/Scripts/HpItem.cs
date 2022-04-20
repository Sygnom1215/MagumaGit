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
        if (col.tag == "Item")
        {
            
            playerHp.HpRecovery(10f);
            Destroy(col.gameObject);
        }
    }
    
}
