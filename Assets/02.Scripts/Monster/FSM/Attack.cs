using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    //attack �켱�� ��� �Լ�, ���� ������ ���������� �ϴ� �Լ�
    public abstract int CalculatePriority(Monster owner);
    public abstract void DoAttack(Monster owner);
}
