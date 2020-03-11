using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollisionDetection : MonoBehaviour
{
    public FlockAgent agent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GameObjective")
        {
            if(!agent.lockHealthDamage)
            {
                agent.lockHealthDamage = true; // Lock damaging
                StartCoroutine(UnlockHealthDamage()); // Unlock Health damage after certain time
                agent.attack = false;
                agent.stayInRadius = true;
                agent.allign = true;
                collision.gameObject.GetComponentInChildren<TargetHealth>().TakeDamage();
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