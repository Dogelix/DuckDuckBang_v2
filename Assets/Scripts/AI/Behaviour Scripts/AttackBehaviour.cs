using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Attack")]
public class AttackBehaviour : FlockBehaviour
{
    private GameObject target;
    private void OnEnable()
    {
        target = GameObject.Find("/pfPicNicBasket_main");
    }
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    { 
        if (agent.attack)
        {
            return target.transform.position - agent.transform.position;
        }
        return Vector3.zero;
    }
}
