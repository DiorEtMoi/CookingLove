using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePointUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointUI;
    [SerializeField]
    private TextMeshProUGUI notDoneOrderUI;

    private void Start()
    {
        GameManager.instance.OnPointChanged += Instance_OnPointChanged;
        GameManager.instance.OnNotDoneOrderChanged += Instance_OnNotDoneOrderChanged;
        pointUI.text = "0";
        notDoneOrderUI.text = "Not done Order : " + GameManager.instance.GetNotDoneOrder().ToString(); ;
    }

    private void Instance_OnNotDoneOrderChanged(object sender, System.EventArgs e)
    {
        notDoneOrderUI.text = "Not done Order : " + GameManager.instance.GetNotDoneOrder().ToString();
    }

    private void Instance_OnPointChanged(object sender, System.EventArgs e)
    {
        pointUI.text = GameManager.instance.GetGamePoint().ToString();
    }
}
