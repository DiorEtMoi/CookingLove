using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTeaPlate : MonoBehaviour
{
    [SerializeField]
    private FoodObjectSO foodSO;

    public event EventHandler<OnFoodChangedArg> OnFoodChanged;
    public class OnFoodChangedArg : EventArgs
    {
        public FoodObjectSO food;
    }
    public void SetFoodSO(FoodObjectSO foodSO)
    {
        this.foodSO = foodSO;
        OnFoodChanged?.Invoke(this, new OnFoodChangedArg
        {
            food = this.foodSO
        });
    }
    public FoodObjectSO GetFoodObjectSO()
    {
        return this.foodSO;
    }
    public void ClearFoodOnPlate()
    {
        SetFoodSO(null);
    }
    public void OnMouseUpAsButton()
    {
        if(this.foodSO != null)
        {
            bool isCorrect = OrderMenuManager.instance.DeliveryFoodForCustomer(foodSO, transform);
            if (isCorrect)
            {
                ClearFoodOnPlate();
            }
            Debug.Log(isCorrect);
        }
    }

}
