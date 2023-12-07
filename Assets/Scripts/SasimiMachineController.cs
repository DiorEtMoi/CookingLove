using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasimiMachineController : MonoBehaviour
{
    public static SasimiMachineController instance { get; private set; }
    [SerializeField]
    private List<SasimiMachine> sasimiMachines;


    public void Awake()
    {
        instance = this;
    }
    public void AddFoodToMachine(FoodObjectSO foodSo)
    {
        SasimiMachine machineEmpty = FindSasimiMachineEmpty();
        if(machineEmpty != null)
        {
            machineEmpty.SetFoodObjectSO(foodSo);
            machineEmpty.SpawnFood(foodSo);
        }
        else
        {
            Debug.Log("All machine full food");
        }
    }

    public SasimiMachine FindSasimiMachineEmpty()
    {
        foreach (SasimiMachine machine in sasimiMachines)
        {
            if(machine.GetFoodObjectSO() == null)
            {
                return machine;
            }
        }
        return null;
    }
}
