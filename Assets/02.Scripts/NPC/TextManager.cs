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
        //npc 개구리
        talkData.Add(1, new string[] { "거기 잠깐, 설마 물에\n빠질 생각은 아니지?", "SPACEBAR를 누르면\n점프할 수 있어 개골", "그리고 연못을 조심해라 개골"});
    }

    public string GetTalk(int id, int textIdx)
    {
        if (textIdx == talkData[id].Length)
            return null;
        return talkData[id][textIdx];
    }
}
