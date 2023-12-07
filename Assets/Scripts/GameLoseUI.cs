using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoseUI : MonoBehaviour
{
    [SerializeField]
    private Transform losePanel;
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private Button exitBtn;
    [SerializeField]
    private Button restartGame;
    [SerializeField]
    private TextMeshProUGUI bestPoint;
    [SerializeField]
    private Button continueBtn;

    private void Awake()
    {
        exitBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        });
        restartGame.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
        });
        
        bestPoint.text = PlayerPrefs.GetInt("BestScore").ToString();
    }
    private void Start()
    {
        GameManager.instance.OnLoseGame += Instance_OnLoseGame;
        GameManager.instance.OnContinueGame += Instance_OnContinueGame;
    }

    private void Instance_OnContinueGame(object sender, System.EventArgs e)
    {
        Time.timeScale = 1f;
        losePanel.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
        {
            background.SetActive(false);
        });
    }

    private void Instance_OnLoseGame(object sender, System.EventArgs e)
    {
        background.SetActive(true);
        losePanel.DOScale(Vector3.one, 0.2f).OnComplete(() =>
        {
            exitBtn.transform.DOScale(Vector3.one,0.2f).OnComplete(() =>
            {
                restartGame.transform.DOScale(Vector3.one, 0.2f).OnComplete(() =>
                {
                    continueBtn.transform.DOScale(Vector3.one, 0.2f).OnComplete(() =>
                    {
                        Time.timeScale = 0f;
                    });
                });
            } );
        });
    }
}
