using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Testing : MonoBehaviour
{
    //private UnityAction action;

    //private void Awake()
    //{
    //    Debug.Log(Application.dataPath);
    //    //action = new UnityAction(SomeOtherFunction);
    //}

    //private void OnEnable()
    //{
    //    EventManager.StartListening("test", action);
    //}


    // Start is called before the first frame update
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            GetComponentInChildren<GrappleHook>().ShootGrappleHook();
        }
        if ( Input.GetKeyDown(KeyCode.LeftShift) )
        {
            GetComponentInChildren<GrappleHook>().ReturnGrappleHook();
        }
    }

    //void SomeOtherFunction()
    //{
    //    Debug.Log("Some Other Function was called!");
    //}
}
