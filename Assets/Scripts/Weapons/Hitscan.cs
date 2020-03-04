using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using static SoundManager;

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

    private SoundManager soundManager;

    void Awake()
    {
        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }


    void Start()
    {
        laser = GetComponent<LineRenderer>();
        soundManager = FindObjectOfType<SoundManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (/*_fireAction.GetStateDown(_pose.inputSource)*/ Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            soundManager.PlaySound(SoundsNames.GunShot_1, false);

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
                    hit.transform.gameObject.GetComponent<QuitScript>().RaycastDestroy();
                }
            }
            else
            {
                laser.SetPosition(1, Barrel.transform.position + (laserRange * Parent.transform.forward));
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
