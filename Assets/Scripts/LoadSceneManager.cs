using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField]
    private Button playBtn;

    public void Awake()
    {
        playBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(2);
        });
    }
    
}
