using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int _damage = 1;
    public float _despawnTime = 5.0f;

    private void Awake()
    {
        Destroy(gameObject, _despawnTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == uString.Enemy)
        {
            collision.transform.GetComponent<AgentHealth>().DoDamage(_damage);
            Destroy(gameObject);
        }
        if (collision.transform.tag == uString.Menu) collision.transform.GetComponent<MenuParentClass>().Activate();
    }
}
