using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewObject",menuName = "SO/Object")]
public class ObejctSO : ScriptableObject
{
    public bool isRespawn = false;       //���� ���� 
    public bool isDamage = false;        //���� ���� 
    public bool isHeal = false;          //���� ���� 
    public float respawnTime = 0f;
    public float heal = 0f;
    public float damage = 0f;
}
