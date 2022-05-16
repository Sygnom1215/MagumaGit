using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LoadingManager : MonoBehaviour
{
    private static string nextScene;
    [SerializeField]
    private TextMeshProUGUI sceneName;
    
    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    /// <summary>
    /// �� �ε��ϴ� �Լ�
    /// </summary>
    /// <param name="sceneName">�ε��� ���� �̸�</param>
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    private IEnumerator LoadSceneProcess()
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
       

        while (!op.isDone)
        {
            yield return null;
            if (op.progress < 0.9f)
            {
                sceneName.text = nextScene;
                yield return new WaitForSeconds(1f);
                op.allowSceneActivation = true;
                yield break;
            }

        }
    }

}
