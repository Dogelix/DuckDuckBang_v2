using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using static SoundManager;

public class Shotgun : MonoBehaviour
{
    public GameObject Barrel;
    public GameObject Parent;

    public SteamVR_Action_Boolean _fireAction = null;

    public float firerate = 0.25f;
    public float laserRange = 5000f;
    public float bloom;
    public float pellets;

    private SteamVR_Behaviour_Pose _pose;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.1f);

    private LineRenderer laser;

    private float nextFire;

    private GameObject sphere;

    private SoundManager soundManager;

    void Awake()
    {
        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_fireAction.GetStateDown(_pose.inputSource) && Time.time > nextFire)
        {
            Debug.Log("Parent Forward Vector: " + Parent.transform.forward);
            nextFire = Time.time + firerate;

            for (int i = 0; i < pellets; i++)
            {



                StartCoroutine(FireEffect());

                //bloom
                Vector3 _bloom = Barrel.transform.position + Parent.transform.forward * 1000f;
                _bloom += Random.Range(-bloom, +bloom) * Barrel.transform.up;
                _bloom += Random.Range(-bloom, +bloom) * Barrel.transform.right;
                _bloom -= Barrel.transform.position;
                _bloom.Normalize();

                //raycast
                RaycastHit hit = new RaycastHit();
                Ray shot = new Ray(Barrel.transform.position, Parent.transform.forward);
                laser.SetPosition(0, Barrel.transform.position);


                if (Physics.Raycast(Barrel.transform.position, _bloom, out hit, laserRange))
                {
                    laser.SetPosition(1, hit.point);

                    if (hit.collider.tag == "Enemy")
                    {
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("GroundEnemy"))
                        {
                            hit.transform.gameObject.GetComponent<GroundAgentCollision>().RaycastDestroy();

                        }
                        else
                        {
                            hit.transform.gameObject.GetComponent<CollisionDetection>().RaycastDestroy();
                        }
                    }
                    else
                    {
                        hit.transform.gameObject.GetComponent<QuitScript>().Activate();
                    }
                }
                else
                {
                    laser.SetPosition(1, Barrel.transform.position + (laserRange * Parent.transform.forward));
                }
            }
        }
    }


    private IEnumerator FireEffect()
    {
        laser.enabled = true;
        yield return shotDuration;
        laser.enabled = false;
    }
}
