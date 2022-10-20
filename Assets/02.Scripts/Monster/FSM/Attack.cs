using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : State<MonsterFSM>
{
    //attack 우선도 계산 함수, 각각 어택을 직접적으로 하는 함수
    public abstract int CalculatePriority(MonsterSO owner);
    public abstract void DoAttack(MonsterSO owner);
}
