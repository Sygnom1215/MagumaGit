using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            //change pool bullet
            Destroy(col.gameObject);
            transform.parent.gameObject.SetActive(false);
            //Destroy(transform.parent.gameObject);
        }
    }
}
