using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State<MonsterFSM>
{
    public override void OnUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    //플레이어 위치 반환
    public Vector2 SearchPlayer(MonsterSO owner, float findRange)
    {

    }
}
