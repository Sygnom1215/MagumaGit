using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHpItem : MonoBehaviour
{
    [Range(10f, 50f)]
    [SerializeField]
    private float recovery = 10f;

    public float GetRecoveryValue()
    {
        return recovery;
    }
}
