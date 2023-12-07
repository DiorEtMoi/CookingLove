using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodObjectSO : ScriptableObject
{
    public string foodName;
    public Transform foodPerfap;
    public Sprite icon;
    public TypeFood type;
    public enum TypeFood
    {
        FOOD,
        DIPPING_SAUCE,
        SPICE
    }
}
