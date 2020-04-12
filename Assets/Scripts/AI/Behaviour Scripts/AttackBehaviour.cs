using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Flock/Behaviour/Attack")]
public class AttackBehaviour : FlockBehaviour
{

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (agent.attack)
        {
            // Check if player is in the area 
            var spawnAreas = GameObject.FindGameObjectsWithTag("SpawnArea");
            var standingArea = spawnAreas.FirstOrDefault(x => x.GetComponent<BoxCollider>().bounds.Contains(flock.Player.transform.position));
            var levelController = standingArea.GetComponentInParent<LevelController>();
            if (levelController.LevelNumber == agent.currentLevel)
            {
                var direction = flock.Player.transform.position - agent.transform.position;
                // Raycast direction for the player to check for collision
                RaycastHit hit = new RaycastHit();
                Ray dir = new Ray(agent.transform.position, direction);
                int layerMask = 1 << LayerMask.NameToLayer("Physics");
                if (Physics.Raycast(dir, out hit, 10000, layerMask))
                {
                    return agent.transform.up * Time.deltaTime * 8;
                }
                return flock.Player.transform.position - agent.transform.position;
            }
            else
            {
                agent.attack = false;
                agent.stayInRadius = true;
                agent.allign = true;
            }
        }

        return Vector3.zero;
    }
}
