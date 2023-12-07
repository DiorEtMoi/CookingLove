using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTeaPlatesController : MonoBehaviour
{
    public static GreenTeaPlatesController Instance { get; private set; }
    [SerializeField]
    private List<GreenTeaPlate> greenTeaList;

    public void Awake()
    {
        Instance = this;
    }
    public void AddGreenTeaToPlate(FoodObjectSO foodSo)
    {
        foreach (GreenTeaPlate item in greenTeaList) 
        {
            if(item.GetFoodObjectSO() == null)
            {
                item.SetFoodSO(foodSo);
            }
        }
    }
    public bool CanAddGreenTeaToPlates()
    {
        foreach (GreenTeaPlate item in greenTeaList)
        {
            if (item.GetFoodObjectSO() == null)
            {
                return true;
            }
        }
        return false;
    }
}
