using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelector : MenuParentClass
{
    public override void Activate()
    {
        SceneManager.LoadSceneAsync(_parameters[0]);
    }
}
