using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _despawnTime = 5.0f;

    private void Awake()
    {
        Destroy(gameObject, _despawnTime);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == uString.Enemy)
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<AgentHealth>().DoDamage(1);
        }
        if (collision.transform.tag == uString.Menu) collision.transform.GetComponent<IMenuItem>().Activate();
    }
}
