using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using static SoundManager;

public class RocketExplosion : MonoBehaviour
{
    public float blastRadius;

    private Collider[] hitColliders;

    void OnCollisionEnter(Collision col)
    {
        DoExplosion(col.contacts[0].point);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void DoExplosion(Vector3 explosionPoint)
    {
        hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius);

        foreach(Collider hitcol in hitColliders)
        {
            if (hitcol.gameObject.tag == "Enemy")
            {
                if (hitcol.gameObject.layer == LayerMask.NameToLayer("GroundEnemy"))
                {
                    hitcol.transform.gameObject.GetComponent<GroundAgentCollision>().RaycastDestroy();

                }
                else
                {
                    hitcol.transform.gameObject.GetComponent<CollisionDetection>().RaycastDestroy();
                }
            }
       
        }
    }
}
