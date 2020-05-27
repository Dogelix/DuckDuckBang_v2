using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static SoundManager;

public class RocketExplosion : MonoBehaviour
{
    public int radius;
    public int blastBackRadius;
    public GameObject mushroom;
    private void OnCollisionEnter(Collision collision)
    {
        var pos = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
        Instantiate(mushroom, pos, Quaternion.identity);
        FindObjectOfType<SoundManager>().PlaySound(SoundsNames.explosion, false, false);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        transform.Rotate(10, 20, 0, Space.Self);
    }

    private void OnDestroy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius).Where(x => x.gameObject.tag == "Enemy").ToArray();
        Collider[] blastHitColliders = Physics.OverlapSphere(transform.position, blastBackRadius).Where(x => x.gameObject.tag == "Enemy").ToArray();

        for (int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].gameObject.GetComponent<AgentHealth>().DoDamage(20);
        }

        for ( int i = 0; i < blastHitColliders.Length; i++ )
        {
            blastHitColliders[i].GetComponent<Rigidbody>().AddExplosionForce(25, transform.position, blastBackRadius);
        }
    }
}
