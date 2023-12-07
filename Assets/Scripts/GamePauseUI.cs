using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField]
    private Transform pauseContainer;
    [SerializeField]
    private Transform pausePanel;
    [SerializeField]
    private Button pauseBtn;
    [SerializeField]
    private Transform resumeBtn;
    [SerializeField]
    private Transform quitBtn;

    private void Awake()
    {
        pauseBtn.onClick.AddListener(() =>
        {
            pausePanel.gameObject.SetActive(true);
            
            pauseContainer.DOScale(Vector3.one, 0.2f).OnComplete(() =>
            {
                quitBtn.DOScale(Vector3.one, 0.2f).OnComplete(() =>
                {
                    resumeBtn.DOScale(Vector3.one, 0.2f).OnComplete(() =>
                    {
                        Time.timeScale = 0;
                    });
                    
                });
            });
        });
        resumeBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            pausePanel.gameObject.SetActive(false);
            pauseContainer.DOScale(Vector3.zero, 0.2f);
            resumeBtn.localScale = Vector3.zero;
            quitBtn.localScale = Vector3.zero;
        });
        quitBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        });
    }
}
