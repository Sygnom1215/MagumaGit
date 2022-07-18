using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    private HpItem hpItem;

    private void Awake()
    {
        hpItem = transform.parent.GetComponent<HpItem>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Wall" && hpItem.isSliding == true)
        {
            hpItem.OutWater();
        }

        else if(col.tag == "Water" && hpItem.isOilBarrier == true && hpItem.isSliding == false)
        {
            hpItem.InWaterSlide();
        }
    }
}
