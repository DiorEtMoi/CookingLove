using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasimiPlateController : MonoBehaviour
{
    public static SasimiPlateController instance { get; private set; }

    [SerializeField]
    private List<SasimiPlates> sasimiPlates;

    [SerializeField]
    private FoodObjectSO sashimiSO;
    public void Awake()
    {
        instance = this;
    }
    public void AddFoodToPlate(FoodObjectSO foodSO)
    {
        SasimiPlates sasimiPlates = FindThePlateNotContainSameType(foodSO);
        if(sasimiPlates != null)
        {
            sasimiPlates.AddAFoodToPlate(foodSO);
        }
    }
    public void AddSpiceForFood(FoodObjectSO foodSO)
    {
        SasimiPlates plate = FindTheSashimiPlateForSpice(foodSO);
        plate.AddAFoodToPlate(foodSO);
    }
    public bool CanAddAFoodToPlate(FoodObjectSO foodSO)
    {
        foreach(SasimiPlates plate in sasimiPlates)
        {
            if (!plate.IsExistAFoodSameTypeOnPlate(foodSO))
            {
                return true;
            }
        }
        return false;
    }
    public bool CanAddSpiceForFood(FoodObjectSO foodSO)
    {
        foreach (SasimiPlates plate in sasimiPlates)
        {
            if (!plate.IsExistAFoodSameTypeOnPlate(foodSO) && plate.IsExistAFoodSameTypeOnPlate(sashimiSO))
            {
                return true;
            }
        }
        return false;
    }
    public SasimiPlates FindTheSashimiPlateForSpice(FoodObjectSO foodSO)
    {
        foreach (SasimiPlates plate in sasimiPlates)
        {
            if (!plate.IsExistAFoodSameTypeOnPlate(foodSO) && plate.IsExistAFoodSameTypeOnPlate(sashimiSO))
            {
                return plate;
            }
        }
        return null;
    }
    private SasimiPlates FindThePlateNotContainSameType(FoodObjectSO foodSO)
    {
       foreach(SasimiPlates plate in sasimiPlates)
        {
            if (!plate.IsExistAFoodSameTypeOnPlate(foodSO))
            {
                return plate;
            }
        }
        return null;
    }
    public void HandleVisualNoPlate()
    {
        sasimiPlates[0].transform.DOMoveY(-1.5f, 0.1f).OnComplete(() =>
        {
            sasimiPlates[0].transform.DOMoveY(-1.733f, 0.1f);
        });
    }
}
