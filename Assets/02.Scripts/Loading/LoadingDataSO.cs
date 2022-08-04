using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Loading/LoadingData")]
public class LoadingDataSO : ScriptableObject
{
    public List<Vector2> startPos = new List<Vector2>();
}
