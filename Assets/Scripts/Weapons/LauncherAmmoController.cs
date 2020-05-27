using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LauncherAmmoController : MonoBehaviour
{
    public int Ammo = 2;

    // Update is called once per frame
    void LateUpdate()
    {
        GetComponent<UnityEngine.UI.Text>().text = Ammo.ToString();
    }

}