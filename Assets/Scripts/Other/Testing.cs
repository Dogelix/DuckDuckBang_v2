using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Testing : MonoBehaviour
{
    //private UnityAction action;

    private void Awake()
    {
        GetComponent<Text>().text = Localisation.GetLocaleValue("test_message");
    }

    //private void OnEnable()
    //{
    //    EventManager.StartListening("test", action);
    //}

    bool a = true;
    // Start is called before the first frame update
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.D) )
        {
            Localisation.Language = Language.DE;
            a = !a;
        }
        if ( Input.GetKeyDown(KeyCode.F) )
        {
            Localisation.Language = Language.FR;
            a = !a;
        }
        if ( Input.GetKeyDown(KeyCode.P) )
        {
            Localisation.Language = Language.PL;
            a = !a;
        }
        if ( Input.GetKeyDown(KeyCode.E) )
        {
            Localisation.Language = Language.EN;
            a = !a;
        }
        if ( !a ) { a = true; GetComponent<Text>().text = Localisation.GetLocaleValue("test_message"); }
    }

    //void SomeOtherFunction()
    //{
    //    Debug.Log("Some Other Function was called!");
    //}
}
