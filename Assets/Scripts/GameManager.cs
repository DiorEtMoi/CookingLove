using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int gamePoint;
    private int notDoneOrder;

    public event EventHandler OnPointChanged;
    public event EventHandler OnLoseGame;
    public event EventHandler OnNotDoneOrderChanged;
    public event EventHandler OnContinueGame;
    private void Awake()
    {
        instance = this;
        gamePoint = 0;
        notDoneOrder = 0;
    }

    public void AddPoint(int point)
    {
        gamePoint += point;
        OnPointChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetGamePoint()
    {
        return gamePoint;
    }
    public int GetNotDoneOrder()
    {
        return notDoneOrder;
    }
    public void UpdateNotDoneOrder()
    {
        notDoneOrder++;
        OnNotDoneOrderChanged?.Invoke(this, EventArgs.Empty);   
        if (notDoneOrder >= 10)
        {
            Debug.Log("Losing Game");
            PlayerPrefs.SetInt("BestScore", gamePoint);
            OnLoseGame?.Invoke(this, EventArgs.Empty);  
        }
    }
    public void ContinueGame()
    {
        notDoneOrder = 0;
        OnNotDoneOrderChanged?.Invoke(this, EventArgs.Empty);
        OnContinueGame?.Invoke(this, EventArgs.Empty);
    }
}
