using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters.Utill;

[CreateAssetMenu(menuName = "Monster/Monster", fileName = "Monster_")]
public class Monster : ScriptableObject
{
    [SerializeField]
    private int id;
    [SerializeField]
    private Type type;
    [SerializeField]
    private Attitude attitude;

    //애니메이션 컨트롤 할 거 적어놓기
    [SerializeField]
    private FSM monsterFSM;

    [Header("Actions")]
    [SerializeField]
    private Attack[] attackActions;
    [SerializeField]
    private Heal healAction;
    [SerializeField]
    private Parring parringAction;
    [SerializeField]
    private Idle idleAction;

    private Monsters.Utill.MonsterState currentState;

    public Monsters.Utill.MonsterState CurrentState => currentState;
}
