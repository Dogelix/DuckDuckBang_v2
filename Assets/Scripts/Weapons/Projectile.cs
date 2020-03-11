using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Projectile : MonoBehaviour
{
    public float shotPower = 8000f;
    public GameObject Barrel;
    public GameObject Parent;

    public SteamVR_Action_Boolean _fireAction = null;

    private SteamVR_Behaviour_Pose _pose;

    void Awake()
    {
        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_fireAction.GetStateDown(_pose.inputSource))
        {
            var bullet = Instantiate(GameAssets.i.Bullet, Barrel.transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(Parent.transform.forward * shotPower);
        }
    }
}
