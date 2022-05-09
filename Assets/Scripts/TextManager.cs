using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
//using DG.Tweening;


public class TextManager : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1mVDkuXvAvh9-9Q1k9dfrERdCST6E9InZQfXSewmBmH8/export?format=tsv";

    [SerializeField] public GameObject textImage;

    /// <summary>
    /// 텍스트가 나오는 패널
    /// </summary>
    [SerializeField] private Text textPanel;

    /// <summary>
    /// 선택 이벤트 시 활성화 되는 패널
    /// </summary>
    [SerializeField] private Transform selectPanel;

    [SerializeField] private GameObject selectButton;
    [SerializeField] public GameObject[] background; // 게임 배경화면 배열
    [SerializeField] private GameObject[] image;    // 추가로 띄워둘 이미지 
    [SerializeField] private GameObject endObject; // 텍스트 끝나면 텍스트 패널 오른쪽 아래에서 뛰옹뛰옹 뱅글뱅글 하는 애
    [SerializeField] private float shakeTime = 0.13f;
    [SerializeField] private float shakestr = 0;
    [SerializeField] private GameObject textlogPrefab;
    [SerializeField] private Transform textlogView;
    [SerializeField] private GameObject textlogScroll; // 캐릭터 이펙트 스크립트
    [SerializeField] private Text autoChecker;

    Dictionary<int, string[,]> Sentence = new Dictionary<int, string[,]>();
    Dictionary<int, int> max = new Dictionary<int, int>();
    List<string> select = new List<string>();

    public int chatID = 1, typingID = 1, backID = 1, slotID = 0; // ID 기본 값 설정
    private bool isTyping = false, skip = false; // 텍스트 스킵 불 기본 값 설정
    string[] imageList;
    public float chatSpeed = 0.1f, autoSpeed = 1f; // 텍스트 나오는 속도와 Auto 속도 기본 값 설정
    public bool Auto = false;
    private float originalChatSpeed; // 원래 텍스트 속도 기억하는 변수
    private GameObject effectObject; //이미지의 인덱스 값

    //[SerializeField] private SoundManager soundManager = null; // 사운드 매니저 스크립트 넣기
    //public SoundManager SOUND { get { return soundManager; } }

    private static TextManager instance;

    public Action<GameObject> OnEffectObject;

    //public delegate void EffectObject(GameObject g);
    //public event EffectObject OnEffectObject;

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

    private void Update()
    {
        if (Auto == true)
        {
            autoChecker.text = "<color=red>자동진행</color>";
        }
        else
        {
            autoChecker.text = "자동진행";
        }
    }

    private void Awake()
    {
        //soundManager = GetComponent<SoundManager>(); // 사운드 매니저 스크립트 대입
        StartCoroutine(LoadTextData()); // 텍스트 데이터 읽기
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

            //if (vertualText != null)
            //{
            //    foreach (char N in vertualText)
            //    {
            //        if (N == 'N')
            //        {
            //            vText = vertualText.Split('N');
            //            if (vText[1] != null)
            //            {
            //                Sentence[chatID][lineCount, 2] = string.Format("{0}{1}{2}", vText[0], GameManager.Instance.PlayerName, vText[1]);
            //            }
            //            else
            //            {
            //                Sentence[chatID][lineCount, 2] = string.Format("{0}{1}", GameManager.Instance.PlayerName, vText[0]);
            //            }
            //            break;
            //        }
            //    }
            //}


            Sentence[chatID][lineCount, ++j] = "x";
            max[chatID]++;
            lineCount++;
        }
    }
    //bool isBracketOpen;
    //bool checkText;
    //int openBracketIndex;
    //int closeBracketIndex;
    //int bracketCount = 0;
    public IEnumerator Typing()
    {
        if (Sentence[chatID] == null)
        {
            LoadTextData();
        }

        textImage.SetActive(true);
        if (!Auto) endObject.SetActive(false);
        isTyping = true;
        if (Sentence[chatID][typingID, 3] != "")
        {
            backID = Convert.ToInt32(Sentence[chatID][typingID, 3]) - 1;
            background[backID].SetActive(true);
            //StartCoroutine(GameManager.Instance.FadeIn());
            //StartCoroutine(GameManager.Instance.FadeOut());
        }
        if (Sentence[chatID][typingID, 4] != "") imageSetactive(true);
        string Name = Sentence[chatID][typingID, 1];
        if (Name == "당신") Name = GameManager.Instance.PlayerName;
        for (int i = 0; i < Sentence[chatID][typingID, 2].Length + 1; i++)
        {
            if (skip)
            {
                textPanel.text = string.Format("{0}\n{1}", Name, Sentence[chatID][typingID, 2]);           // 텍스트 넘김.....누르면 한줄이 한번에 딱
                skip = false;
                break;
            }


            //if(isBracketOpen)
            //{
            //    if (Sentence[chatID][typingID, 2][i] == '>')
            //    {
            //        checkText = true;
            //        isBracketOpen = false;
            //    }
            //    textPanel.text = string.Format("{0}\n{1}", Name, Sentence[chatID][typingID, 2].Substring(0, i));
            //    continue;
            //}
            //if (Sentence[chatID][typingID, 2][i] == '<') isBracketOpen = true;


            textPanel.text = string.Format("{0}\n{1}", Name, Sentence[chatID][typingID, 2].Substring(0, i));
            //soundManager.TypingSound(); // 텍스트 출력....따따따따
            yield return new WaitForSeconds(chatSpeed);
        }
        ///↓↓↓ 타이핑 끝난 후 실행
        if (!Auto) endObject.SetActive(true);
        isTyping = false;

        OnEffectObject?.Invoke(effectObject);

        //if(OnEffectObject!=null)

        GameObject tl = Instantiate(textlogPrefab, textlogView);
        if (Name == "") tl.GetComponent<Text>().text = string.Format(Sentence[chatID][typingID, 2]);
        else tl.GetComponent<Text>().text = string.Format("{0}: {1}", Name, Sentence[chatID][typingID, 2]);
        GameObject t2 = Instantiate(textlogPrefab, textlogView);

        if (Auto)
        {
            yield return new WaitForSeconds(autoSpeed);
            SkipText();
        }
    }

    public void SkipTextClick() // 텍스트 패널 클릭 시 스킵
    {
        if (!Auto)
        {
            if (!isTyping) SkipText();

            else
            {
                skip = true;
                //EffectObject.SkipDotweenAnimation = true;
            }
        }
    }

    public void FastSkipText() // 텍스트 빠른 재생
    {
        if (chatSpeed != 0.01f)
        {
            originalChatSpeed = chatSpeed;
            chatSpeed = 0.01f;
        }
        else if (chatSpeed == 0.01f)
        {
            chatSpeed = originalChatSpeed;
        }
    }

    public void SkipText() // 텍스트 진행
    {
        if (backID >= 1) background[backID].SetActive(false);
        if (Sentence[chatID][typingID, 4] != "") imageSetactive(false);

        string eventName = Sentence[chatID][typingID, 5];
        if (eventName == "함수") Invoke(Sentence[chatID][typingID, 6], 0f);
        else if (eventName == "이동")
        {
            int num = UnityEngine.Random.Range(6, Convert.ToInt32(Sentence[chatID][typingID, 19]));
            chatID = Convert.ToInt32(Sentence[chatID][typingID, num]);
            typingID = 0;
        }
        if (eventName == "선택")
        {
            if (Sentence[chatID][typingID, 3] != "")
            {
                backID = Convert.ToInt32(Sentence[chatID][typingID, 3]) - 1;
                background[backID].SetActive(true);
            }
            textImage.SetActive(false);
            SelectOpen();
        }
        else
        {
            typingID++;
            if (typingID != max[chatID]) StartCoroutine(Typing());
            else textImage.SetActive(false);
        }
    }

    public void AutoPlay()
    {
        Auto = !Auto;
        if (!isTyping)
        {
            SkipText();
        }

        endObject.SetActive(false);
    }

    public void ShowTextLog(bool check)
    {
        textlogScroll.SetActive(check);
    }

    public void SelectOpen()
    {
        for (int i = 6; i < Convert.ToInt32(Sentence[chatID][typingID, 19]); i++)
        {
            select.Add(Sentence[chatID][typingID, i]);
            GameObject button = Instantiate(selectButton, selectPanel);
            Text selectText = button.transform.GetChild(0).GetComponent<Text>();
            selectText.text = Sentence[chatID][typingID, i];
            select.Add(Sentence[chatID][typingID, ++i]);
            button.SetActive(true);
        }
        selectPanel.gameObject.SetActive(true);
    }

    public void Select(GameObject selectObj)
    {
        for (int i = 1; i < selectPanel.transform.childCount; i++)
        {
            if (selectPanel.transform.GetChild(i).gameObject == selectObj)
            {
                int num = (i - 1) * 2;
                chatID = Convert.ToInt32(select[num + 1]);
                selectPanel.gameObject.SetActive(false);
                textImage.SetActive(true);

                typingID = 1;
                StartCoroutine(Typing());
            }
        }
    }

    private void imageSetactive(bool set)
    {
        imageList = Sentence[chatID][typingID, 4].Split(',');
        foreach (string x in imageList)
        {
            image[Convert.ToInt32(x) - 1].SetActive(set);
            effectObject = image[Convert.ToInt32(x) - 1];
        }
    }

    //private void PlayMusic(int num)
    //{
    //    GameManager.Instance.SOUND.PauseMusic();
    //    GameManager.Instance.SOUND.PlayingMusic(num, 0.5f);
    //}

    //public void CallCameraShake()
    //{
    //    //Camera.main.GetComponent<CameraShaking>().ShakeCam(shakeTime);
    //    CameraShaking.Instance.ShakeCam(shakeTime, shakestr);
    //}

    //public void InputNameCanvasOpen()
    //{
    //    GameManager.Instance.InputNameCanvas.SetActive(true);
    //}

    private void TestFunction()
    {
        print("따라란");
    }
}