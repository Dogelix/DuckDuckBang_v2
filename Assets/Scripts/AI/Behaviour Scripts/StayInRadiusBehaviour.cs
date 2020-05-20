using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    public float radius = 15f;
    public float height;
    private GameObject center;

    private void OnEnable()
    {
        center = GameObject.FindGameObjectWithTag("Player");
    }

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (center == null)
        {
            center = GameObject.FindGameObjectWithTag("Player");
        }

        if (agent.stayInRadius)
        {
            var higher = new Vector3(center.transform.position.x, height, center.transform.position.z);

            Vector3 centerOffset = higher - agent.transform.position;

            float t = centerOffset.magnitude / radius;
            if (t < 0.9f)
            {
                return Vector3.zero;
            }
            centerOffset = centerOffset * t * t;

            return centerOffset;
        }
        else
        {
            return Vector3.zero;
        }
        
    }
}
