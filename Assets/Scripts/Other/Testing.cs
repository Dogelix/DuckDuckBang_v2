using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Testing : MonoBehaviour
{
    //private UnityAction action; x -29.7    z -37.9

    private void Awake()
    {
        
    }

    //private void OnEnable()
    //{
    //    EventManager.StartListening("test", action);
    //}

    bool a = true;
    // Start is called before the first frame update
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Alpha1)) Debug.Log(Input.GetKeyDown(KeyCode.Alpha1));
    }

    //void SomeOtherFunction()
    //{
    //    Debug.Log("Some Other Function was called!");
    //}
}
