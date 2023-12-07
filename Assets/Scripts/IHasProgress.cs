using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress 
{
    public event EventHandler<OnProgressChangedArg> OnProgressChanged;
    public class OnProgressChangedArg : EventArgs
    {
        public float progress;
    }
}
