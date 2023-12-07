using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SasimiPlateVisual : MonoBehaviour
{
    [Serializable]
    private struct FoodVisual_GameObject
    {
        public GameObject objectVisual;
        public FoodObjectSO foodSO;
    }

    [SerializeField]
    private FoodVisual_GameObject[] foodVisual;
    private SasimiPlates sasimiPlate;

    private void Awake()
    {
        sasimiPlate = GetComponentInParent<SasimiPlates>();
    }

    private void Start()
    {
        sasimiPlate.OnAddAFoodToPlate += SasimiPlate_OnAddAFoodToPlate;
        sasimiPlate.OnRemoveAFoodFromPlate += SasimiPlate_OnRemoveAFoodFromPlate;
    }

    private void SasimiPlate_OnRemoveAFoodFromPlate(object sender, EventArgs e)
    {
        foreach (FoodVisual_GameObject item in foodVisual)
        {
            item.objectVisual.SetActive(false);
        }
    }

    private void SasimiPlate_OnAddAFoodToPlate(object sender, SasimiPlates.OnAddAFoodType e)
    {
            foreach (FoodVisual_GameObject item in foodVisual)
            {
                if (item.foodSO == e.food)
                {
                    item.objectVisual.SetActive(true);
                    
                }
            }
            transform.DOLocalMoveY(0.1f,0.1f).OnComplete(() =>
            {
                transform.DOLocalMoveY(0, 0.1f);
            });
    }
}
