using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SashimiMachineUI : MonoBehaviour
{
    [SerializeField]
    private Transform sashimiMachineUI_Panel;
    [SerializeField]
    private SasimiMachine sashimiMachine;
    [SerializeField]
    private Image progressTimerUI;
    public void Start()
    {
        sashimiMachine.OnStateChanged += SashimiMachine_OnStateChanged;
        sashimiMachine.OnProgressChanged += SashimiMachine_OnProgressChanged;
    }

    private void SashimiMachine_OnProgressChanged(object sender, IHasProgress.OnProgressChangedArg e)
    {
        progressTimerUI.fillAmount = e.progress;
    }

    private void SashimiMachine_OnStateChanged(object sender, System.EventArgs e)
    {
        SasimiMachine.SasimiMachineState state = sashimiMachine.GetCurrentState();
        if(state == SasimiMachine.SasimiMachineState.CUTTING)
        {
            sashimiMachineUI_Panel.gameObject.SetActive(true);
        }else if (state == SasimiMachine.SasimiMachineState.DONE)
        {
            sashimiMachineUI_Panel.gameObject.SetActive(false);
        }
    }
}
