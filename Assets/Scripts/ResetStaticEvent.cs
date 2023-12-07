using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticEvent : MonoBehaviour
{
    public void Awake()
    {
        SasimiHolder.ResetStatic();
        SasimiPlates.ResetStatic();
        SasimiMachine.ResetStatic();
    }
}
