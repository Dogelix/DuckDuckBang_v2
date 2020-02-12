using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Flock/Behaviour/Aligment")]
public class AllignmentBehaviour : FilterFlockBehaviour
{

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        context.RemoveAll(x => x.tag == "Bullet");
        //if no neighbours, return no adjustment
        if (context.Count == 0)
            return agent.transform.forward;
        //add all points together
        Vector3 alignmentMove = Vector3.zero;
        foreach (Transform item in context)
        {
            alignmentMove += item.transform.forward;
        }
        alignmentMove /= context.Count;
        return alignmentMove;
    }
}
