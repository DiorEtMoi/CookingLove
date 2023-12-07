using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasimiHolder : MonoBehaviour
{
    [SerializeField]
    private FoodObjectSO foodSO;
    public static event EventHandler OnClickToHolder;
    public static void ResetStatic()
    {
        OnClickToHolder = null;
    }
    private void OnMouseUpAsButton()
    {
        SasimiMachineController.instance.AddFoodToMachine(foodSO);
        OnClickToHolder?.Invoke(this, EventArgs.Empty);
    }
}
