using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using static SoundManager;

public class Launcher : MonoBehaviour
{
    public GameObject rocketPrefab;
    public SteamVR_Action_Boolean _fireAction = null;
    public float propulsionForce;

    private SteamVR_Behaviour_Pose _pose;
    private float nextFire;
    public float firerate = 0.25f;
    private Transform rocketTransform;
    void Awake()
    {
        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }

    void Start()
    {
        SetInitialReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (_fireAction.GetStateDown(_pose.inputSource) && Time.time > nextFire)
        {
            nextFire = Time.time + firerate;

            SpawnRocket();
        }
    }

    void SpawnRocket()
    {
        GameObject rocket = (GameObject) Instantiate(rocketPrefab, rocketTransform.transform.TransformPoint(0, 0, 2f), rocketTransform.rotation);
        rocket.GetComponent<Rigidbody>().AddForce(rocketTransform.forward * propulsionForce, ForceMode.Impulse);
        Destroy(rocket, 3);
    }

    void SetInitialReferences()
    {
        rocketTransform = transform;
    }
}
