using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneAsyn : MonoBehaviour
{
    [SerializeField]
    private Slider processingBarUI;
    private void Awake()
    {
        StartCoroutine(LoadingSceneAsy(1));
    }

    IEnumerator LoadingSceneAsy(int sceneIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncOperation.isDone) 
        {
            processingBarUI.value = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            yield return null;
        }
    }
}
