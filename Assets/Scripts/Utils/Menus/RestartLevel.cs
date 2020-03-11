using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MenuParentClass
{
    public override void Activate()
    {
        FindObjectOfType<GameMode_SO>().RestartGameMode();
    }
}
