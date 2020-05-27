using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Attack")]
public class AttackBehaviour : FlockBehaviour
{

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    { 
        if (agent.attack && agent.target != null)
        {
            return agent.target.transform.position - agent.transform.position;
        }

        return Vector3.zero;
    }
}
