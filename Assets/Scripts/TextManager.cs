using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using DG.Tweening;
//using DG.Tweening;


public class TextManager : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1mVDkuXvAvh9-9Q1k9dfrERdCST6E9InZQfXSewmBmH8/export?format=tsv";

    private Text _textPanel;
    public GameObject textPanelObj; //텍스트 패널 오브젝트

    [SerializeField] private Transform selectPanel;

    [SerializeField] private GameObject _selectButton;
    public GameObject[] background; // 게임 배경화면 배열
    public GameObject[] imageList;    // 추가로 띄워둘 이미지 

    Dictionary<int, string[,]> Sentence = new Dictionary<int, string[,]>();
    Dictionary<int, int> max = new Dictionary<int, int>();
    List<string> select = new List<string>();

    public int chatID = 1, lineNumber = 1, backgroundID = 1, slotID = 0; // ID 기본 값 설정
    private bool _isSkip = false, _isTyping = false;

    private static TextManager instance;

    public UnityEvent TextTyping;

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

    private void Awake()
    {
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

    private void Start()
    {
        StartCoroutine(StartTyping());
    }

    public IEnumerator StartTyping()
    {
        yield return new WaitForSeconds(2f);
        TextTyping.Invoke();
    }

    /// <summary>
    /// 대사 타이핑
    /// </summary>
    public void Typing()
    {
        if (Sentence[chatID] == null) //만약 대사를 못 불러왔다면
        {
            StartCoroutine(LoadTextData()); //다시 불러오기
        }

        StartCoroutine(TypingCoroutine()); //타이핑 코루틴 실행
    }

    /// <summary>
    /// 대사 타이핑 코루틴 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator TypingCoroutine()
    {
        textPanelObj.SetActive(true);


        string pName = Sentence[chatID][lineNumber, (int)IDType.CharacterName];
        string storyText = Sentence[chatID][lineNumber, (int)IDType.Text];

        //if (Sentence[chatID][lineNumber, (int)IDType.Direct] == null || Sentence[chatID][lineNumber, (int)IDType.Direct] == "") {

        if (Sentence[chatID][lineNumber, (int)IDType.Direct] == "" || Sentence[chatID][lineNumber, (int)IDType.Direct] == null)
        {
            for (int i = 0; i < storyText.Length + 1; i++)
            {

                if (_isSkip)
                {
                    _textPanel.text = string.Format("{0}\n{1}", pName, storyText);           // 텍스트 넘김.....누르면 한줄이 한번에 딱
                    _isSkip = false;
                    break;
                }


                _textPanel.text = string.Format("{0}\n{1}", pName, storyText.Substring(0, i));
                //soundManager.TypingSound(); // 텍스트 출력....따따따따
                yield return new WaitForSeconds(0.2f);

                _textPanel.text = string.Format("{0}\n{1}", pName, storyText);
            }
        }

        else
        {
            _textPanel.text = string.Format("");
        }
     
        ///↓↓↓ 타이핑 끝난 후 실행
        _isTyping = false;

    }

    //else
    //{
    //    SkipText();
    //}

    public void SkipTextClick() // 텍스트 패널 클릭 시 스킵
    {
        if (!_isTyping) SkipText();
        else _isSkip = true;
    }

    public void SkipText() // 텍스트 진행
    {
        if (backgroundID >= 1) background[backgroundID].gameObject.SetActive(true);

        if (Sentence[chatID][lineNumber, (int)IDType.ImageID] != "" && Sentence[chatID][lineNumber, (int)IDType.ImageID] != null)
            ImageSetActive(false);

        if (Sentence[chatID][lineNumber + 1, (int)IDType.Direct] != "" && Sentence[chatID][lineNumber + 1, (int)IDType.Direct] != null)
        {
            string directName = Sentence[chatID][lineNumber + 1, (int)IDType.Direct];
            StartCoroutine(directName.Trim());
        }

        if (Sentence[chatID][lineNumber, (int)IDType.SFX] != "" && Sentence[chatID][lineNumber, (int)IDType.SFX] != null)
        {
            string clipName = Sentence[chatID][lineNumber, (int)IDType.SFX].Trim();
        }

        if (Sentence[chatID][lineNumber, (int)IDType.Event] != "" && Sentence[chatID][lineNumber, (int)IDType.Event] != null)
        {
            string eventName = Sentence[chatID][lineNumber, (int)IDType.Event];
            if (eventName == "함수")
            {
                string funcName = Sentence[chatID][lineNumber, (int)IDType.Event + 1];
                StartCoroutine(funcName.Trim());
            }
            else if (eventName == "이동")
            {
                int num = UnityEngine.Random.Range((int)IDType.Event + 1, Convert.ToInt32(Sentence[chatID][lineNumber, 19]));
                chatID = Convert.ToInt32(Sentence[chatID][lineNumber, num]);
                lineNumber = 0;
            }

            //if (eventName == "선택")
            //{
            //    if (Sentence[chatID][lineNumber, (int)IDType.BackgroundID] != "")
            //    {
            //        backgroundID = Convert.ToInt32(Sentence[chatID][lineNumber, (int)IDType.BackgroundID]) - 1;
            //        background[backgroundID].SetActive(true);
            //    }
            //    textPanelObj.SetActive(false);
            //    SelectOpen();
            //    return;
            //}
        }

        lineNumber++;
        if (lineNumber != max[chatID])
        {
            TextTyping?.Invoke();
        }
        else textPanelObj.SetActive(false);
    }

    private void ImageSetActive(bool set)
    {
        string[] imageNumList = Sentence[chatID][lineNumber, (int)IDType.ImageID].Split(',');
        foreach (string x in imageNumList)
        {
            int num = Convert.ToInt32(x) - 1;

            this.imageList[num].gameObject.SetActive(set);
        }
    }
}