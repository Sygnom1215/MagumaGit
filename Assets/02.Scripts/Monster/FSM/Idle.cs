using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Idle : MonoBehaviour
{
    //�÷��̾� ��ġ ��ȯ
    public abstract Vector2 SearchPlayer(Monster owner, float findRange);
}
