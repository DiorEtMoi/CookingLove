using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SasimiMachine : MonoBehaviour, IHasProgress
{
    [SerializeField]
    private FoodObjectSO foodSO;
    [SerializeField]
    private Transform foodPos;
    [SerializeField]
    private FoodRecipeSO recipeSO;

    private float timeCurrent;

    private Transform foodCurrent;

    public event EventHandler OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedArg> OnProgressChanged;
    public static event EventHandler OnServerToPlates;
    public enum SasimiMachineState
    {
        START,
        CUTTING,
        DONE
    }
    private SasimiMachineState state;

   
    private void Awake()
    {
        state = SasimiMachineState.START;
    }
    public static void ResetStatic()
    {
        OnServerToPlates = null;
    }
    public void Update()
    {
        if (foodSO == null) return;
        switch (state)
        {
            case SasimiMachineState.START:
                timeCurrent = 0;
                state = SasimiMachineState.CUTTING;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedArg
                {
                    progress = timeCurrent
                });
                break;
            case SasimiMachineState.CUTTING:
                timeCurrent += Time.deltaTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArg
                {
                    progress = timeCurrent / recipeSO.timer
                }) ;
                if (timeCurrent > recipeSO.timer)
                {
                    ClearFoodCurrent();
                    SpawnFood(recipeSO.output);
                    state = SasimiMachineState.DONE;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case SasimiMachineState.DONE:
                break;
        }
    }
    public void OnMouseUpAsButton()
    {
        if(state == SasimiMachineState.DONE)
        {
            if (SasimiPlateController.instance.CanAddAFoodToPlate(recipeSO.output))
            {
                SasimiPlateController.instance.AddFoodToPlate(recipeSO.output);
                state = SasimiMachineState.START;
                ClearFoodSOAndCurrent();
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                OnServerToPlates?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                SasimiPlateController.instance.HandleVisualNoPlate();
            }
        }
    }
    public void ClearFoodSOAndCurrent()
    {
        ClearFoodCurrent();
        SetFoodObjectSO(null);
    }
    public void SetFoodObjectSO(FoodObjectSO foodSO)
    {
        this.foodSO = foodSO;
    }
    public FoodObjectSO GetFoodObjectSO()
    {
        return this.foodSO;
    }
    public Transform GetTopPosition()
    {
        return foodPos;
    }
    public void SpawnFood(FoodObjectSO foodSO)
    {
        foodCurrent = Instantiate(foodSO.foodPerfap, foodPos);
    }
    public void ClearFoodCurrent()
    {
        Destroy(foodCurrent.gameObject);
    }
    public SasimiMachineState GetCurrentState()
    {
        return state;
    }
}
