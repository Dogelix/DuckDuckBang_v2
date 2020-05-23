using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ShotgunScript : MonoBehaviour, IShootable
{
    public GameObject Barrel;
    public GameObject Parent;
    public GameObject Bullet;
    public SteamVR_Action_Boolean _fireAction = null;
    private SteamVR_Behaviour_Pose _pose;
    public float firerate = 0.25f;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.1f);
    private float nextFire;
    public float shotPower;
    public int numberOfBuletsPerShot;
    public AudioSource shotgunShot;

   public float offset;


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
        if (_fireAction.GetStateDown(_pose.inputSource) && Time.time > nextFire)
        {
            shotgunShot.Play();
            nextFire = Time.time + firerate;
            for (int i = 0; i < numberOfBuletsPerShot; i++)
            {
                var bullet = Instantiate(GameAssets.i.Bullet, Barrel.transform.position, transform.rotation);
                Vector3 dir = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 1f);
                Vector3 sprayDir = Parent.transform.TransformVector(dir);
                bullet.GetComponent<Rigidbody>().AddForce(sprayDir * shotPower);
            }
            
        }
        return false;
    }
}
