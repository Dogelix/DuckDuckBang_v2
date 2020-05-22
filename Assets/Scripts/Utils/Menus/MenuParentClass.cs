using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuParentClass : MonoBehaviour, IMenuItem
{
    public string[] _parameters;

    public virtual void Activate() { }
}
