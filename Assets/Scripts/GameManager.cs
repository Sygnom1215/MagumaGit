using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] private TextManager textManager = null;
    public TextManager TEXT { get { return textManager; } }


    [SerializeField] public GameObject Buttons;
    [SerializeField] public GameObject TitlePanel;
    [SerializeField] GameObject optionPanel;

    public string PlayerName = "무명";

    //옵션
    [SerializeField] Slider chatSpeedSlider;
    [SerializeField] Slider audoSpeedSlider;

    //FadeIn
    [SerializeField] Image BlackImage;
    [SerializeField] GameObject BlackImageObject;

    //이름입력
    public GameObject InputNameCanvas;
    [SerializeField] private InputField inputField;


    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(Instance);
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        TitlePanel.SetActive(true);
        Buttons.SetActive(true);
        textManager = GetComponent<TextManager>();
    }

    private void Start()
    {
        TEXT.chatSpeed = PlayerPrefs.GetFloat("chatSpeed", 1);
        //chatSpeedSlider.maxValue = 0.3f; // 슬라이더의 최댓값을 0.3으로 설정
        //chatSpeedSlider.minValue = 0.05f; // 슬라이더의 최솟값을 0.05로 설정
        chatSpeedSlider.value = chatSpeedSlider.value * -1; // 슬라이더 값 -1 곱하기
        chatSpeedSlider.value = TEXT.chatSpeed; // 슬라이더 값을 chatspeed로 변경
        TEXT.autoSpeed = PlayerPrefs.GetFloat("auto", 1);
        audoSpeedSlider.value = audoSpeedSlider.value * -1;
        audoSpeedSlider.value = TEXT.autoSpeed;
        Debug.Log(PlayerPrefs.GetFloat("auto", 1));
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            OptionPanelOC(0);
        }
    }

    public void Game(string name)
    {
        switch (name)
        {
            case "시작":
                TitlePanel.SetActive(false);
                Buttons.SetActive(false);
                textManager.chatID = 1;
                StartCoroutine(FadeIn());
                StartCoroutine(textManager.Typing());
                break;
            case "종료":
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                break;
            default:
                break;
        }
    }

    public void OptionPanelOC(int check)
    {
        if (check == 1)
        {
            optionPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else optionPanel.SetActive(true);
    }

    public IEnumerator FadeIn()
    {
        Debug.Log("실행");
        BlackImageObject.SetActive(true);
        Color color = BlackImage.color;
        color.a = 1f;
        while (color.a >= 0)
        {
            color.a -= 0.06f;
            BlackImage.color = color;
            yield return new WaitForSeconds(0.1f);
        }
        BlackImageObject.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        BlackImageObject.SetActive(true);
        Color color = BlackImage.color;
        while (color.a != 100)
        {
            color.a += 0.05f;
            BlackImage.color = color;
            BlackImageObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
    }


    public IEnumerator StopCroutine()
    {
        StopAllCoroutines();
        yield break;
    }

    public void BackTitle()
    {
        StopAllCoroutines();
        OptionPanelOC(1);
        BlackImageObject.SetActive(false);
        TEXT.textImage.gameObject.SetActive(false);
        TEXT.background[TEXT.backID].gameObject.SetActive(false);
        TitlePanel.SetActive(true);
        Buttons.SetActive(true);
    }

    public void InputName()
    {
        PlayerName = inputField.text;
        InputNameCanvas.SetActive(false);
        TEXT.chatID = 100003;
        StartCoroutine(TEXT.LoadTextData());
        StartCoroutine(TEXT.Typing());
    }
}