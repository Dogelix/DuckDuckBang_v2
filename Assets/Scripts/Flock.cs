using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GizmosHelper gizmosHelper;
    public FlockAgent agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;

    [Range(10, 40)]
    public int startingCount = 10;
    const float AgentDensity = 0.04f;

    [Range(1f, 100f)]
    public float driveFactor = 10;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.8f;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private void Spawn()
    {
        bool loop = true;
        Vector3? pos = null;
        FlockAgent newAgent = null;
        for (int i = 0; i < startingCount; i++)
        {
            while (loop)
            {
                pos = gizmosHelper.center + new Vector3(Random.Range(-gizmosHelper.size.x / 2, gizmosHelper.size.x / 2),
                                                        Random.Range(-gizmosHelper.size.y / 2, gizmosHelper.size.y / 2),
                                                        Random.Range(-gizmosHelper.size.z / 2, gizmosHelper.size.z / 2));
                // We have to instantiate object even if we dont want them on the screen because we need bounds taken from transformation matrix on actual position
                newAgent = Instantiate(agentPrefab, pos.Value, Quaternion.identity);
                newAgent.SetCollider();
                if (!IsCollidingWithOthers(newAgent.AgentCollider.bounds))
                {
                    loop = false;
                }
                else
                {
                    // Objest is destroyed before being displaed
                    Destroy(newAgent.gameObject);
                }
            }
            loop = true;
            newAgent.name = "Agent " + 1;
            agents.Add(newAgent);
        }
    }
    // Start is called before the first frame update
    void Start()
    {       
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        Spawn();
    }

    private bool IsCollidingWithOthers(Bounds b)
    {
        for (int i = 0; i < agents.Count; i++)
        {
            if (agents[i].AgentCollider.bounds.Intersects(b))
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            Vector3 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
        foreach(var c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

    public void Respawn()
    {
        startingCount += (int)Random.Range(4, 8);
        Spawn();
    }
}
