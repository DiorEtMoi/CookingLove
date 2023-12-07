using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTeaPlateVisual : MonoBehaviour
{
    [SerializeField]
    private Transform cocTraSprite;

    private GreenTeaPlate plate;

    private void Awake()
    {
        plate = GetComponent<GreenTeaPlate>();
    }
    private void Start()
    {
        plate.OnFoodChanged += Plate_OnFoodChanged;
    }

    private void Plate_OnFoodChanged(object sender, GreenTeaPlate.OnFoodChangedArg e)
    {
        if(e.food != null)
        {
            cocTraSprite.gameObject.SetActive(true);
        }
        else
        {
            cocTraSprite.gameObject.SetActive(false);

        }
    }
}
