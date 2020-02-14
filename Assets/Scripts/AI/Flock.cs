using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Flock : MonoBehaviour
{
    public GameObject SpawnPoints;
    public FlockAgent agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();
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
    public float spawnDelay = 0.5f;
    public float attackDelay = 5f;


    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;

    int spawnCount = 0;
    Vector3[] spawnPoints;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        // Get random spawn point
        var pos = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var newAgent = Instantiate(agentPrefab, pos, Quaternion.identity);
        newAgent.SetCollider();
        newAgent.name = "Flying Duck " + agents.Count() + 1;
        agents.Add(newAgent);
        spawnCount++;
        if (spawnCount < startingCount)
        {
            StartCoroutine(Spawn());
        }      
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
        if (agents.Count() > 0)
        {
            var randomDuck = agents[Random.Range(0, agents.Count())];
            if(!randomDuck.lockHealthDamage)
            {
                randomDuck.stayInRadius = false;
                randomDuck.allign = false;
                randomDuck.attack = true;
            }           
        }
        // set different timing
        attackDelay = Random.Range(1f, 7f);
        StartCoroutine(Attack());
    }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        // Populate spawn points
        var tempList = new List<Vector3>();
        foreach (Transform child in SpawnPoints.transform)
        {
            tempList.Add(child.position);
        }
        spawnPoints = tempList.ToArray(); // convert to array to improve perfomrance
        StartCoroutine(Spawn());
        StartCoroutine(Attack());
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

    public void NextWave()
    {
        spawnCount = 0;
        startingCount += (int)Random.Range(4, 8);
        StartCoroutine(Spawn());
    }
}
