using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollisionDetection : MonoBehaviour
{
    public FlockAgent agent;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GameObjective")
        {
            if (!agent.lockHealthDamage)
            {
                agent.lockHealthDamage = true; // Lock damaging
                StartCoroutine(UnlockHealthDamage()); // Unlock Health damage after certain time
                agent.attack = false;
                agent.stayInRadius = true;
                agent.allign = true;
                other.gameObject.GetComponentInChildren<TargetHealth>().TakeDamage();
            }
        }

        //if (other.gameObject.tag == "Enemy")
        //{

        //    Flock.agents.Remove(agent);
        //    Flock.agents.Add(agent);
        //}
    }

    private IEnumerator UnlockHealthDamage()
    {
        yield return new WaitForSeconds(3f); // Wait seconds before attacking again
        agent.lockHealthDamage = false;
    }

    public void RaycastDestroy()
    {
        // Remove from list first
        Flock.agents.Remove(agent);
        Destroy(gameObject);
    }
}