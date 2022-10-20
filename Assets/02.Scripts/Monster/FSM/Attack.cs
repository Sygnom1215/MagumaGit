using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : State<MonsterFSM>
{
    //attack �켱�� ��� �Լ�, ���� ������ ���������� �ϴ� �Լ�
    public abstract int CalculatePriority(MonsterSO owner);
    public abstract void DoAttack(MonsterSO owner);
}
