using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Loading/SceneName")]
public class SceneNameDataSO : ScriptableObject
{
    public List<string> SceneName = new List<string>();
}
