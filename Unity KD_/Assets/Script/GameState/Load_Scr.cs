using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load_Scr : MonoBehaviour
{
    private AsyncOperation async;
    public GameObject LoadingUi;

    public Image image;
    Color color;

    private void Start()
    {
        color = image.color;
        LoadNextScene();
    }
    public void LoadNextScene()
    {
        LoadingUi.SetActive(true);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync("MainGame");

        while (!async.isDone)
        {
            color.a = async.progress;
            image.color = color;
            yield return null;
        }
    }
}
