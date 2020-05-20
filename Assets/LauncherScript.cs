﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LauncherScript : MonoBehaviour
{
    public GameObject Barrel;
    public GameObject Parent;
    public GameObject Rocket;
    public SteamVR_Action_Boolean _fireAction = null;
    private SteamVR_Behaviour_Pose _pose;
    public float firerate = 0.25f;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.1f);
    private float nextFire;
    public float shotPower;

    public GameObject Ammo;

    void Awake()
    {
        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }

    private void Update()
    {
        Shoot();
    }


    public bool Shoot()
    {
        int ammo = Int32.Parse(Ammo.GetComponent<UnityEngine.UI.Text>().text);

        if (_fireAction.GetStateDown(_pose.inputSource) && Time.time > nextFire && ammo > 0)
        {
            nextFire = Time.time + firerate;

            var rocket = Instantiate(Rocket, Barrel.transform.position, transform.rotation);
            rocket.GetComponent<Rigidbody>().AddForce(Parent.transform.forward * shotPower);
            Ammo.GetComponent<UnityEngine.UI.Text>().text = (ammo - 1).ToString();
        }
        return false;
    }
}
