using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask layerMask;
    public Vector2 attackRange = new Vector2(3f, 2f);

    private Animator animator = null;
    private Vector2 attackPos = Vector2.zero;

    [SerializeField]
    private MovementDataSO movement = null;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShortAttack()
    {
        attackPos = transform.position;
        attackPos.x += movement._movementData.PlayerDir;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPos, attackRange, 45f, layerMask);
        foreach (var item in colliders)
        {
            //TODO:몬스터 완성되면 추가할것
            //item.gameObject.GetComponent<>()
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos, attackRange);
    }
}