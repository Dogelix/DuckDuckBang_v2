using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hitscan : MonoBehaviour
{
    public GameObject Barrel;
    public GameObject Parent;

    public SteamVR_Action_Boolean _fireAction = null;

    public float firerate = 0.25f;

    public float laserRange = 5000f;

    private SteamVR_Behaviour_Pose _pose;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.1f);

    private LineRenderer laser;

    private float nextFire;

    private GameObject sphere;

    void Awake()
    {
        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        sphere = GameObject.FindGameObjectWithTag("temp");
    }


    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        sphere.transform.position = Barrel.transform.position + (Parent.transform.forward * laserRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (_fireAction.GetStateDown(_pose.inputSource) && Time.time > nextFire)
        {
            Debug.Log("Parent Forward Vector: " + Parent.transform.forward);
            nextFire = Time.time + firerate;

            StartCoroutine(FireEffect());

            RaycastHit hit = new RaycastHit();
            Ray shot = new Ray(Barrel.transform.position, Parent.transform.forward);
            laser.SetPosition(0, Barrel.transform.position);

            if (Physics.Raycast(shot, out hit, laserRange))
            {
                laser.SetPosition(1, hit.point);

                if (hit.collider.tag == "Enemy")
                {
                    //hit.collider.gameObject.GetComponent<Enemy>().ImHit();
                }
            }
            else
            {
                laser.SetPosition(1, Barrel.transform.position + (laserRange * Parent.transform.forward));
            }
        }
    }

    //public void Shoot()
    //{
    //    RaycastHit hit = new RaycastHit();
    //    Ray shot = new Ray(Barrel.transform.position, transform.forward); 

    //    if(Physics.Raycast(shot, out hit, 1000))
    //    {
    //        if (hit.collider.attachedRigidbody)
    //        {
    //            Debug.Log(hit.collider.gameObject.name + " has been hit");
    //        }
    //    }

    //}

    private IEnumerator FireEffect()
    {
        laser.enabled = true;
        yield return shotDuration;
        laser.enabled = false;
    }
}
