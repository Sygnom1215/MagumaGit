using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeObject : ObjectBase
{
    private bool isCol = false;
    private float originalSpeed = 0f;
    private float changeSpeed = 0f;

    [SerializeField]
    private MovementDataSO movementDataSO = null;

    [SerializeField]
    private float waitTime = 0.5f;



    private void OnEnable()
    {
        Respawn();
    }
    private void Start()
    {
        originalSpeed = movementDataSO._movementData.Speed;
        changeSpeed = originalSpeed / 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isCol = true;
        movementDataSO._movementData.Speed = changeSpeed;
        Damage();
        StartCoroutine(DamageCheck());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCol = false;
        movementDataSO._movementData.Speed = originalSpeed;
        StopCoroutine(DamageCheck());
        Despawn();
        StartCoroutine(RespawnTime());
    }

    private IEnumerator DamageCheck()
    {
        while (isCol)
        {
            yield return new WaitForSeconds(waitTime);
            Damage();
        }
    }
}
