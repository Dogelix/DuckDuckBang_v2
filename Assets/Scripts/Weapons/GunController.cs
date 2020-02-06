using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
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
            var bullet = Instantiate(bulletPrefab, Barrel.transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(Parent.transform.forward * shotPower);
            // Wait 5 seconds and check if bullet still exists.
            StartCoroutine(WaitAndDestroy(5.0f, bullet));
        }
    }

    private IEnumerator WaitAndDestroy(float waitTime, GameObject prefab)
    {
        yield return new WaitForSeconds(waitTime);
        // Bullet execution time has ended, see if it still exists, if so; Destroy it
        if (prefab != null)
        {
            Destroy(prefab);
        }
    }
}
