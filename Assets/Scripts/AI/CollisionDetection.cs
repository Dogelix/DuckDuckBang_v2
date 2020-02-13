using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            flock.agents.Remove(agent);

            if (flock.agents.Count == 0) // Respawn
            {
                flock.NextWave();
            } 

            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "GameObjective")
        {
            collision.gameObject.GetComponentInChildren<TargetHealth>().TakeDamage();
            agent.attack = false;
            agent.stayInRadius = true;
            agent.allign = true;
        }
    }
}
