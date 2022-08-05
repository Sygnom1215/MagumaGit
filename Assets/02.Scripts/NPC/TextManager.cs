using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;

    // int = NPC�� ID, string�� �ش� NPC�� ��ȭ
    public Dictionary<int, string[]> talkData = new Dictionary<int, string[]>();

    private void Awake()
    {
        Instance = this;
        GenerateData();
    }

    //��ȭ ����
    private void GenerateData()
    {
        //�ڵ�󿡼� ������� ��Ÿ���� ���� ����� ��...

        //npc ������
        talkData.Add(1, new string[] { "�ű� ���, ���� ������\n���� ������ �ƴ���?",
                                        "���� ����\n�� ���� �������ݾ�",
                                        "�׷� ���� ������\n�����ؾ��� ����",
                                        "��, SPACEBAR�� ������\n������ �� �־� ����",
                                        "�������ʰ� �����ؼ�\n����� ����" });
    }

    public string GetTalk(int id, int textIdx)
    {
        if (textIdx == talkData[id].Length)
            return null;
        return talkData[id][textIdx];
    }
}
