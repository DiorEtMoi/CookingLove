using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTeaMachine : MonoBehaviour, IHasProgress
{
    [SerializeField]
    private FoodObjectSO foodSO;

    private float timeCurrent;
    private float timeMax = 3f;
    public event EventHandler<OnGreenTeaMachineActive> OnGreenTeaChanged;
    public event EventHandler<IHasProgress.OnProgressChangedArg> OnProgressChanged;
    public class OnGreenTeaMachineActive : EventArgs
    {
        public bool isActive;
    }

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void Update()
    {
        if (GreenTeaPlatesController.Instance.CanAddGreenTeaToPlates())
        {
            OnGreenTeaChanged?.Invoke(this, new OnGreenTeaMachineActive
            {
                isActive = true
            });
            timeCurrent += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArg
            {
                progress = timeCurrent / 3f
            });
            if (timeCurrent >= timeMax)
            {
                GreenTeaPlatesController.Instance.AddGreenTeaToPlate(foodSO);
                timeCurrent = 0;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArg
                {
                    progress = timeCurrent / 3f
                });
            }
            source.mute = false;
        }
        else
        {
            OnGreenTeaChanged?.Invoke(this, new OnGreenTeaMachineActive
            {
                isActive = false
            });
            source.mute = true;

        }
    }

}
