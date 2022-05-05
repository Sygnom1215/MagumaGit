using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Transform startAttackPos;
    [SerializeField]
    private float attackLength;
    [SerializeField]
    [Range(0f,5f)]
    private float rad;
    private void Update()
    {
        Attack();
    }
    public void Attack()
    {
        Vector3 endAttackPos = new Vector3(startAttackPos.position.x + attackLength, startAttackPos.position.y, startAttackPos.position.z);
        Physics2D.OverlapCapsule(endAttackPos, startAttackPos.position, CapsuleDirection2D.Horizontal,rad);
        Debug.DrawLine(startAttackPos.position, endAttackPos);


    }
    public void DefaultAttack()
    {

    }
}
