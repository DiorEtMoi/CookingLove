using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHolder : MonoBehaviour
{
    [SerializeField]
    private FoodObjectSO foodSO;

    private void OnMouseUpAsButton()
    {
        switch (foodSO.type)
        {
            case FoodObjectSO.TypeFood.SPICE:
                if (SasimiPlateController.instance.CanAddSpiceForFood(foodSO))
                {
                    SasimiPlateController.instance.AddSpiceForFood(foodSO);
                }
                break;
            case FoodObjectSO.TypeFood.DIPPING_SAUCE:
                if (SasimiPlateController.instance.CanAddSpiceForFood(foodSO))
                {
                    SasimiPlateController.instance.AddSpiceForFood(foodSO);
                }
                break;
        }
        transform.DOScale(new Vector2(1.1f, 1.1f), 0.1f).OnComplete(() =>
        {
            transform.DOScale(Vector2.one, 0.1f);
        });
    }
    
}
