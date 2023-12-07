using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SasimiPlates : MonoBehaviour
{
    [SerializeField]
    private List<FoodObjectSO> foodsOnPlate;

    public event EventHandler<OnAddAFoodType> OnAddAFoodToPlate;
    public event EventHandler OnRemoveAFoodFromPlate;
    public class OnAddAFoodType : EventArgs
    {
        public FoodObjectSO food;
    }

    private float lastTime;

    public static event EventHandler OnTrashFood;
    public void Awake()
    {
        foodsOnPlate = new List<FoodObjectSO>();
    }
   
    public static void ResetStatic()
    {
        OnTrashFood = null;
    }
    private void Instance_OnDeliverySuccess(object sender, EventArgs e)
    {
        ClearFoodOnPlate();
    }

    public void AddAFoodToPlate(FoodObjectSO foodSO)
    {
        foodsOnPlate.Add(foodSO);
        OnAddAFoodToPlate?.Invoke(this, new OnAddAFoodType
        {
            food = foodSO
        });
    }
    public bool IsExistAFoodSameTypeOnPlate(FoodObjectSO foodSO)
    {
        foreach(FoodObjectSO food in foodsOnPlate)
        {
            if(food.type == foodSO.type)
            {
                return true;
            }
        }
        return false;
    }
    public List<FoodObjectSO> GetFoodInPlate() { return foodsOnPlate; }

    public void ClearFoodOnPlate()
    {
        if(foodsOnPlate.Count > 0)
        {
            foodsOnPlate.Clear();
            OnRemoveAFoodFromPlate?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Nothing food on plate");
        }
    }

    public void OnMouseUpAsButton()
    {
        if(lastTime + 0.5f >= Time.time && foodsOnPlate.Count > 0)
        {
            ClearFoodOnPlate();
            OnTrashFood?.Invoke(this, EventArgs.Empty);
        }
        else
        {
           lastTime = Time.time;
          if(foodsOnPlate.Count > 0)
            {
                bool isCorrect = OrderMenuManager.instance.DeliveryFoodForCustomer(GetFoodInPlate(), transform);
                if (isCorrect)
                {
                    ClearFoodOnPlate();
                }
            }
        }
    }
}
