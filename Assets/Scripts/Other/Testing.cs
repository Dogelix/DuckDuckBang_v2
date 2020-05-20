using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Testing : MonoBehaviour
{
    //private UnityAction action; x -29.7    z -37.9

    private void Awake()
    {
        DebugPlus.DrawSphere(new Vector3(-29.7f, 0, -37.9f), 1);
        DebugPlus.DrawSphere(new Vector3(470.3f, 0, -37.9f), 1);
        DebugPlus.DrawSphere(new Vector3(-29.7f, 0, 462.1f), 1);
        DebugPlus.DrawSphere(new Vector3(470.3f, 0, 462.1f), 1);

    }

    //private void OnEnable()
    //{
    //    EventManager.StartListening("test", action);
    //}

    bool a = true;
    // Start is called before the first frame update
    void Update()
    {
    }

    //void SomeOtherFunction()
    //{
    //    Debug.Log("Some Other Function was called!");
    //}
}
