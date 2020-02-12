using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilterFlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, return no adjustment
        if (context.Count == 0)
            return Vector3.zero;
        //add all points together
        Vector3 cohesionMove = Vector3.zero;
        foreach(Transform item in context)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= context.Count;
        //create offset from agent pos
        cohesionMove -= agent.transform.position;
        return cohesionMove;
    }
}
