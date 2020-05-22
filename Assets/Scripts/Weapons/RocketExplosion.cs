using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RocketExplosion : MonoBehaviour
{
    public GameObject effect;
    public int radius;
    public int blastBackRadius;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        transform.Rotate(10, 20, 0, Space.Self);
    }

    private void OnDestroy()
    {
        var obj = Instantiate(effect, transform.position, Quaternion.identity);
        obj.GetComponent<ParticleSystem>().loop = false;
        Destroy(obj, 2);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius).Where(x => x.gameObject.tag == "Enemy").ToArray();
        Collider[] blastHitColliders = Physics.OverlapSphere(transform.position, blastBackRadius).Where(x => x.gameObject.tag == "Enemy").ToArray();

        for (int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].gameObject.GetComponent<AgentHealth>().DoDamage(5);
        }

        for ( int i = 0; i < blastHitColliders.Length; i++ )
        {
            blastHitColliders[i].GetComponent<Rigidbody>().AddExplosionForce(25, transform.position, blastBackRadius);
        }
    }
}
