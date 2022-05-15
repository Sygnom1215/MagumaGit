using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
//using DG.Tweening;


public class TextManager : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1mVDkuXvAvh9-9Q1k9dfrERdCST6E9InZQfXSewmBmH8/export?format=tsv";

    private Text _textPanel;
    public GameObject textPanelObj;

    [SerializeField] private Transform selectPanel;

    [SerializeField] private GameObject _selectButton;
    public GameObject[] background; // 게임 배경화면 배열
    public GameObject[] image;    // 추가로 띄워둘 이미지 
    [SerializeField] private GameObject _endAnimationObj; // 텍스트 끝나면 텍스트 패널 오른쪽 아래에서 뛰옹뛰옹 뱅글뱅글 하는 애
    [SerializeField] private float shakeTime = 0.13f;
    [SerializeField] private float shakestr = 0;
    [SerializeField] private GameObject textlogPrefab;
    [SerializeField] private Transform textlogView;
    //[SerializeField] private Text autoChecker;

    Dictionary<int, string[,]> Sentence = new Dictionary<int, string[,]>();
    Dictionary<int, int> max = new Dictionary<int, int>();
    List<string> select = new List<string>();

    public int chatID = 1, typingID = 1, backgroundID = 1, slotID = 0; // ID 기본 값 설정
    //private bool isTyping = false, skip = false; // 텍스트 스킵 불 기본 값 설정
    //public float chatSpeed = 0.1f, autoSpeed = 1f; // 텍스트 나오는 속도와 Auto 속도 기본 값 설정
    //public bool isAuto = false;

    private static TextManager instance;

    public Action<GameObject> OnEffectObject;
    public UnityEvent TextTyping;
    public UnityEvent OnTextTypingEnd;

    enum IDType
    {
        ChatID = 0,
        CharacterName,
        Text,
        BackgroundID,
        ImageID,
        Direct,
        SFX,
        BGM,
        Event
    }

    public static TextManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TextManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("TextManager");
                    instance = container.AddComponent<TextManager>();
                }
            }
            return instance;
        }
    }

    //private void Update()
    //{
    //    if (isAuto == true)
    //    {
    //        autoChecker.text = "<color=red>자동진행</color>";
    //    }
    //    else
    //    {
    //        autoChecker.text = "자동진행";
    //    }
    //}

    private void Awake()
    {
        //_textDataSO = GetComponentInChildren<TextDataSave>();
        StartCoroutine(LoadTextData()); // 텍스트 데이터 읽기
        _textPanel = textPanelObj.transform.Find("text").GetComponent<Text>();
    }

    public IEnumerator LoadTextData()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text, vertualText;
        string[] line = data.Split('\n'), vText;
        int lineSize = line.Length;
        int rawSize;
        int chatID = 1, lineCount = 1, i, j;

        for (i = 1; i < lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            rawSize = line[i].Split('\t').Length;
            if (row[0] != "")
            {
                lineCount = 1;
                chatID = Convert.ToInt32(row[0]);
                max[chatID] = 1;
                Sentence[chatID] = new string[lineSize, 20];
            }

            for (j = 1; j < rawSize; j++)
            {
                Sentence[chatID][lineCount, j] = row[j];
                if (j > 4)
                {
                    if (row[j] == "") break;
                }
            }
            Sentence[chatID][lineCount, 19] = j.ToString();

            vertualText = Sentence[chatID][lineCount, 2];

            if (vertualText != null)
            {
                foreach (char N in vertualText)
                {
                    if (N == 'N')
                    {
                        vText = vertualText.Split('N');
                        if (vText[1] != null)
                        {
                            Sentence[chatID][lineCount, 2] = $"{vText[0]}{GameManager.Instance.playerName}{vText[1]}";
                        }
                        else
                        {
                            Sentence[chatID][lineCount, 2] = $"{GameManager.Instance.playerName}{vText[0]}";
                        }
                        break;
                    }
                }
            }


            Sentence[chatID][lineCount, ++j] = "x";
            max[chatID]++;
            lineCount++;
        }
    }
}