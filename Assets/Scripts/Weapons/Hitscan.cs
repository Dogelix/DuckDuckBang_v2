using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using static SoundManager;

public class Hitscan : MonoBehaviour, IShootable
{
    public GameObject Barrel;
    public GameObject Parent;

    public SteamVR_Action_Boolean _fireAction = null;

    private SteamVR_Behaviour_Pose _pose;


    public float firerate = 0.25f;

    public float laserRange = 5000f;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.1f);

    private LineRenderer laser;

    private float nextFire;


    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    void Awake()
    {
        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }

    private void Update()
    {
        Shoot();
    }

    private IEnumerator FireEffect()
    {
        laser.enabled = true;
        yield return shotDuration;
        laser.enabled = false;
    }

    public bool Shoot()
    {
        if (_fireAction.GetStateDown(_pose.inputSource) && Time.time > nextFire)
        {
            //soundManager.PlaySound(SoundsNames.GunShot_1, false, false);

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
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("GroundEnemy") || hit.collider.gameObject.layer == LayerMask.NameToLayer("GoldRegi"))
                    {
                        Destroy(hit.transform.gameObject);
                    }
                    else
                    {
                        hit.transform.gameObject.GetComponent<CollisionDetection>().RaycastDestroy();
                    }
                }
                else if (hit.collider.tag == "PowerUp")
                {
                    Destroy(hit.transform.gameObject);
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
            return true;
        }
        return false;
    }
}
