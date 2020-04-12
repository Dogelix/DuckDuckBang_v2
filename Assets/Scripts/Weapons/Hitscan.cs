using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR;
using static SoundManager;

public class Hitscan : MonoBehaviour, IShootable
{
    public GameObject Barrel;
    public GameObject Parent;

    public float firerate = 0.25f;

    public float laserRange = 5000f;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.1f);

    private LineRenderer laser;

    private float nextFire;

    private AudioSource shotSound;
    private WaveController waveController;

    void Start()
    {
        laser = GetComponent<LineRenderer>();
        shotSound = FindObjectOfType<AudioSource>();
        waveController = GameObject.FindObjectOfType<WaveController>();
        // ignore collision with area checker
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private IEnumerator FireEffect()
    {
        laser.enabled = true;
        yield return shotDuration;
        laser.enabled = false;
    }

    public bool Shoot()
    {
        if ( !( Time.time > nextFire ) ) return false;

        shotSound.Play();

        Debug.Log("Parent Forward Vector: " + Parent.transform.forward);
        nextFire = Time.time + firerate;

        StartCoroutine(FireEffect());

        RaycastHit hit = new RaycastHit();
        Ray shot = new Ray(Barrel.transform.position, Parent.transform.forward);
        laser.SetPosition(0, Barrel.transform.position);

        // Bit shift the index of the layer  to get a bit mask
        int layerMask = 1 << LayerMask.NameToLayer("SpawnArea");
        // This would cast rays only against colliders in layer 16.
        // But instead we want to collide against everything except layer SpawnArea. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        if ( Physics.Raycast(shot, out hit, laserRange, layerMask))
        {
            laser.SetPosition(1, hit.point);

            if ( hit.collider.tag == "Enemy" )
            {
                if ( hit.collider.gameObject.layer == LayerMask.NameToLayer("GroundEnemy") )
                {
                    Destroy(hit.transform.gameObject);
                }
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("AirEnemy"))
                {
                    waveController.Agents.Remove(hit.transform.gameObject);
                    Destroy(hit.transform.gameObject);
                }
            }
            else
            {
                //hit.transform.gameObject.GetComponent<QuitScript>().Activate();
            }
        }
        else
        {
            laser.SetPosition(1, Barrel.transform.position + ( laserRange * Parent.transform.forward ));
        }

        return true;
    }
}
