﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollisionDetection : MonoBehaviour
{

    private Flock flock;
    public FlockAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        flock = GameObject.Find("/FlyingDucks").GetComponent<Flock>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // Destroy bullet too
            Destroy(collision.gameObject);
            // Remove from list first
            Flock.agents.Remove(agent);

            if (Flock.agents.Count == 0) // Respawn
            {
                flock.NextWave();
            } 

            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "GameObjective")
        {
            if(!agent.lockHealthDamage)
            {
                agent.lockHealthDamage = true; // Lock damaging
                StartCoroutine(UnlockHealthDamage()); // Unlock Health damage after certain time
                collision.gameObject.GetComponentInChildren<TargetHealth>().TakeDamage();
                agent.attack = false;
                agent.stayInRadius = true;
                agent.allign = true;
            }           
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Flock.agents.Remove(agent);
            Flock.agents.Add(agent);
        }
    }


    private IEnumerator UnlockHealthDamage()
    {
        yield return new WaitForSeconds(2f); // Wait two seconds before attacking again
        agent.lockHealthDamage = false;
    }
}
