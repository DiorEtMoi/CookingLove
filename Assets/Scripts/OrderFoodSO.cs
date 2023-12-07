using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OrderFoodSO : ScriptableObject
{
    public string foodName;
    public Transform perfap;
    public List<FoodObjectSO> foodCompleted;
}
