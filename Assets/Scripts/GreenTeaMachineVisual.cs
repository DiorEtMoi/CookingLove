using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTeaMachineVisual : MonoBehaviour
{
    private GreenTeaMachine machine;
    private Animator animator;
    private void Awake()
    {
        machine = GetComponent<GreenTeaMachine>();
        animator = GetComponent<Animator>();
    }
    public void Start()
    {
        machine.OnGreenTeaChanged += Machine_OnGreenTeaChanged;
    }

    private void Machine_OnGreenTeaChanged(object sender, GreenTeaMachine.OnGreenTeaMachineActive e)
    {
        animator.SetBool("RotTra", e.isActive);
    }
}
