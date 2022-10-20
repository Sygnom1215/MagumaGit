using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterFSM : MonoBehaviour
{
    private StateMachine<MonsterFSM> fsmManager;
    public StateMachine<MonsterFSM> FsmManager { get => fsmManager; set { fsmManager = value; } }

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private Transform[] posTargets;
    [SerializeField]
    private Transform posTarget = null; //현 로밍 위치
    [SerializeField]
    private int posTargetIdx = 0;

    [SerializeField]
    private MonsterSO monsterSO;

    public MonsterSO GetMonsterList => monsterSO;

    private void Start()
    {
        fsmManager = new StateMachine<MonsterFSM>(this, monsterSO.GetIdleAction);

        fsmManager.AddStateList(monsterSO.GetHealAction);
        foreach (var attack in monsterSO.GetAttackActions)
            fsmManager.AddStateList(attack);
        fsmManager.AddStateList(monsterSO.GetHealAction);
        fsmManager.AddStateList(monsterSO.GetParringAction);

    }
    private void Update()
    {
        fsmManager.OnUpdate(Time.deltaTime);
    }
    public void OnHitEvent()
    {
        Debug.Log("OnHitEvent");
        fsmManager.OnHitEvent();
    }
    public Transform SearchEnemy()
    {
        target = null;
        Collider2D[] seekCol = Physics2D.OverlapCircleAll(transform.position, monsterSO.NoticeRange);

        for (int i = 0; i < seekCol.Length; i++)
        {
            if (seekCol[i].CompareTag("Player"))
            {
                Vector2 dir = seekCol[i].transform.position - transform.position.normalized;
                float dist = Vector2.Distance(transform.position, seekCol[i].transform.position);
                RaycastHit2D hit= Physics2D.Raycast(transform.position, dir, monsterSO.NoticeRange);
                if (hit.collider.transform.CompareTag("Player"))
                {
                    target = hit.collider.transform;
                }
            }
        }
        return target;
    }

    public Transform SearchNextTargetPositon()
    {
        posTarget = null;
        if (posTargets.Length > 0 && posTargets.Length > posTargetIdx)
            posTarget = posTargets[posTargetIdx];

        posTargetIdx = (posTargetIdx + 1) % posTargets.Length;

        return posTarget;
    }
}
