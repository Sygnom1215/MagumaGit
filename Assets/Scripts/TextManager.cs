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
    public GameObject textPanelObj; //�ؽ�Ʈ �г� ������Ʈ

    [SerializeField] private Transform selectPanel;

    [SerializeField] private GameObject _selectButton;
    public GameObject[] background; // ���� ���ȭ�� �迭
    public GameObject[] imageList;    // �߰��� ����� �̹��� 

    Dictionary<int, string[,]> Sentence = new Dictionary<int, string[,]>();
    Dictionary<int, int> max = new Dictionary<int, int>();
    List<string> select = new List<string>();

    public int chatID = 1, lineNumber = 1, backgroundID = 1, slotID = 0; // ID �⺻ �� ����
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
        StartCoroutine(LoadTextData()); // �ؽ�Ʈ ������ �б�
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
    /// ��� Ÿ����
    /// </summary>
    public void Typing()
    {
        if (Sentence[chatID] == null) //���� ��縦 �� �ҷ��Դٸ�
        {
            StartCoroutine(LoadTextData()); //�ٽ� �ҷ�����
        }

        StartCoroutine(TypingCoroutine()); //Ÿ���� �ڷ�ƾ ����
    }

    /// <summary>
    /// ��� Ÿ���� �ڷ�ƾ �Լ�
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
                    _textPanel.text = string.Format("{0}\n{1}", pName, storyText);           // �ؽ�Ʈ �ѱ�.....������ ������ �ѹ��� ��
                    _isSkip = false;
                    break;
                }


                _textPanel.text = string.Format("{0}\n{1}", pName, storyText.Substring(0, i));
                //soundManager.TypingSound(); // �ؽ�Ʈ ���....��������
                yield return new WaitForSeconds(0.2f);

                _textPanel.text = string.Format("{0}\n{1}", pName, storyText);
            }
        }

        else
        {
            _textPanel.text = string.Format("");
        }
     
        ///���� Ÿ���� ���� �� ����
        _isTyping = false;

    }

    //else
    //{
    //    SkipText();
    //}

    public void SkipTextClick() // �ؽ�Ʈ �г� Ŭ�� �� ��ŵ
    {
        if (!_isTyping) SkipText();
        else _isSkip = true;
    }

    public void SkipText() // �ؽ�Ʈ ����
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
            if (eventName == "�Լ�")
            {
                string funcName = Sentence[chatID][lineNumber, (int)IDType.Event + 1];
                StartCoroutine(funcName.Trim());
            }
            else if (eventName == "�̵�")
            {
                int num = UnityEngine.Random.Range((int)IDType.Event + 1, Convert.ToInt32(Sentence[chatID][lineNumber, 19]));
                chatID = Convert.ToInt32(Sentence[chatID][lineNumber, num]);
                lineNumber = 0;
            }

            //if (eventName == "����")
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