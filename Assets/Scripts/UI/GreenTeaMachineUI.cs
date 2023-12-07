using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenTeaMachineUI : MonoBehaviour
{
    [SerializeField]
    private Image progressUI;
    [SerializeField]
    private GreenTeaMachine greenTeaMachine;
    [SerializeField]
    private Transform clockPanel;

   
    private void Start()
    {
        greenTeaMachine.OnProgressChanged += GreenTeaMachine_OnProgressChanged;
        greenTeaMachine.OnGreenTeaChanged += GreenTeaMachine_OnGreenTeaChanged;
    }

    private void GreenTeaMachine_OnGreenTeaChanged(object sender, GreenTeaMachine.OnGreenTeaMachineActive e)
    {
        clockPanel.gameObject.SetActive(e.isActive);
    }

    private void GreenTeaMachine_OnProgressChanged(object sender, IHasProgress.OnProgressChangedArg e)
    {
        progressUI.fillAmount = e.progress;
    }
}
