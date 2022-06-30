using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Text npcDialog;

    private bool isOpenMenu = false;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        menuPanel.SetActive(false);
        CloseText();
    }

    void Update()
    {
        // ESC
        if(Input.GetKeyDown(KeyCode.Escape))
            OpenMenu();
    }

    public void OpenMenu()
    {
        if (isOpenMenu == false)
        {
            menuPanel.SetActive(true);
            isOpenMenu = true;
            Time.timeScale = 0;
        }
        else if (isOpenMenu == true)
        {
            menuPanel.SetActive(false);
            isOpenMenu = false;
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name); // 자신이 있는 Scene만 Reroad 되는 코드로 변경할 것.
        Debug.Log("Restart");
        Time.timeScale = 1;
    }

    public void BackStage()
    {
        SceneManager.LoadScene("Title");
    }    

    public void OpenText(string textData, Vector3 npcPos)
    {
        npcDialog.gameObject.SetActive(true);
        npcDialog.text = textData;
        npcDialog.rectTransform.anchoredPosition = npcPos;
        Time.timeScale = 0f;
    }

    public void CloseText()
    {
        npcDialog.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
