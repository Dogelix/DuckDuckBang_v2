using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Flock : MonoBehaviour
{
    public GameObject SpawnPoints;
    public FlockAgent agentPrefab;
    public static List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;

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
    public float attackDelay = 5f;


    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;


    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }


    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
        if (agents.Count() > 0)
        {
            var randomDuck = agents.FirstOrDefault(x => !x.lockHealthDamage && x.transform.position.y > 6); // Make sure that duck reached certain height before performing attack.
            if (randomDuck != null)
            {
                randomDuck.stayInRadius = false;
                randomDuck.allign = false;
                randomDuck.attack = true;
            }
        }
        // set different timing
        attackDelay = Random.Range(5f, 10f);
        StartCoroutine(Attack());
    }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        StartCoroutine(Attack()); ; // Init reccursive co-Routine for attacking
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
        foreach (FlockAgent agent in agents)
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
        foreach (var c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
