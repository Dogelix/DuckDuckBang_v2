using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RocketExplosion : MonoBehaviour
{
    public GameObject effect;
    public int radius;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        var obj = Instantiate(effect, transform.position, Quaternion.identity);
        obj.GetComponent<ParticleSystem>().loop = false;
        Destroy(obj, 2);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius).Where(x => x.gameObject.tag == "Enemy").ToArray();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Destroy(hitColliders[i].gameObject);
        }
    }
}
