using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters.Utill;

[CreateAssetMenu(menuName = "SO/Monster/Monster", fileName = "Monster_")]
public class MonsterSO : ScriptableObject
{
    [SerializeField]
    private int id;
    [SerializeField]
    private Attitude attitude;

    [Header("Actions")]
    [SerializeField]
    private Attack[] attackActions;
    [SerializeField]
    private Heal healAction;
    [SerializeField]
    private Parring parringAction;
    [SerializeField]
    private Idle idleAction;
    [Header("Atc")]
    [SerializeField]
    private float noticeRange = 0f;

    private Monsters.Utill.MonsterState currentState;
    public Monsters.Utill.MonsterState CurrentState => currentState;

    public int Id => id;
    public Attitude Attitude => attitude;

    public Attack[] GetAttackActions => attackActions;
    public Heal GetHealAction => healAction;
    public Parring GetParringAction => parringAction;
    public Idle GetIdleAction => idleAction;

    public float NoticeRange { get => noticeRange; set { noticeRange = value; } }
}
