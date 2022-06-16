using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : MonoBehaviour
{
    private PlayerHp playerHp;

    public Queue<SaveHpItem> saveItemQ = new Queue<SaveHpItem>();

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
            col.gameObject.SetActive(false);
        }
        else if (col.tag == "Trickle")
        {

            playerHp.HpDecrease(10f);
            col.gameObject.SetActive(false);
        }
        else if(col.tag == "SaveItem")
        {
            saveItemQ.Enqueue(col.GetComponent<SaveHpItem>());
            col.gameObject.SetActive(false);
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
            playerHp.HpDecrease(1f);
        }
    }

    public void UseHpItem()
    {
        if (saveItemQ.Count <= 0) return;
        float value = saveItemQ.Dequeue().GetRecoveryValue();
        playerHp.HpRecovery(value);
    }

}
