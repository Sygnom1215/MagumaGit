using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //[SerializeField]
    //private Transform startAttackPos;
    [SerializeField]
    private Transform attackPos;
    [SerializeField]
    private float attackLength;
    [SerializeField]
    [Range(0f,5f)]
    private float rad;
    [SerializeField]
    private float coolTime;
    [SerializeField]
    LayerMask attackLayerMask;
    private static float currentTime;
    private void Start()
    {
        currentTime = coolTime;
    }
    private void Update()
    {
        DownCoolTime();
    }

    private void DownCoolTime()
    {   
        currentTime -= Time.deltaTime;
    }
    private bool IsCoolTime()
    {
        if (currentTime <= 0)
        {
            currentTime = coolTime;
            return true;
        }
        else
        {
            return false;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackLength);
    }
    public void Attack()
    {
        if (!IsCoolTime()) return;
        //Ä¸½¶ ÇüÅÂ·Î °ø°Ý.
        //Collider2D[] hitColliders = Physics2D.OverlapCapsuleAll(endAttackPos.position, startAttackPos.position, CapsuleDirection2D.Horizontal, rad);
        //Debug.DrawLine(startAttackPos.position, endAttackPos.position);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPos.position, attackLength,attackLayerMask);
        foreach(Collider2D collider in hitColliders)
        {
            Debug.Log(collider.tag);
        }
    }
}
