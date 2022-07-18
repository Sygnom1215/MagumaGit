using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;

    // int = NPC의 ID, string은 해당 NPC의 대화
    public Dictionary<int, string[]> talkData = new Dictionary<int, string[]>();

    private void Awake()
    {
        Instance = this;
        GenerateData();
    }

    //대화 설정
    private void GenerateData()
    {
        talkData.Add(1000, new string[] { "Test", "Hello"});
    }

    public string GetTalk(int id, int textIdx)
    {
        if (textIdx == talkData[id].Length)
            return null;
        return talkData[id][textIdx];
    }
}
