using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Attack")]
public class AttackBehaviour : FlockBehaviour
{
    private GameObject[] targets;
    private void OnEnable()
    {
        targets = GameObject.FindGameObjectsWithTag("GameObjective");
    }
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    { 
        if (targets.Length > 0 && agent.attack)
        {
            int random = Random.Range(0, targets.Length);
            return targets[random].transform.position - agent.transform.position;
        }
        return Vector3.zero;
    }
}
