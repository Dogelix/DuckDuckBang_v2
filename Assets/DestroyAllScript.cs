using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static PowerUpController;

public class DestroyAllScript : MonoBehaviour
{

    private void Update()
    {
        RotateMe();
    }
    // Start is called before the first frame update
    private void OnDestroy()
    {
        int flyCount = Flock.agents.Count();

        for (int i = flyCount - 1; i >= 0; i--)
        {
            if (Flock.agents[i] != null)
            {
                Destroy(Flock.agents.ElementAt(i).gameObject);
            }
        }

        var walkingAgents = GameObject.FindGameObjectsWithTag("Enemy").Where(x => x.layer == LayerMask.NameToLayer("GroundEnemy")).ToList();
        foreach (var b in walkingAgents) // Destroys walking agents
        {
            Destroy(b); // Points script handles OnDestroy and does rest of the logic.
        }
    }

    private void RotateMe()
    {
        transform.Rotate(Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
