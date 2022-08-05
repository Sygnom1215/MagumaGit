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
        //코드상에서 어떤식으로 나타내야 보기 쉬울까 흠...

        //npc 개구리
        talkData.Add(1, new string[] { "거기 잠깐, 설마 연못에\n빠질 생각은 아니지?",
                                        "가만 보니\n넌 불의 정령이잖아",
                                        "그럼 더욱 연못을\n조심해야지 개골",
                                        "자, SPACEBAR를 누르면\n점프할 수 있어 개골",
                                        "빠지지않게 조심해서\n가라고 개골" });
    }

    public string GetTalk(int id, int textIdx)
    {
        if (textIdx == talkData[id].Length)
            return null;
        return talkData[id][textIdx];
    }
}
