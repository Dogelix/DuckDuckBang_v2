using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitScript : MenuParentClass
{
    public override void Activate()
    {
        Application.Quit();
    }
}
