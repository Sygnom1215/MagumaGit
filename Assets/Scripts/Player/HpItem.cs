using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : MonoBehaviour
{
    public bool isOilBarrier = false;
    public bool isSliding = false;

    private PlayerHp playerHp;
    private Rigidbody2D playerRigid;

    public Queue<SaveHpItem> saveItemQ = new Queue<SaveHpItem>();

    

    private void Start()
    {
        playerHp = GetComponentInParent<PlayerHp>();
        playerRigid = GetComponentInParent<Rigidbody2D>();
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
        else if(col.tag == "Wall" && isSliding == true)
        {
            playerRigid.velocity *= -1f;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "HealZone")
        {
            playerHp.HpRecovery(1f);
        }
        else if(col.tag == "Water" && isOilBarrier == false)
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

    public void SetOilBarrier()
    {
        if (isOilBarrier == true) return;
        isOilBarrier = true;
    }

    public void InWaterSlide()
    {
        isSliding = true;
        playerRigid.gravityScale = 0f;
        playerRigid.velocity = Vector2.zero;
        playerRigid.velocity = -playerRigid.transform.right * 5f;
    }

    public void OutWater()
    {
        isSliding = false;
        isOilBarrier = false;
        playerRigid.gravityScale = 3.5f;
        playerRigid.velocity = Vector2.up * 10f;
    }

}
