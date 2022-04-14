using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : MonoBehaviour
{
    public PlayerHp playerHp;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            
            playerHp.HpRecovery(10f);
            Destroy(gameObject);
        }
    }
    
}
