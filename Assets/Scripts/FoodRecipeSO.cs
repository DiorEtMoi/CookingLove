using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodRecipeSO : ScriptableObject
{
    public FoodObjectSO input;
    public FoodObjectSO output;
    public float timer;
}
